using AutoMapper;
using DailyNotesChart.Application.Operations.Account.Commands;
using DailyNotesChart.WebApi.Requests.Account;

namespace DailyNotesChart.WebApi.Profiles;

public class AccountRequestsToCommandsProfile : Profile
{
    public AccountRequestsToCommandsProfile()
    {
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<LoginByEmailRequest, LoginByEmailCommand>();
        CreateMap<LoginByUserNameRequest, LoginByUserNameCommand>();
        CreateMap<LoginWithRefreshTokenRequest, LoginWithRefreshTokenCommand>();
    }
}