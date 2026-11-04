using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Techninxa;
using Techninxa.Hubs;

public class Program
{
	private static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
		builder.Services.AddControllersWithViews();
		builder.Services.AddSignalR();
		EntityFrameworkServiceCollectionExtensions.AddDbContext<ApplicationDbContext>(builder.Services, (Action<DbContextOptionsBuilder>)((DbContextOptionsBuilder options) =>
		{
			SqlServerDbContextOptionsExtensions.UseSqlServer(options, builder.Configuration.GetConnectionString("DefaultConnection"), (Action<SqlServerDbContextOptionsBuilder>)null);
		}), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
		builder.Services.AddSession();
		WebApplication webApplication = builder.Build();
		if (!webApplication.Environment.IsDevelopment())
		{
			webApplication.UseExceptionHandler("/Home/Error");
			webApplication.UseHsts();
		}
		webApplication.UseHttpsRedirection();
		webApplication.UseStaticFiles();
		webApplication.UseRouting();
		webApplication.UseSession();
		webApplication.UseAuthorization();
		webApplication.MapStaticAssets();
		webApplication.MapHub<CompanyChatHub>("/companyChatHub");
		webApplication.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();
		webApplication.Run();
	}
}
