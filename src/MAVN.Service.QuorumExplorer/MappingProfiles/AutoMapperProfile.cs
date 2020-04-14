using AutoMapper;
using MAVN.Service.QuorumExplorer.Client.Models;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using Event = MAVN.Service.QuorumExplorer.Client.Models.Event;
using EventParameter = MAVN.Service.QuorumExplorer.Domain.DTOs.EventParameter;
using FunctionCall = MAVN.Service.QuorumExplorer.Domain.DTOs.FunctionCall;
using FunctionCallParameter = MAVN.Service.QuorumExplorer.Domain.DTOs.FunctionCallParameter;
using TransactionDetailedInfo = MAVN.Service.QuorumExplorer.Domain.DTOs.TransactionDetailedInfo;
using TransactionLog = MAVN.Service.QuorumExplorer.Domain.DTOs.TransactionLog;

namespace MAVN.Service.QuorumExplorer.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TransactionInfo, Transaction>();
            CreateMap<TransactionsResult, PaginatedTransactionsResponse>();
            CreateMap<EventParameter, Client.Models.EventParameter>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ParameterName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ParameterType))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.ParameterOrder))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ParameterValue));
            CreateMap<EventInfo, Event>();
            CreateMap<EventsResult, PaginatedEventsResponse>();
            CreateMap<TransactionLog, Client.Models.TransactionLog>();
            CreateMap<FunctionCall, Client.Models.FunctionCall>();
            CreateMap<FunctionCallParameter, Client.Models.FunctionCallParameter>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ParameterName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ParameterType))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.ParameterOrder))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ParameterValue));
            CreateMap<TransactionDetailedInfo, Client.Models.TransactionDetailedInfo>();
        }
    }
}
