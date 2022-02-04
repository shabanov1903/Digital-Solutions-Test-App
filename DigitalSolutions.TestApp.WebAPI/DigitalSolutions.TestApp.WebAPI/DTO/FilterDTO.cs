using DigitalSolutions.TestApp.WebAPI.DataBase.DBContext;

namespace DigitalSolutions.TestApp.WebAPI.DTO
{
    public class FilterDTO
    {
        public string? minCreateTime { get; set; }
        public string? maxCreateTime { get; set; }
        public string? minChangeTime { get; set; }
        public string? maxChangeTime { get; set; }
        public int minAccountNumber { get; set; }
        public int maxAccountNumber { get; set; }
        public string? accountStatus { get; set; }
        public long minBalance { get; set; }
        public long maxBalance { get; set; }
        public string? paymentMethod { get; set; }
    }
}
