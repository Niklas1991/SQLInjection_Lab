using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLInjection_Lab1.Models
{
	public class Products
	{
		public int ID { get; set; }
		public string ProductName { get; set; }
		public int ProductPrice { get; set; }
		public int Quantity { get; set; }
	}
}
