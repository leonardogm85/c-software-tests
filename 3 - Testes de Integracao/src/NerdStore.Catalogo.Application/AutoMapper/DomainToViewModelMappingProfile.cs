using AutoMapper;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Domain;

namespace NerdStore.Catalogo.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Categoria, CategoriaViewModel>();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(v => v.Largura, m => m.MapFrom(p => p.Dimensoes.Largura))
                .ForMember(v => v.Altura, m => m.MapFrom(p => p.Dimensoes.Altura))
                .ForMember(v => v.Profundidade, m => m.MapFrom(p => p.Dimensoes.Profundidade));
        }
    }
}
