using AutoMapper;
using WebApi.Models;

namespace WebApi.Services.Account;

public class ProfileService : Profile
{
    public ProfileService()
    {
        CreateMap<User, UserViewModel>();
    }
}