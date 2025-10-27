using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    // DTO para listagem e detalhes de produtopublic 
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Informações da categoria (sem trazer todos os produtos)
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    // DTO para criação de produtopublic 
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, 1000000, ErrorMessage = "O preço deve estar entre 0.01 e 1.000.000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo")]
        public int Stock { get; set; }

        [Url(ErrorMessage = "URL da imagem inválida")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "A categoria é obrigatória")]
        public int CategoryId { get; set; }
    }

    // DTO para atualização de produtopublic 
    public class UpdateProductDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, 1000000, ErrorMessage = "O preço deve estar entre 0.01 e 1.000.000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo")]
        public int Stock { get; set; }

        [Url(ErrorMessage = "URL da imagem inválida")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "A categoria é obrigatória")]
        public int CategoryId { get; set; }
    }
}
