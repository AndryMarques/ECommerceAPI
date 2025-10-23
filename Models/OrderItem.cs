namespace ECommerceAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }

        // Relacionamento: Item pertence a um pedido
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        // Relacionamento: Item referencia um produto
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
