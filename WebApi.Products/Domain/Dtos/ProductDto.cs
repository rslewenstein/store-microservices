using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Products.Domain.Dtos
{
    public class ProductDto
    {
        public string? ProductName { get; set; }
        public string? Type { get; set; }
    }
}