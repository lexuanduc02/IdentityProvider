using IP.Domain;
using IP.Model;

namespace IP.Services;

public interface IOauthServices
{
    Task<BaseResponseModel<User>> Login(LoginRequestModel model);
}
