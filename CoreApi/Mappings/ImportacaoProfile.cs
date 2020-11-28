using AutoMapper;
using CoreApi.DTO;
using CoreApi.Entities.Master;

namespace CoreApi.Mappings
{
    public class ImportacaoProfile : Profile
    {
        public ImportacaoProfile()
        {
            CreateMap<DtoImportacao, Entregas>().ReverseMap();
        }
    }
}
