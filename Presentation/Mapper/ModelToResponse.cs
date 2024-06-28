using AutoMapper;
using Domain.Publishing.Models.Entities;
using Domain.Publishing.Models.Entities.Orders;
using Domain.Publishing.Models.Entities.Product;
using Domain.Publishing.Models.Response;
using Domain.Publishing.Models.Response.OrderResponse;

namespace WS51_MyEvent.Middleware;

public class ModelToResponse:Profile
{
    public ModelToResponse()
    {
        CreateMap<Customer, CustomerResponse>();
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.ParametrosPersonalizacion, opt => opt.MapFrom(src => src.ParametrosPersonalizacion))
            .ForMember(dest => dest.Caracteristicas, opt => opt.MapFrom(src => src.Caracteristicas))
            .ForMember(dest => dest.ImagenesDetalle, opt => opt.MapFrom(src => src.ImagenesDetalle));
        CreateMap<ParametrosPersonalizacion, ParametrosPersonalizacionResponse>();
        CreateMap<Parametro, ParametroResponse>();
        CreateMap<ValorParametro, ValorParametroResponse>();
        CreateMap<Imagen, ImagenResponse>();
        CreateMap<Caracteristica, CaracteristicaResponse>();

        CreateMap<Order, OrderResponse>();
        CreateMap<OrderParameter, OrderParametersResponse>();
    }
}