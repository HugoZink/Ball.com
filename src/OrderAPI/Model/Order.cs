using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Model
{
    public class Order
    {
        public string OrderId { get; set; }

        public Customer Customer { get; set; }

        public DateTime DateTime { get; set; }

        public string TrackingCode { get; set; }

        public List<OrderStateChange> StateChanges { get; set; }

        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; }

        public bool AfterPayment { get; set; }

        [NotMapped]
        public IEnumerable<Product> Products
        {
            get
            {
                var products = new List<Product>();
                foreach (OrderProduct op in OrderProducts)
                {
                    products.Add(op.Product);
                }

                return products;
            }
        }

        [NotMapped]
        public string CurrentState
        {
            get
            {
                return StateChanges.OrderBy(s => s.Version).LastOrDefault().State;
            }
        }

        [NotMapped]
        public OrderStateChange LastStateChange
        {
            get
            {
                return StateChanges.OrderBy(s => s.Version).LastOrDefault();
            }
        }

        public Order()
        {
            this.OrderProducts = new List<OrderProduct>();
            this.StateChanges = new List<OrderStateChange>();
        }

        public void AddStateChange(string newState)
        {
            int newVersion;
            var lastState = LastStateChange;
            if (lastState == null)
            {
                newVersion = 1;
            }
            else
            {
                newVersion = lastState.Version + 1;
            }

            this.StateChanges.Add(new OrderStateChange()
            {
                State = newState,
                Version = newVersion
            });
        }
    }

    public static class OrderState
    {
        public const string PENDING = "Pending";
        public const string PAYMENTINPROGRESS = "Payment In Progress";
        public const string PAYMENTCOMPLETE = "PaymentComplete";
        public const string AWAITINGAFTERPAYMENT = "AwaitingAfterPayment";
        public const string CLOSED = "Closed";
    }
}
