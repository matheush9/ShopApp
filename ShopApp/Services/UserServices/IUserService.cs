using Microsoft.IdentityModel.Tokens;
using ShopApp.Dtos.User;
using System.IdentityModel.Tokens.Jwt;

namespace ShopApp.Services.UserServices
{
    public interface IUserService
    {
        Task<GetUserResponseDto> GetUserById(int id);
        Task AddUser(AddUserRequestDto newUser);
        Task<GetUserResponseDto> DeleteUser(int id);
        Task<GetUserResponseDto> UpdateUser(int id, AddUserRequestDto newUser);
        Task<JwtToken> Authenticate(AddUserRequestDto userRequest);
    }
}
