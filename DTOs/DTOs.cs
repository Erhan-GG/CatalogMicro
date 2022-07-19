using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.DTOs
{
    public class DTOs
    {
        public record ItemDTO(
            Guid id,
            string Name,
            string Description,
            decimal Price,
            DateTimeOffset CreatedDate);

        public record CreateItemDto(
            [Required] string Name,
            string Description,
            [Range(0,1000)]decimal Price);
        public record UpdateItemDto(
            [Required] string Name,
            string Description,
            [Range(0, 1000)] decimal Price);
    }
}
