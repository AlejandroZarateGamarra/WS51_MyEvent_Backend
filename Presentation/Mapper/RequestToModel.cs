using AutoMapper;
using Domain.Publishing.Models.Commands.CustomerCommands;
using Domain.Publishing.Models.Commands.OrderCommands;
using Domain.Publishing.Models.Commands.ProductCommands;
using Domain.Publishing.Models.Entities;
using Domain.Publishing.Models.Entities.Orders;
using Domain.Publishing.Models.Entities.Product;

namespace WS51_MyEvent.Mapper;

public class RequestToModel:Profile
{
    public RequestToModel()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerPasswordCommand, Customer>();

        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.ParametrosPersonalizacion, opt => opt.MapFrom(src => src.ParametrosPersonalizacion))
            .ForMember(dest => dest.ImagenesDetalle, opt => opt.MapFrom(src => src.ImagenesDetalle))
            .ForMember(dest => dest.Caracteristicas, opt => opt.MapFrom(src => src.Caracteristicas));
        CreateMap<ImagenCommand, Imagen>();
        CreateMap<CaracteristicaCommand, Caracteristica>();
        CreateMap<ParametroPersonalizacionCommand, ParametrosPersonalizacion>();
        CreateMap<ParametroCommand, Parametro>();
        CreateMap<ValorParametroCommand,ValorParametro>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());;

        CreateMap<CreateOrderCommand, Order>();
        CreateMap<OrderParameterCommand, OrderParameter>();
    }
}