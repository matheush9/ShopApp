using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Interfaces.User;
using ShopApp.Application.Services.PasswordHasher;
using ShopApp.Application.Services.Token;
using ShopApp.Domain.DTOs.JwtToken;
using ShopApp.Domain.DTOs.User;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;

namespace ShopApp.Application.Services.UserServices
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
            var user = await _context.Users
                .Include(i => i.Images)
                .Include(s => s.Store)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(u => u.Id == id);

            return _mapper.Map<GetUserResponseDto>(user);
        }

        public async Task<GetUserResponseDto> AddUser(AddUserRequestDto newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.Password = PasswordHasherService.HashPassword(newUser.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetUserResponseDto>(user);
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

        public async Task<GetUserResponseDto> UpdateUser(int id, EditUserRequestDto newUser)
        {
            var user = await _context.Users
                .Include(i => i.Images)
                .Include(s => s.Store)
                .Include(c => c.Customer)
                .FirstAsync(u => u.Id == id);

            if (user != null)
            {
                user.Name = newUser.Name;
                user.Email = newUser.Email;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetUserResponseDto>(user);
        }

        public async Task<JwtTokenResponseDto> Authenticate(AddUserRequestDto userRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userRequest.Email);

            if (user is not null && PasswordHasherService.VerifyPasswordMatching(userRequest.Password, user.Password))
                return TokenService.GenerateToken(user);

            return new JwtTokenResponseDto();
        }
    }
}
