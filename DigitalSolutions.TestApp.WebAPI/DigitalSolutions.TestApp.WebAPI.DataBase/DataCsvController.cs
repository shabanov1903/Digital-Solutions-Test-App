using CsvHelper;
using CsvHelper.Configuration;
using DigitalSolutions.TestApp.WebAPI.DataBase.DBContext;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSolutions.TestApp.WebAPI.DataBase
{
    public class DataCsvController : IData
    {
        private readonly string _path;
        private Type accountContextType = typeof(AccountContext);
                
        public DataCsvController(string path)
        {
            _path = path;
            if (!File.Exists(_path)) File.AppendAllText(_path, GetInitialCsvString(accountContextType));
        }

        public async Task<bool> AddAccount(AccountContext context)
        {
            var checkAccountNumber = await GetAccountById(context.accountNumber);
            if (checkAccountNumber.accountNumber == context.accountNumber)
            {
                return false;
            }
            context.createTime = DateTime.Now;
            context.changeTime = DateTime.Now;
            context.accountStatus = AccountStatus.New;
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
            using (var stream = File.Open(_path, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                await Task.Run(() => {
                    csv.WriteRecord(context);
                    csv.NextRecord();
                });
            }
            return true;
        }

        public async Task ChangeAccount(AccountContext context)
        {
            StringBuilder text;
            var changeAccount = await GetAccountById(context.accountNumber);
            context.changeTime = DateTime.Now;
            using (var reader = new StreamReader(_path))
            {
                text = new StringBuilder(await reader.ReadToEndAsync());
                text = text.Replace(GetCsvString(changeAccount), GetCsvString(context));
            }
            using (var writer = new StreamWriter(_path))
            {
                await writer.WriteAsync(text.ToString());
                await writer.FlushAsync();
            }
        }

        public async Task<AccountContext> GetAccountById(int Id)
        {
            var resultList = new List<AccountContext>();
            using (var reader = new StreamReader(_path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecordsAsync<AccountContext>();
                await foreach (var record in records)
                {
                    resultList.Add(record);
                }
            }
            try
            {
                return (from t in resultList where t.accountNumber == Id select t).First();
            }
            catch
            {
                return new AccountContext();
            }
        }

        public async Task<List<AccountContext>> GetAccountList(AccountFilter filter)
        {
            var result = new List<AccountContext>();
            using (var reader = new StreamReader(_path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecordsAsync<AccountContext>();
                await foreach (var record in records)
                {
                    result.Add(record);
                }
            }
            result = (from t in result
                      where t.createTime <= filter.maxCreateTime && t.createTime >= filter.minCreateTime &&
                            t.changeTime <= filter.maxChangeTime && t.changeTime >= filter.minChangeTime &&
                            t.accountNumber <= filter.maxAccountNumber && t.accountNumber >= filter.minAccountNumber &&
                            t.balance <= filter.maxBalance && t.balance >= filter.minBalance
                      select t).ToList();
            if (filter.accountStatus != 0)
            {
                result = (from t in result
                          where t.accountStatus == filter.accountStatus
                          select t).ToList();
            }
            if (filter.paymentMethod != 0)
            {
                result = (from t in result
                          where t.paymentMethod == filter.paymentMethod
                          select t).ToList();
            }
            return result;
        }

        private string GetInitialCsvString(Type type)
        {
            StringBuilder result = new StringBuilder();
            PropertyInfo[] fields = type.GetProperties();
            foreach (PropertyInfo field in fields)
            {
                result.Append(field.Name);
                result.Append(",");
            }
            result.Remove(result.Length - 1, 1);
            result.Append("\n");
            return result.ToString();
        }

        private string GetCsvString(AccountContext context)
        {
            StringBuilder result = new StringBuilder();
            PropertyInfo[] fields = context.GetType().GetProperties();
            foreach (PropertyInfo field in fields)
            {
                result.Append(Convert.ToString(field.GetValue(context), CultureInfo.InvariantCulture));
                result.Append(",");
            }
            result.Remove(result.Length - 1, 1);
            result.Append("\r\n");
            return result.ToString();
        }
    }
}
