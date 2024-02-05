using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
	public class HomeViewModelService : IHomeViewModelService
	{
		private readonly IRepository<Product> _productRepo;
		private readonly IRepository<Category> _categoryRepo;
		private readonly IRepository<Brand> _brandRepo;
        public HomeViewModelService(IRepository<Product> productRepo, IRepository<Category> categoryRepo, IRepository<Brand> brandRepo)
        {
            _productRepo = productRepo;
			_categoryRepo = categoryRepo;
			_brandRepo = brandRepo;
        }
        public async Task<HomeViewModel> GetHomeViewModelAsync(int? categoryId, int? brandId, int pageId)
		{
			var specProducts = new ProductFilterSpecification(categoryId, brandId);
			int totalItems = await _productRepo.CountAsync(specProducts);

			var specProductsPaginated = new ProductFilterSpecification(categoryId, brandId, (pageId - 1) * Constants.ITEMS_PER_PAGE, Constants.ITEMS_PER_PAGE);
			var productsPaginated = await _productRepo.GetAllAsync(specProductsPaginated);

			var vm = new HomeViewModel()
			{
				Products = productsPaginated.Select(x => new ProductViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					Price = x.Price,
					PictureUri = x.PictureUri
				}).ToList(),

				Categories = (await _categoryRepo.GetAllAsync()).Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),

				Brands = (await _brandRepo.GetAllAsync()).Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),

				CategoryId = categoryId,
				BrandId = brandId,
				PaginationInfo = new PaginationInfoViewModel()
				{
					PageId = pageId,
					ItemsOnPage = productsPaginated.Count(),
					TotalItems = totalItems
				}
			};

			return vm;
		}
	}
}
