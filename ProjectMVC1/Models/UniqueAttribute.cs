using System.ComponentModel.DataAnnotations;

namespace ProjectMVC1.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            
            var dbContext = validationContext.GetService<MVCDbContext>();

            
            var existingCourse = dbContext?.Courses
                .FirstOrDefault(c => c.Name == value.ToString());

            if (existingCourse != null)
            {
                return new ValidationResult("This course name already exists.");
            }

            return ValidationResult.Success;
        }
    }
}
