namespace ECommerceAPI.Models
{
    public enum OrderStatus
    {
        Pending,// Pendente
        Processing,// Processando
        Shipped,// Enviado
        Delivered,// Entregue
        Cancelled// Cancelado
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }

        // Relacionamento: Pedido pertence a um usuário
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Relacionamento: Um pedido tem vários itens
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}