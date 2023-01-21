using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Item;

namespace WebAPITextRPG.Services.ItemService
{
    public class ItemService : IItemService
    {
        private static List<Item> items = new List<Item> //creating a list of items
        {
            new Item(), //adding a default item to the list
            new Item { Id = 1, Name = "Shortbow", Damage = 5, Type = ItemType.Bow} //adding a item named "Shortbow" with an id = 1, damage = 5 and type = Bow
        };
        private readonly IMapper _mapper;

        public ItemService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> AddItem(AddItemDto newItem) //adding item method
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>(); //serviceResponse variable
            var item = _mapper.Map<Item>(newItem); //Item variable
            item.Id = items.Max(c => c.Id) + 1; //finding the max value of id and increasing it by one whenever a new item is added
            items.Add(item); //creating a new character
            serviceResponse.Data = items.Select(c => _mapper.Map<GetItemDto>(c)).ToList(); //mapping response to DTO
            return serviceResponse; //sending the response to controller
        }

        public async Task<ServiceResponse<List<GetItemDto>>> DeleteItem(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>();
            try //whenever we try to delete an item that doesn't exist we catch an exception and display a massage
            {
                var character = items.FirstOrDefault(c => c.Id == id);
                if (character is null) //checking if item doesn't exist 
                    throw new Exception($"item with Id '{id}' not found."); //throwing an exception with a custom message

                items.Remove(character); //deleting the item

                serviceResponse.Data = items.Select(c => _mapper.Map<GetItemDto>(c)).ToList(); //mapping response to DTO
            }
            catch (Exception ex) //contents of exception
            {
                serviceResponse.Success = false; //prompt saying signalising operation wasn't a success
                serviceResponse.Message = ex.Message; //exception message
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> GetAllItems() //Returning list of items
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>();
            serviceResponse.Data = items.Select(c => _mapper.Map<GetItemDto>(c)).ToList(); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetItemDto>> GetItemById(int id) //Returning a single item by id
        {
            var serviceResponse = new ServiceResponse<GetItemDto>();
            var character = items.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetItemDto>(character); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetItemDto>> UpdateItem(UpdateItemDto updatedItem)
        {

            var serviceResponse = new ServiceResponse<GetItemDto>();
            try //whenever we try to update an item that doesn't exist we catch an exception and display a massage
            {
                var item = items.FirstOrDefault(c => c.Id == updatedItem.Id);
                if (item is null) //checking if item doesn't exist and throwing an exception with a custom message
                    throw new Exception($"Item with Id '{updatedItem.Id}' not found.");

                _mapper.Map(updatedItem, item); //mapping updated item to item

                //values that are allowed to be updated
                item.Name = updatedItem.Name;
                item.Damage = updatedItem.Damage;
                item.Type = updatedItem.Type;

                serviceResponse.Data = _mapper.Map<GetItemDto>(item); //mapping response to DTO
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
