﻿using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventCatalogAPI.Data
{
    public class EventCatalogContext : DbContext
    {
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventLocation> EventLocations { get; set; }
        public DbSet<EventOrganizer> EventOrganizers { get; set; }
        public DbSet<EventItem> EventItems { get; set; }

        public EventCatalogContext (DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventCategory>(e =>
            {
                e.Property(t => t.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<EventCategory>(e =>
            {
                e.Property(t => t.Category)
                        .IsRequired()
                        .HasMaxLength(100);
            });

            modelBuilder.Entity<EventLocation>(e =>
            {
                e.Property(t => t.Id)
                        .IsRequired()
                        .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<EventLocation>(e =>
            {
                e.Property(t => t.LocationName)
                        .IsRequired()
                        .HasMaxLength(100);
            });

            modelBuilder.Entity<EventOrganizer>(e =>
            {
                e.Property(t => t.Id)
                        .IsRequired()
                        .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<EventOrganizer>(e =>
            {
                e.Property(t => t.OrganizerName)
                        .IsRequired()
                        .HasMaxLength(100);
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.Property(t => t.Id)
                        .IsRequired()
                        .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.Property(t => t.Price)
                        .IsRequired();
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.Property(t => t.Name)
                        .IsRequired()
                        .HasMaxLength(200);
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.Property(t => t.EventAddress)
                        .IsRequired()
                        .HasMaxLength(300);
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.Property(t => t.Date)
                        .IsRequired();
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.Property(t => t.StartTime)
                        .IsRequired();
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.Property(t => t.EndTime)
                        .IsRequired();
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.Property(t => t.Description)
                        .IsRequired();
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.HasOne(l => l.EventLocation)
                        .WithMany()
                        .HasForeignKey(l => l.EventLocationId);
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.HasOne(c => c.EventCategory)
                        .WithMany()
                        .HasForeignKey(c => c.EventCategoryId);
            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.HasOne(o => o.EventOrganizer)
                        .WithMany()
                        .HasForeignKey(o => o.EventOrganizerId);
            });
        }

    }
}
