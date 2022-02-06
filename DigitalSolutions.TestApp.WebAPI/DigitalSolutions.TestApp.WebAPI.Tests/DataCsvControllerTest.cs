using DigitalSolutions.TestApp.WebAPI.DataBase;
using DigitalSolutions.TestApp.WebAPI.DataBase.DBContext;
using System.Threading.Tasks;
using Xunit;

namespace DigitalSolutions.TestApp.WebAPI.Tests
{
    public class DataCsvControllerTest
    {
        [Fact]
        public async Task CsvOperations()
        {
            string _path = @"D:\Programming\Testing\Invoices.csv";

            var _dataController = new DataCsvController(_path);
            var acc1 = new AccountContext() {
                createTime = System.DateTime.Now,
                changeTime = System.DateTime.Now,
                accountNumber = 1,
                accountStatus = AccountStatus.New,
                balance = 100,
                paymentMethod = PaymentMethod.DebitCard
            };
            var acc2 = new AccountContext()
            {
                createTime = System.DateTime.Now,
                changeTime = System.DateTime.Now,
                accountNumber = 2,
                accountStatus = AccountStatus.Paid,
                balance = 200,
                paymentMethod = PaymentMethod.DebitCard
            };
            var acc3 = new AccountContext()
            {
                createTime = System.DateTime.Now,
                changeTime = System.DateTime.Now,
                accountNumber = 3,
                accountStatus = AccountStatus.Paid,
                balance = 300,
                paymentMethod = PaymentMethod.DebitCard
            };
            var acc4 = new AccountContext()
            {
                createTime = System.DateTime.Now,
                changeTime = System.DateTime.Now,
                accountNumber = 4,
                accountStatus = AccountStatus.Canceled,
                balance = 400,
                paymentMethod = PaymentMethod.DebitCard
            };
            var acc5 = new AccountContext()
            {
                createTime = System.DateTime.Now,
                changeTime = System.DateTime.Now,
                accountNumber = 4,
                accountStatus = AccountStatus.Canceled,
                balance = 500,
                paymentMethod = PaymentMethod.DebitCard
            };

            Assert.True(await _dataController.AddAccount(acc1));
            Assert.True(await _dataController.AddAccount(acc2));
            Assert.True(await _dataController.AddAccount(acc3));
            Assert.True(await _dataController.AddAccount(acc4));
            Assert.False(await _dataController.AddAccount(acc5));

            var acc1FromQuery = await _dataController.GetAccountById(1);
            Assert.True(acc1FromQuery.accountNumber == 1);

            acc1.balance = 1000;
            await _dataController.ChangeAccount(acc1);
            acc1FromQuery = await _dataController.GetAccountById(1);
            Assert.True(acc1FromQuery.balance == 1000);

            var filter1 = new AccountFilter() {
                minCreateTime = System.DateTime.MinValue,
                maxCreateTime = System.DateTime.MaxValue,
                minChangeTime = System.DateTime.MinValue,
                maxChangeTime = System.DateTime.MaxValue,
                minAccountNumber = 1,
                maxAccountNumber = int.MaxValue,
                accountStatus = 0,
                minBalance = 0,
                maxBalance = 2000,
                paymentMethod = 0
            };
            var filter2 = new AccountFilter()
            {
                minCreateTime = System.DateTime.MinValue,
                maxCreateTime = System.DateTime.MaxValue,
                minChangeTime = System.DateTime.MinValue,
                maxChangeTime = System.DateTime.MaxValue,
                minAccountNumber = 1,
                maxAccountNumber = 2,
                accountStatus = 0,
                minBalance = 0,
                maxBalance = 2000,
                paymentMethod = 0
            };
            var accListFromQuery1 = await _dataController.GetAccountList(filter1);
            var accListFromQuery2 = await _dataController.GetAccountList(filter2);
            Assert.True(accListFromQuery1.Count == 4);
            Assert.True(accListFromQuery2.Count == 2);

            System.IO.File.Delete(_path);
        }
    }
}