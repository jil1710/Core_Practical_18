using Practical_18_API.Enum;
using System.ComponentModel.DataAnnotations;

namespace Practical_18_API.Models
{
	public class Student
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50), MinLength(3)]
		public string Name { get; set; }

		[Required]
		[StringLength(10)]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression("^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$", ErrorMessage = "Phone Must be Number")]
		public string PhoneNumber { get; set; }

		[Required]
		[StringLength(250)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[EnumDataType(typeof(Gender), ErrorMessage = "Gender Field Is Required!")]
		public Gender Gender { get; set; }
	}
}
