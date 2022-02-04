using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSolutions.TestApp.WebAPI.DataBase.DBContext
{
    public record AccountFilter
    {
        public DateTime minCreateTime { get; set; } = DateTime.MinValue;
        public DateTime maxCreateTime { get; set; } = DateTime.MaxValue;
        public DateTime minChangeTime { get; set; } = DateTime.MinValue;
        public DateTime maxChangeTime { get; set; } = DateTime.MaxValue;
        public int minAccountNumber { get; set; } = int.MinValue;
        public int maxAccountNumber { get; set; } = int.MaxValue;
        public AccountStatus accountStatus { get; set; }
        public long minBalance { get; set; } = long.MinValue;
        public long maxBalance { get; set; } = long.MaxValue;
        public PaymentMethod paymentMethod { get; set; }
    }
}
