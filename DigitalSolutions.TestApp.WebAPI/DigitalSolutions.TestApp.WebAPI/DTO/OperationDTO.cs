using DigitalSolutions.TestApp.WebAPI.DataBase.DBContext;

namespace DigitalSolutions.TestApp.WebAPI.DTO
{
    public class OperationDTO
    {
        public OperationDTO(bool value)
        {
            successful = value;
        }
        public bool successful { get; set; }
    }
}
