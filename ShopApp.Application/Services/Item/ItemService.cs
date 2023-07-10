using AutoMapper;
using ShopApp.Application.Interfaces.Item;
using ShopApp.Domain.DTOs.Item;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Application.Services.ItemServices
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public ItemService(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<GetItemResponseDto> GetById(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            return _mapper.Map<GetItemResponseDto>(item);
        }

        public async Task<List<GetItemResponseDto>> GetItemsByOrderId(int id)
        {
            var items = await _itemRepository.GetItemsByOrderId(id);

            return _mapper.Map<List<GetItemResponseDto>>(items);
        }

        public async Task<GetItemResponseDto> Add(AddItemRequestDto newItem)
        {
            var item = _mapper.Map<Item>(newItem);
            await _itemRepository.AddAsync(item);

            return _mapper.Map<GetItemResponseDto>(item);
        }

        public async Task<List<GetItemResponseDto>> AddItemsList(List<AddItemRequestDto> itemsList)
        {
            var mappedList = itemsList.Select(_mapper.Map<Item>).ToList();
            await _itemRepository.AddItemsList(mappedList);

            return _mapper.Map<List<GetItemResponseDto>>(mappedList);
        }

        public async Task<GetItemResponseDto> Delete(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            if (item != null)
            {
                await _itemRepository.DeleteAsync(item);
            }

            return _mapper.Map<GetItemResponseDto>(item);
        }

        public async Task<GetItemResponseDto> Update(int id, AddItemRequestDto newItem)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            if (item != null)
            {
                item.Quantity = newItem.Quantity;
                item.PriceTotal = newItem.PriceTotal;
                item.ProductId = newItem.ProductId;
                item.OrderId = newItem.OrderId;

                await _itemRepository.UpdateAsync(item);
            }

            return _mapper.Map<GetItemResponseDto>(item);
        }
    }
}
