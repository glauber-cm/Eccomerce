using AutoMapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Web.ViewModels;

namespace Ecommerce.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
        }
    }
}
