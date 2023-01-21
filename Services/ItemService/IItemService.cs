using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Item;

namespace WebAPITextRPG.Services.ItemService
{
    public interface IItemService
    {
        Task<ServiceResponse<List<GetItemDto>>> GetAllItems();
        Task<ServiceResponse<GetItemDto>> GetItemById(int id);
        Task<ServiceResponse<List<GetItemDto>>> AddItem(AddItemDto newItem);
        Task<ServiceResponse<GetItemDto>> UpdateItem(UpdateItemDto updatedItem);
        Task<ServiceResponse<List<GetItemDto>>> DeleteItem(int id);

    }
}
