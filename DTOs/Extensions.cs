using CatalogService.Entities;
using static CatalogService.DTOs.DTOs;

namespace CatalogService.DTOs
{
    public static class Extensions
    {
        public static ItemDTO AsDTO(this Item item)
        {
            return new ItemDTO(
                item.Id,
                item.Name,
                item.Description,
                item.Price,
                item.CreatedDate);
        }
    }
}
