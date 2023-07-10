using AutoMapper;
using ShopApp.Application.Interfaces.User;
using ShopApp.Application.Services.PasswordHasher;
using ShopApp.Application.Services.Token;
using ShopApp.Domain.DTOs.JwtToken;
using ShopApp.Domain.DTOs.User;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<GetUserResponseDto> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<GetUserResponseDto>(user);
        }

        public async Task<GetUserResponseDto> AddUser(AddUserRequestDto newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.Password = PasswordHasherService.HashPassword(newUser.Password);

            await _userRepository.AddAsync(user);

            return _mapper.Map<GetUserResponseDto>(user);
        }

        public async Task<GetUserResponseDto> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user != null)
                await _userRepository.DeleteAsync(user);

            return _mapper.Map<GetUserResponseDto>(user);
        }

        public async Task<GetUserResponseDto> UpdateUser(int id, EditUserRequestDto newUser)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user != null)
            {
                user.Name = newUser.Name;
                user.Email = newUser.Email;

                await _userRepository.UpdateAsync(user);
            }

            return _mapper.Map<GetUserResponseDto>(user);
        }

        public async Task<JwtTokenResponseDto> Authenticate(AddUserRequestDto userRequest)
        {
            var user = await _userRepository.GetUserByEmail(userRequest.Email);

            if (user is not null && PasswordHasherService.VerifyPasswordMatching(userRequest.Password, user.Password))
                return TokenService.GenerateToken(user);

            return new JwtTokenResponseDto();
        }
    }
}
