using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Products.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Type { get; set; }
    }
}