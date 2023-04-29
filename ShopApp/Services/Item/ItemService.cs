using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Item;
using ShopApp.Services.Generic;

namespace ShopApp.Services.ItemService
{
    public class ItemService : IGenericService<GetItemResponseDto, AddItemRequestDto>
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
            
        public async Task Add(AddItemRequestDto newItem)
        {
            _context.Items.Add(_mapper.Map<Item>(newItem));
            await _context.SaveChangesAsync();
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
