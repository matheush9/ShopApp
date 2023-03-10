using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Store;
using ShopApp.Services.GenericService;

namespace ShopApp.Services.StoreServices
{
    public class StoreService : IGenericService<GetStoreResponseDto, AddStoreRequestDto>
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

            return _mapper.Map<GetStoreResponseDto>(store); ;
        }

        public async Task<List<GetStoreResponseDto>> GetAll()
        {
            return await _context.Stores.Select(p => _mapper.Map<GetStoreResponseDto>(p)).ToListAsync();
        }   

        public async Task Add(AddStoreRequestDto newStore)
        {
            _context.Stores.Add(_mapper.Map<Store>(newStore));            
            await _context.SaveChangesAsync();
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
                store.ImageUrl = newStore.ImageUrl;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetStoreResponseDto>(store);
        }
    }
}
