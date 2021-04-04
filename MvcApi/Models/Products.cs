using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApi.Models
{
    public class Products
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int QntyInStock { get; set; }

        public string Description { get; set; }

        public string Suppiler { get; set; }
    }
}
