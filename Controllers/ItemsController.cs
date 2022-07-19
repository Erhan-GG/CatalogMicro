using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static CatalogService.DTOs.DTOs;

namespace CatalogService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDTO> items = new()
        {
            new ItemDTO ( Guid.NewGuid(),
                "Potion",
                "Restore a samll amount of HP",
                5,
                DateTimeOffset.UtcNow ),
            new ItemDTO(Guid.NewGuid(),
                "Antidote",
                "Cures poison",
                7,
                DateTimeOffset.UtcNow),
            new ItemDTO(Guid.NewGuid(),
                "Bronze sword",
                "Deals a samll amount of damage",
                20,
                DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ItemDTO> Get()
        {
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDTO> GetById(Guid id)
        {
            var item = items.SingleOrDefault(item => item.id == id);
            
            if (items is null)
                return NotFound();

            return item;
        }

        [HttpPost]
        public ActionResult<ItemDTO> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDTO(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Guid id, UpdateItemDto createItemDto)
        {
            var existingItem = items.Find(item => item.id == id);

            if (items is null)
                return NotFound();

            var updatedItem = existingItem with
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
            };

            var index = items.FindIndex(existingItem => existingItem.id == id);
            items[index] = updatedItem;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.id == id);

            if (index == -1)
                return NotFound();

            items.RemoveAt(index);
            return NoContent();
        }
    }
}
