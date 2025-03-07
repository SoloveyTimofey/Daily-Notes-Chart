using AutoMapper;
using DailyNotesChart.Application.Operations.Account.Commands;
using DailyNotesChart.Application.Operations.Account.Queries;
using DailyNotesChart.WebApi.Requests.Account;

namespace DailyNotesChart.WebApi.Profiles;

public class AccountRequestsToCommandsProfile : Profile
{
    public AccountRequestsToCommandsProfile()
    {
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<LoginByEmailRequest, LoginByEmailQuery>();
        CreateMap<LoginByUserNameRequest, LoginByUserNameQuery>();
        CreateMap<LoginWithRefreshTokenRequest, LoginWithRefreshTokenQuery>();
    }
}