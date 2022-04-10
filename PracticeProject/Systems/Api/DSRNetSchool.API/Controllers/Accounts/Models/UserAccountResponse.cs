namespace DSRNetSchool.API.Controllers.Accounts.Models;

using AutoMapper;
using DSRNetSchool.UserAccount;

public class UserAccountResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserAccountResponseProfile : Profile
{
    public UserAccountResponseProfile()
    {
        CreateMap<UserAccountModel, UserAccountResponse>();
    }
}
