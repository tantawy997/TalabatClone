﻿using Core.entites.OrderAggragate;

namespace WepAPIAssignment.Dtos
{
    public class OrderItemDTO
    {


        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int Qunatity { get; set; }
    }
}