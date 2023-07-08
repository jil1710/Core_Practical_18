using Microsoft.EntityFrameworkCore;
using Practical_18_API.Enum;
using Practical_18_API.Models;

namespace Practical_18_API.DatabaseContext
{
	public class ApplicationDb : DbContext
	{
		public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options)
		{

		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Student>().HasData( 
				new Student()
				{
					Id = 1,
					Email = "jil@gmail.com",
					Gender = Gender.Male,
					Name = "Jil",
					PhoneNumber = "1234567890",
				},
				new Student()
				{
					Id = 2,
					Email = "janvi@gmail.com",
					Gender = Gender.Female,
					Name = "Janvi",
					PhoneNumber = "1234567890",
				}
			);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> students { get; set; }

	}
}
