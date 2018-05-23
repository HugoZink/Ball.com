using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Model
{
    public class OrderStateChange
    {
		public string Id { get; set; }

		public int Version { get; set; }

		public string State { get; set; }
    }
}
