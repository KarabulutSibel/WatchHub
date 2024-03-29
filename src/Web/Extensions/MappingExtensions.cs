﻿using ApplicationCore.Entities;
using Web.Models;

namespace Web.Extensions
{
	public static class MappingExtensions
	{
		public static BasketViewModel ToBasketViewModel(this Basket basket)
		{
			return new BasketViewModel()
			{
				Id = basket.Id,
				BuyerId = basket.BuyerId,
				Items = basket.Items.Select(x => new BasketItemViewModel()
				{
					Id = x.Id,
					ProductId = x.ProductId,
					ProductName = x.Product.Name,
					PictureUri = x.Product.PictureUri ?? "noimage.jpg",
					Quantity = x.Quantity,
					UnitPrice = x.Product.Price,
				}).ToList()
			};
		}
	}
}
