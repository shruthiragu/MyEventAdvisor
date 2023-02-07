using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderApi.Data;
using RabbitMQ.Client;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<OrdersContext>(options => options.UseSqlServer(configuration["ConnectionString"]));

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var identityUrl = configuration["IdentityUrl"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = identityUrl.ToString();
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = false,
        ValidateIssuer = false,
        ValidateAudience = false
    };
    options.Audience = "order";

});

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddBus(provider =>
    {
        return Bus.Factory.CreateUsingRabbitMq(rmq =>
        {
            rmq.Host(new Uri("rabbitmq://rabbitmq"), "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            rmq.ExchangeType = ExchangeType.Fanout;
            MessageDataDefaults.ExtraTimeToLive = TimeSpan.FromDays(1);
        });
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProviders = scope.ServiceProvider;
    var context = serviceProviders.GetRequiredService<OrdersContext>();
    MigrateDatabase.EnsureCreated(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
