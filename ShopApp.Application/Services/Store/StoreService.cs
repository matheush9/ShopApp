using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Interfaces.Store;
using ShopApp.Domain.DTOs.Store;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;

namespace ShopApp.Application.Services.StoreServices
{
    public class StoreService : IStoreService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StoreService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetStoreResponseDto> GetById(int id)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<GetStoreResponseDto>(store);
        }

        public async Task<List<GetStoreResponseDto>> GetAll()
        {
            return await _context.Stores.Select(p => _mapper.Map<GetStoreResponseDto>(p)).ToListAsync();
        }
        
        public async Task<GetStoreResponseDto> GetStoreByUserId(int userId)
        {
            var store =  await _context.Stores.FirstOrDefaultAsync(s => s.UserId == userId);
            return _mapper.Map<GetStoreResponseDto>(store);
        }

        public async Task<GetStoreResponseDto> Add(AddStoreRequestDto newStore)
        {
            var store = _mapper.Map<Store>(newStore);
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetStoreResponseDto>(store);
        }

        public async Task<GetStoreResponseDto> Delete(int id)
        {
            var store = await _context.Stores.FindAsync(id);

            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetStoreResponseDto>(store);
        }

        public async Task<GetStoreResponseDto> Update(int id, AddStoreRequestDto newStore)
        {
            var store = await _context.Stores.FindAsync(id);

            if (store != null)
            {
                store.Name = newStore.Name;
                store.Description = newStore.Description;
                store.Country = newStore.Country;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetStoreResponseDto>(store);
        }
    }
}
