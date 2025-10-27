using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    // DTO para listagem e detalhes de categoriapublic 
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int ProductCount { get; set; }// Quantidade de produtos
    }

    // DTO para categoria com produtospublic 
    public class CategoryWithProductsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }

    // DTO para criação de categoriapublic 
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        public string Description { get; set; } = string.Empty;
    }

    // DTO para atualização de categoriapublic 
    public class UpdateCategoryDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        public string Description { get; set; } = string.Empty;
    }
}
