using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Item;

namespace WebAPITextRPG.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ItemService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> AddItem(AddItemDto newItem) //adding item method
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>(); //serviceResponse variable
            var item = _mapper.Map<Item>(newItem); //Item variable

            _context.Items.Add(item); //creating a new item
            await _context.SaveChangesAsync(); //writing changes to database and generating new ID for item
            serviceResponse.Data =
                await _context.Items.Select(c => _mapper.Map<GetItemDto>(c)).ToListAsync();
            return serviceResponse; //sending the response to controller
        }

        public async Task<ServiceResponse<List<GetItemDto>>> DeleteItem(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>();
            try //whenever we try to delete an item that doesn't exist we catch an exception and display a massage
            {
                var item = await _context.Items.FirstOrDefaultAsync(c => c.Id == id); //choosing the item by id
                if (item is null) //checking if item doesn't exist 
                    throw new Exception($"item with Id '{id}' not found."); //throwing an exception with a custom message

                _context.Items.Remove(item); //deleting the item

                await _context.SaveChangesAsync(); //writing changes to database

                serviceResponse.Data = await _context.Items.Select(c => _mapper.Map<GetItemDto>(c)).ToListAsync();
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
            var dbItems = await _context.Items.ToListAsync(); //getting all items from database
            serviceResponse.Data = dbItems.Select(c => _mapper.Map<GetItemDto>(c)).ToList(); //mapping response to DTO
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<GetItemDto>> GetItemById(int id) //Returning a single item by id
        {
            var serviceResponse = new ServiceResponse<GetItemDto>();
            var dbItem = await _context.Items.FirstOrDefaultAsync(c => c.Id == id); //getting item from database
            serviceResponse.Data = _mapper.Map<GetItemDto>(dbItem); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetItemDto>> UpdateItem(UpdateItemDto updatedItem)
        {

            var serviceResponse = new ServiceResponse<GetItemDto>();
            try //whenever we try to update an item that doesn't exist we catch an exception and display a massage
            {
                var item =
                    await _context.Items.FirstOrDefaultAsync(c => c.Id == updatedItem.Id); //choosing the item by id
                if (item is null) // checking if item doesn't exist
                    throw new Exception($"Item with Id '{updatedItem.Id}' not found."); //throwing an exception with a custom message

                //values that are allowed to be updated
                item.Name = updatedItem.Name;
                item.Damage = updatedItem.Damage;
                item.Type = updatedItem.Type;

                await _context.SaveChangesAsync(); //writing changes to database
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
