using Practical_18_FrontEnd.Enum;

namespace Practical_18_FrontEnd.Models
{

	public class Student
	{
		public int? Id { get; set; }
		public string Name { get; set; }

		public string PhoneNumber { get; set; }

		public string Email { get; set; }

		public Gender Gender { get; set; }
	}
}
