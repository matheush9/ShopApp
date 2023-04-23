using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.User;
using ShopApp.Services.PasswordHasher;
using ShopApp.Services.TokenServices;
using System.IdentityModel.Tokens.Jwt;

namespace ShopApp.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetUserResponseDto> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return _mapper.Map<GetUserResponseDto>(user);
        }

        public async Task AddUser(AddUserRequestDto newUser)
        {
            newUser.Password = PasswordHasherService.HashPassword(newUser.Password);

            _context.Users.Add(_mapper.Map<User>(newUser));
            await _context.SaveChangesAsync();
        }

        public async Task<GetUserResponseDto> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetUserResponseDto>(user);
        }

        public async Task<GetUserResponseDto> UpdateUser(int id, AddUserRequestDto newUser)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                user.Name = newUser.Name;
                user.Email = newUser.Email;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetUserResponseDto>(user);
        }

        public async Task<JwtToken> Authenticate(AddUserRequestDto userRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userRequest.Email); 

            if (user is not null && PasswordHasherService.VerifyPasswordMatching(userRequest.Password, user.Password))
                return TokenService.GenerateToken(user);

            return new JwtToken();
        }
    }
}
