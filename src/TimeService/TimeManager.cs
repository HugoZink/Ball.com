using Pitstop.Infrastructure.Messaging;
using Pitstop.TimeService.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pitstop.TimeService
{
    public class TimeManager
    {
        TimeSpan start;
        CancellationTokenSource _cancellationTokenSource;
        Task _task;
        IMessagePublisher _messagePublisher;


        public TimeManager(IMessagePublisher messagePublisher)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            start = new TimeSpan(10, 0, 0);
            _messagePublisher = messagePublisher;
        }

        public void Start()
        {
            _task = Task.Run(() => Worker(), _cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        private async void Worker()
        {
            while (true)
            {
				TimeSpan now = DateTime.Now.TimeOfDay;

				if (start == now)
                {
                    Console.WriteLine($"Day has begun!");
                    DayHasBegun e = new DayHasBegun(Guid.NewGuid());
                    await _messagePublisher.PublishMessageAsync(MessageTypes.DayHasBegun, e, "");
                }

                Thread.Sleep(10000);
            }
        }
    }
}
