namespace DSRNetSchool.WEB;

using System.Threading.Tasks;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
}
