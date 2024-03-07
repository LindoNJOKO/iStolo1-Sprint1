﻿using System.ComponentModel.DataAnnotations;

namespace iStolo1.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }
        public string ProductDescription { get; set; }

    }
}
