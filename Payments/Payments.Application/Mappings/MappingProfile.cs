using AutoMapper;
using Payments.Application.DTOs.Payment;
using Payments.Application.DTOs.Transaction;

namespace Payments.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Payment mappings
        CreateMap<Domain.Entities.Payment, PaymentDto>();
        CreateMap<CreatePaymentDto, Domain.Entities.Payment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Domain.Entities.Enums.PaymentStatus.Pending))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        // Transaction mappings
        CreateMap<Domain.Entities.Transaction, TransactionDto>();
        CreateMap<CreateTransactionDto, Domain.Entities.Transaction>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Payment, opt => opt.Ignore());
    }
}
