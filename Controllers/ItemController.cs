using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPITextRPG.Dtos.Item;
using WebAPITextRPG.Services.ItemService;

namespace WebAPITextRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("GetAll")] //Get method returning a list of all items
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> Get()
        {
            return Ok(await _itemService.GetAllItems());
        }

        [HttpGet("{id}")] //Get method returnig a single item using the id parameter
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> GetSingle(int id)
        {
            return Ok(await _itemService.GetItemById(id)); //Returns the first item where the id of the items equals the given ID
        }

        [HttpPost] //POST method for creating a new item
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> AddItem(AddItemDto newItem)
        {
            return Ok(await _itemService.AddItem(newItem));
        }

        [HttpPut] //PUT method for updating a item
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> UpdateItem(UpdateItemDto updatedItem)
        {
            var response = await _itemService.UpdateItem(updatedItem);
            if (response.Data is null) //if item was not found return response as notfound (404)
            {
                return NotFound(response);
            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> DeleteItemr(int id)
        {
            var response = await _itemService.DeleteItem(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}