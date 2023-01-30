﻿using System;
namespace WebMvc.Models.OrderModels
{
	public class OrderItem
	{
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
    }
}

