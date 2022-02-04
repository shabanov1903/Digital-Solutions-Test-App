using AutoMapper;
using DigitalSolutions.TestApp.WebAPI.DataBase.DBContext;
using DigitalSolutions.TestApp.WebAPI.DTO;
using System.Linq.Expressions;

namespace DigitalSolutions.TestApp.WebAPI
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AccountContext, AccountDTO>();
            CreateMap<AccountDTO, AccountContext>();
            CreateMap<AccountFilter, FilterDTO>();
            CreateMap<FilterDTO, AccountFilter>();
        }
    }
}
