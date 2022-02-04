using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSolutions.TestApp.WebAPI.DataBase.DBContext
{
    public class AccountContext
    {
        [Name("createTime")]
        public DateTime createTime { get; set; }
        [Name("changeTime")]
        public DateTime changeTime { get; set; }
        [Name("accountNumber")]
        public int accountNumber { get; set; }
        [Name("accountStatus")]
        public AccountStatus accountStatus { get; set; }
        [Name("balance")]
        public long balance { get; set; }
        [Name("paymentMethod")]
        public PaymentMethod paymentMethod { get; set; }
    }
}
