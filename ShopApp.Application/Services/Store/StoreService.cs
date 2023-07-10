using AutoMapper;
using ShopApp.Application.Interfaces.Store;
using ShopApp.Domain.DTOs.Store;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Application.Services.StoreServices
{
    public class StoreService : IStoreService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;

        public StoreService(IMapper mapper, IStoreRepository storeRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
        }

        public async Task<GetStoreResponseDto> GetById(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);

            return _mapper.Map<GetStoreResponseDto>(store);
        }

        public async Task<List<GetStoreResponseDto>> GetAll()
        {
            var stores = await _storeRepository.GetAll();
            return _mapper.Map<List<GetStoreResponseDto>>(stores);
        }
        
        public async Task<GetStoreResponseDto> GetStoreByUserId(int userId)
        {
            var store =  await _storeRepository.GetStoreByUserId(userId);
            return _mapper.Map<GetStoreResponseDto>(store);
        }

        public async Task<GetStoreResponseDto> Add(AddStoreRequestDto newStore)
        {
            var store = _mapper.Map<Store>(newStore);
            await _storeRepository.AddAsync(store);

            return _mapper.Map<GetStoreResponseDto>(store);
        }

        public async Task<GetStoreResponseDto> Delete(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);

            if (store != null)
            {
                await _storeRepository.DeleteAsync(store);
            }

            return _mapper.Map<GetStoreResponseDto>(store);
        }

        public async Task<GetStoreResponseDto> Update(int id, AddStoreRequestDto newStore)
        {
            var store = await _storeRepository.GetByIdAsync(id);

            if (store != null)
            {
                store.Name = newStore.Name;
                store.Description = newStore.Description;
                store.Country = newStore.Country;

                await _storeRepository.UpdateAsync(store);
            }

            return _mapper.Map<GetStoreResponseDto>(store);
        }
    }
}
