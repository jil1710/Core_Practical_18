using Microsoft.EntityFrameworkCore;
using Practical_18_API.DatabaseContext;

namespace Practical_18_API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddDbContextPool<ApplicationDb>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
			});

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				//app.UseSwagger();
				//app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.MapControllers();

			app.Run();
		}
	}
}