﻿
namespace Mango.Service.ProductAPI.Models.Dto
{
    public class ProductDto
    {
        
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CagegoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
