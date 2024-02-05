using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
	public static class WatchHubContextSeed
	{
		public static async Task SeedAsync(WatchHubContext db)
		{
			await db.Database.MigrateAsync();

			if (await db.Categories.AnyAsync() || await db.Brands.AnyAsync() || await db.Products.AnyAsync())
				return;

				var c1 = new Category() { Name = "Mens" };
				var c2 = new Category() { Name = "Ladies" };
				var c3 = new Category() { Name = "Smart Watches" };

				var b1 = new Brand() { Name = "Fossil" };
				var b2 = new Brand() { Name = "Casio" };
				var b3 = new Brand() { Name = "Tissot" };
				var b4 = new Brand() { Name = "Citizen" };

				var p1 = new Product() { Category = c1, Brand = b1, PictureUri = "01.jpg", Price = 211.35m, Name = "Gents Fossil Blue Watch FS6013" };
				var p2 = new Product() { Category = c1, Brand = b1, PictureUri = "02.jpg", Price = 186.34m, Name = "Gents Fossil Watches Incription Watch" };
				var p3 = new Product() { Category = c1, Brand = b1, PictureUri = "03.jpg", Price = 198.85m, Name = "Gents Fossil Watches Neutra Watch" };
				var p4 = new Product() { Category = c2, Brand = b1, PictureUri = "04.jpg", Price = 186.34m, Name = "Ladies Fossil Raquel Watch ES5303" };
				var p5 = new Product() { Category = c2, Brand = b1, PictureUri = "05.jpg", Price = 248.87m, Name = "Ladies Fossil Raquel Watch ES5304" };
				var p6 = new Product() { Category = c3, Brand = b1, PictureUri = "06.jpg", Price = 348.92m, Name = "Fossil Gen 6 Ombre Bluetooth 5 Smartwatch FTW4061" };
				var p7 = new Product() { Category = c1, Brand = b2, PictureUri = "07.jpg", Price = 211.35m, Name = "Mens Casio Sapphire Crystal 710 Series Chronograph Watch" };
				var p8 = new Product() { Category = c2, Brand = b2, PictureUri = "08.jpg", Price = 137.57m, Name = "Casio 'G-Shock' Pink Plastic/Resin Quartz Watch" };
				var p9 = new Product() { Category = c3, Brand = b3, PictureUri = "09.jpg", Price = 1183.01m, Name = "Mens Tissot Titanium Solar Powered Bluetooth Smartwatch" };
				var p10 = new Product() { Category = c2, Brand = b3, PictureUri = "10.jpg", Price = 315.09m, Name = "Ladies Tissot Classic Dream Small Watch" };
				var p11 = new Product() { Category = c1, Brand = b4, PictureUri = "11.jpg", Price = 373.93m, Name = "Mens Citizen Tsuyosa Automatic Watch" };
				var p12 = new Product() { Category = c2, Brand = b4, PictureUri = "12.jpg", Price = 211.35m, Name = "Citizen Eco-Drive Crystal Case Ladies Watch Black" };

				db.AddRange(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
				await db.SaveChangesAsync();
		}
    }
}
