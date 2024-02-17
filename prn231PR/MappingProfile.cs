using AutoMapper;
using DataAccsess.DTO;
using DataAccsess.Models;

namespace prn231PR
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDetail, OrderDetailDTO>();
        }
    }
}
