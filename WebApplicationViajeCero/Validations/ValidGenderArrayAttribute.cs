using System.ComponentModel.DataAnnotations;

namespace WebApplicationViajeCero.Validations
{
    public class ValidGenderArrayAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var genderArray = value as char[];

            if (genderArray == null || genderArray.Length == 0)
                return ValidationResult.Success; // nullable or empty is OK

            if (genderArray.Length > 2)
                return new ValidationResult("Solo se permiten hasta dos caracteres.");

            if (genderArray.Any(g => g != 'f' && g != 'm'))
                return new ValidationResult("El sexo debe ser solo 'f' o 'm'.");

            return ValidationResult.Success;
        }
    }

}
