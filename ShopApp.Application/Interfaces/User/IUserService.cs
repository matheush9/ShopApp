using ShopApp.Domain.DTOs.JwtToken;
using ShopApp.Domain.DTOs.User;

namespace ShopApp.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<GetUserResponseDto> GetUserById(int id);
        Task<GetUserResponseDto> AddUser(AddUserRequestDto newUser);
        Task<GetUserResponseDto> DeleteUser(int id);
        Task<GetUserResponseDto> UpdateUser(int id, EditUserRequestDto newUser);
        Task<JwtTokenResponseDto> Authenticate(AddUserRequestDto userRequest);
    }
}
