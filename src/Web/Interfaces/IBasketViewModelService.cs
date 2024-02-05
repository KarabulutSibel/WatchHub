using Web.Models;

namespace Web.Interfaces
{
	public interface IBasketViewModelService
	{
		Task<BasketViewModel> GetBasketViewModelAsync();
		Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity);
		Task EmptyBasketAsync();
		Task RemoveItemAsync(int productId);
		Task<BasketViewModel> UpdateQuantities(Dictionary<int, int> quantities);
		Task TransferBasketAsync();
		Task CheckoutAsync(string street, string city, string? state, string country, string zipCode);
	}
}
