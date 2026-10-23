using System;
using System.Collections.Generic;
using System.Text;

namespace DemoShoppe.Domain.Entities
{
    public class ProductCategory
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }

        public Product Product { get; set; } 
        public Category Category { get; set; } 
    }
}
