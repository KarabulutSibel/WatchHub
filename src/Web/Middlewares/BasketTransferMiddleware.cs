using Web.Interfaces;

namespace Web.Middlewares
{
	public class BasketTransferMiddleware
	{
		private readonly RequestDelegate _next;

		public BasketTransferMiddleware(RequestDelegate next)
        {
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, IBasketViewModelService basketViewModelService)
		{
			await basketViewModelService.TransferBasketAsync();
			await _next(context);
		}
    }
}
