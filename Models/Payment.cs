namespace ECommerceAPI.Models
{
    public class Payment
    {

        public int Id { get; set; }
        public int OrderId { get; set; }
        public enum PaymentMethod
        {
            CreditCard,
            DebitCard,
            PayPal,
            BankTransfer
        }
        public decimal Amount { get; set; }
        public bool Status { get; set; }


    }
}
