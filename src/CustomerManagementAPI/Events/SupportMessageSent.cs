using Pitstop.CustomerManagementAPI.Model;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pitstop.CustomerManagementAPI.Commands
{
	public class SupportMessageSent : Event
	{
		public readonly string SupportMessageId;
		public readonly string Content;
		public readonly MessageType SupportMessageType;

		public SupportMessageSent(Guid messageId, string supportMessageId, string content, MessageType supportMessageType) : base(messageId, MessageTypes.SupportMessageSent)
		{
			SupportMessageId = supportMessageId;
			Content = content;
			SupportMessageType = supportMessageType;
		}
	}
}
