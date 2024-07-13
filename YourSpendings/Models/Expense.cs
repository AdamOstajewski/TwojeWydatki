namespace YourSpendings.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }


        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}
