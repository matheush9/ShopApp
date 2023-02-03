using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Store;
using ShopApp.Services.GenericService;
using ShopApp.Services.ResponseHandlers;

namespace ShopApp.Services.StoreServices
{
    public class StoreService : IGenericService<GetStoreResponseDto, AddStoreRequestDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DefaultResponseHandler<GetStoreResponseDto> responseHandler = new();

        public StoreService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetStoreResponseDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetStoreResponseDto>();

            var store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetStoreResponseDto>(store);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<List<GetStoreResponseDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetStoreResponseDto>>();
            var responseHandler = new DefaultResponseHandler<List<GetStoreResponseDto>>();
            serviceResponse.Data = await _context.Stores.Select(p => _mapper.Map<GetStoreResponseDto>(p)).ToListAsync();

            return responseHandler.SetResponse(serviceResponse);
        }   

        public async Task<ServiceResponse<GetStoreResponseDto>> Add(AddStoreRequestDto newStore)
        {
            var serviceResponse = new ServiceResponse<GetStoreResponseDto>();

            _context.Stores.Add(_mapper.Map<Store>(newStore));            
            await _context.SaveChangesAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStoreResponseDto>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<GetStoreResponseDto>();
            var store = await _context.Stores.FindAsync(id);

            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetStoreResponseDto>(store);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetStoreResponseDto>> Update(int id, AddStoreRequestDto newStore)
        {
            var serviceResponse = new ServiceResponse<GetStoreResponseDto>();
            var store = await _context.Stores.FindAsync(id);

            if (store != null)
            {
                store.Name = newStore.Name;
                store.Description = newStore.Description;
                store.Country = newStore.Country;
                store.ImageUrl = newStore.ImageUrl;

                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetStoreResponseDto>(store);

            return responseHandler.SetResponse(serviceResponse);
        }
    }
}
