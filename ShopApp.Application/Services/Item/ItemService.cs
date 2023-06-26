using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Interfaces.Item;
using ShopApp.Domain.DTOs.Item;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;

namespace ShopApp.Application.Services.ItemServices
{
    public class ItemService : IItemService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ItemService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetItemResponseDto> GetById(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<GetItemResponseDto>(item);
        }

        public async Task<List<GetItemResponseDto>> GetItemsByOrderId(int id)
        {
            var items = await _context.Items.Where(i => i.OrderId == id).ToListAsync();

            return _mapper.Map<List<GetItemResponseDto>>(items);
        }

        public async Task<GetItemResponseDto> Add(AddItemRequestDto newItem)
        {
            var item = _mapper.Map<Item>(newItem);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetItemResponseDto>(item);
        }

        public async Task<List<GetItemResponseDto>> AddItemsList(List<AddItemRequestDto> itemsList)
        {
            var mappedList = itemsList.Select(_mapper.Map<Item>).ToList();
            _context.Items.AddRange(mappedList);
            await _context.SaveChangesAsync();

            return _mapper.Map<List<GetItemResponseDto>>(mappedList);
        }

        public async Task<GetItemResponseDto> Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetItemResponseDto>(item);
        }

        public async Task<GetItemResponseDto> Update(int id, AddItemRequestDto newItem)
        {
            var item = await _context.Items.FindAsync(id);

            if (item != null)
            {
                item.Quantity = newItem.Quantity;
                item.PriceTotal = newItem.PriceTotal;
                item.ProductId = newItem.ProductId;
                item.OrderId = newItem.OrderId;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetItemResponseDto>(item);
        }
    }
}
