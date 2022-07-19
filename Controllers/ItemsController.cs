using CatalogService.Entities;
using CatalogService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CatalogService.DTOs.DTOs;

namespace CatalogService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> GetAsync()
        {
            var items = await itemsRepository.GetAllAsync();
            return items.Select(item => item.AsDTO());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetByIdAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);

            if (item is null)
                return NotFound();

            return item.AsDTO();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto createItemDto)
        {
            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem is null)
                return NotFound();

            existingItem.Name = createItemDto.Name;
            existingItem.Description = createItemDto.Description;
            existingItem.Price = createItemDto.Price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var existingItem = await itemsRepository.GetAsync(id);

            if (existingItem is null)
                return NotFound();

            await itemsRepository.RemoveAsync(id);

            return NoContent();
        }
    }
}
