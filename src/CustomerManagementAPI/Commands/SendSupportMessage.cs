using Pitstop.CustomerManagementAPI.Model;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pitstop.CustomerManagementAPI.Commands
{
    public class SendSupportMessage : Command
    {
		public readonly string SupportMessageId;
		public readonly string Content;
		public readonly MessageType SupportMessageType;

		public SendSupportMessage(Guid messageId, string supportMessageId, string content, MessageType supportMessageType) : base(messageId, MessageTypes.SendSupportMessage)
        {
			SupportMessageId = supportMessageId;
			Content = content;
			SupportMessageType = supportMessageType;
		}
	}
}
