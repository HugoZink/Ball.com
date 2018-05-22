using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI
{
    public class EventHandler : IMessageHandlerCallback
	{
		IMessageHandler _messageHandler;

		public EventHandler(IMessageHandler messageHandler)
		{
			_messageHandler = messageHandler;
		}

		public void Start()
		{
			_messageHandler.Start(this);
		}

		public void Stop()
		{
			_messageHandler.Stop();
		}

		public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
		{
			JObject messageObject = MessageSerializer.Deserialize(message);
			try
			{
				switch (messageType)
				{
				}
			}
			catch (Exception ex)
			{
				string messageId = messageObject.Property("MessageId") != null ? messageObject.Property("MessageId").Value<string>() : "[unknown]";
				Console.WriteLine($"Error while handling {messageType} message with id {messageId}.\n{ex.ToString()}");
			}

			// always akcnowledge message - any errors need to be dealt with locally.
			return true;
		}
	}
}
