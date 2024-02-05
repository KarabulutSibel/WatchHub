using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
	public class ProductNotFoundException : Exception
	{
        public ProductNotFoundException(int productId) : base($"Product with the id\"{productId}\" couldn't be found!")
        {
            
        }
    }
}
