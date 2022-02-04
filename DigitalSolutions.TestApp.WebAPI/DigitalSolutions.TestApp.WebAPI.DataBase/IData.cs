using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalSolutions.TestApp.WebAPI.DataBase.DBContext;

namespace DigitalSolutions.TestApp.WebAPI.DataBase
{
    public interface IData
    {
        public Task<List<AccountContext>> GetAccountList(AccountFilter filter);
        public Task<AccountContext> GetAccountById(int Id);
        public Task AddAccount(AccountContext context);
        public Task ChangeAccount(AccountContext context);
    }
}
