using AutoMapper;
using CoreApi.DTO;
using CoreApi.Entities.Master;

namespace CoreApi.Mappings
{
    public class LoteImportacaoProfile : Profile
    {
        public LoteImportacaoProfile() 
        {
            CreateMap<DtoLoteImportacao, LoteEntregas>()
                .ForMember(o => o.IdLoteEntrega, opt => opt.MapFrom(src => src.Id))
                .ForMember(o => o.DtEntregaMenor, opt => opt.MapFrom(src => src.MenorDataEntrega))
                .ReverseMap();
        }
    }
}
