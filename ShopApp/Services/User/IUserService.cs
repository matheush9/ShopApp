using ShopApp.Dtos.User;

namespace ShopApp.Services.UserService
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
