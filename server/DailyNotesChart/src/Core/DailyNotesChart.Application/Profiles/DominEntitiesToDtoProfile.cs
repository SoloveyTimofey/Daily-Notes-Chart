using AutoMapper;
using DailyNotesChart.Application.DTOs.ChartGroups;
using DailyNotesChart.Domain.Models.ChartGroupAggregate.AggregateRoot;

namespace DailyNotesChart.Application.Profiles;

public sealed class DominEntitiesToDtoProfile : Profile
{
    public DominEntitiesToDtoProfile()
    {
        //CreateMap<ChartGroup, ChartGroupFullReadDto>()
        //    .ForMember(dest => dest.Id)
    }
}