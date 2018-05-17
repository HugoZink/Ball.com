using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pitstop.CustomerManagementAPI.Model
{
    public class SupportMessage
    {
		public string SupportMessageId { get; set; }

		public string Content { get; set; }

		public MessageType MessageType { get; set; }
    }

	public enum MessageType
	{
		FROMCUSTOMER,
		FROMSUPPORT
	}
}
