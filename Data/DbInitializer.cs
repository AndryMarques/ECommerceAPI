using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ECommerceDbContext context)
        {
            // Verifica se já existe dados
            if (context.Categories.Any())
            {
                return;// Banco já foi populado
            }

            // Criar Categorias
            var categories = new Category[]
            {
                new Category
                {
                    Name = "Eletrônicos",
                    Description = "Produtos eletrônicos e gadgets"
                },
                new Category
                {
                    Name = "Livros",
                    Description = "Livros físicos e digitais"
                },
                new Category
                {
                    Name = "Roupas",
                    Description = "Vestuário masculino e feminino"
                },
                new Category
                {
                    Name = "Casa e Decoração",
                    Description = "Itens para casa e decoração"
                }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Criar Produtos
            var products = new Product[]
            {
                // Eletrônicos
                new Product
                {
                    Name = "Smartphone XYZ",
                    Description = "Smartphone com 128GB de armazenamento",
                    Price = 1999.99M,
                    Stock = 50,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Smartphone",
                    CategoryId = categories[0].Id
                },
                new Product
                {
                    Name = "Notebook Pro",
                    Description = "Notebook i7, 16GB RAM, 512GB SSD",
                    Price = 4599.00M,
                    Stock = 30,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Notebook",
                    CategoryId = categories[0].Id
                },
                new Product
                {
                    Name = "Fone Bluetooth",
                    Description = "Fone de ouvido wireless com cancelamento de ruído",
                    Price = 299.90M,
                    Stock = 100,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Fone",
                    CategoryId = categories[0].Id
                },

                // Livros
                new Product
                {
                    Name = "Clean Code",
                    Description = "Livro sobre código limpo por Robert Martin",
                    Price = 89.90M,
                    Stock = 75,
                    ImageUrl = "https://via.placeholder.com/300x300?text=CleanCode",
                    CategoryId = categories[1].Id
                },
                new Product
                {
                    Name = "Design Patterns",
                    Description = "Padrões de projeto em programação",
                    Price = 95.00M,
                    Stock = 60,
                    ImageUrl = "https://via.placeholder.com/300x300?text=DesignPatterns",
                    CategoryId = categories[1].Id
                },

                // Roupas
                new Product
                {
                    Name = "Camiseta Básica",
                    Description = "Camiseta 100% algodão",
                    Price = 49.90M,
                    Stock = 200,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Camiseta",
                    CategoryId = categories[2].Id
                },
                new Product
                {
                    Name = "Calça Jeans",
                    Description = "Calça jeans slim fit",
                    Price = 159.90M,
                    Stock = 150,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Jeans",
                    CategoryId = categories[2].Id
                },

                // Casa e Decoração
                new Product
                {
                    Name = "Luminária LED",
                    Description = "Luminária de mesa com controle de intensidade",
                    Price = 129.90M,
                    Stock = 80,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Luminaria",
                    CategoryId = categories[3].Id
                },
                new Product
                {
                    Name = "Quadro Decorativo",
                    Description = "Quadro abstrato 60x40cm",
                    Price = 79.90M,
                    Stock = 45,
                    ImageUrl = "https://via.placeholder.com/300x300?text=Quadro",
                    CategoryId = categories[3].Id
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            // Criar usuário de teste
            var user = new User
            {
                Name = "João Silva",
                Email = "joao@exemplo.com",
                PasswordHash = "hash_temporario_123",// No módulo 3 faremos hash real
                Phone = "(11) 98765-4321"
            };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}