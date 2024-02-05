namespace Web.Models
{
	public class BasketViewModel
	{
		public int Id { get; set; }
		public string BuyerId { get; set; }
		public List<BasketItemViewModel> Items { get; set; } = new();
        public int TotalItems => Items.Sum(x => x.Quantity);
		public decimal TotalPrice => Items.Sum(x => x.TotalPrice);


    }
}
