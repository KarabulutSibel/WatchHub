using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public class BasketService : IBasketService

	{ 
		private readonly IRepository<Basket> _basketRepository;
		private readonly IRepository<BasketItem> _basketItemRepository;
		private readonly IRepository<Product> _productRepository;

		public BasketService(IRepository<Basket> basketRepository, IRepository<BasketItem> basketItemRepository, IRepository<Product> productRepository)
		{
			_basketRepository = basketRepository;
			_basketItemRepository = basketItemRepository;
			_productRepository = productRepository;
		}

		public async Task<Basket> AddItemToBasketAsync(string buyerId, int productId, int quantity)
		{
			var basket = await GetOrCreateBasketAsync(buyerId);
			var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == productId);

			if (basketItem != null)
			{
				basketItem.Quantity += quantity;
			}
			else 
			{
				var product = await _productRepository.GetByIdAsync(productId);
				if (product == null)
					throw new ProductNotFoundException(productId);
	
				basketItem = new BasketItem()
				{
					ProductId = productId,
					Quantity = quantity,
					Product = product
				};
				basket.Items.Add(basketItem);
			}

			await _basketRepository.UpdateAsync(basket);
			return basket;
		}

		public async Task DeleteBasketItemAsync(string buyerId, int productId)
		{
			var basket = await GetOrCreateBasketAsync(buyerId);
			var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == productId);
			if (basketItem == null) return;
			await _basketItemRepository.DeleteAsync(basketItem);
		}

		public async Task EmptyBasketAsync(string buyerId)
		{
			var basket = await GetOrCreateBasketAsync(buyerId);
			foreach (var item in basket.Items.ToList())
			{
				await _basketItemRepository.DeleteAsync(item);
			}
		}

		public async Task<Basket> GetOrCreateBasketAsync(string buyerId)
		{
			var specBasket = new BasketWithItemsSpecification(buyerId);
			var basket = await _basketRepository.FirstOrDefaultAsync(specBasket);

			if (basket == null)
			{
				basket = new Basket() { BuyerId = buyerId };
				basket = await _basketRepository.AddAsync(basket);
			}
			return basket;
		}

		public async Task<Basket> SetQuantitiesAsync(string buyerId, Dictionary<int, int> quantities)
		{
			var basket = await GetOrCreateBasketAsync(buyerId);
			foreach (var item in basket.Items)
			{
				if (quantities.ContainsKey(item.ProductId))
				{
					item.Quantity = quantities[item.ProductId];
					await _basketItemRepository.UpdateAsync(item);
				}
			}

			return basket;
		}

		public async Task TransferBasketAsync(string sourceBuyerId, string destinationBuyerId)
		{
			var specSourceBasket = new BasketWithItemsSpecification(sourceBuyerId);
			var sourceBasket = await _basketRepository.FirstOrDefaultAsync(specSourceBasket);
			if (sourceBasket == null) return;

			var destinationBasket = await GetOrCreateBasketAsync(destinationBuyerId);

			foreach (var item in sourceBasket.Items)
			{
				var targetItem = destinationBasket.Items.FirstOrDefault(x => x.ProductId == item.ProductId);
				if (targetItem == null)
				{
					destinationBasket.Items.Add(new BasketItem() 
					{
						ProductId = item.ProductId,
						Quantity = item.Quantity,
					});
				}
				else
				{
					targetItem.Quantity += item.Quantity;
				}
			}

			await _basketRepository.UpdateAsync(destinationBasket);
			await _basketRepository.DeleteAsync(sourceBasket);
		}
	}
}
