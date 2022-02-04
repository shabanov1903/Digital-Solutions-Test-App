using DigitalSolutions.TestApp.WebAPI.DataBase.DBContext;

namespace DigitalSolutions.TestApp.WebAPI.DTO
{
    public class AccountDTO
    {
        public string? createTime { get; set; }
        public string? changeTime { get; set; }
        public int accountNumber { get; set; }
        public string? accountStatus { get; set; }
        public long balance { get; set; }
        public string? paymentMethod { get; set; }
    }
}
