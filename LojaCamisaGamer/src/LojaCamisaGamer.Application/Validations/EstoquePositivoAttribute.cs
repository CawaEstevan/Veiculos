using System.ComponentModel.DataAnnotations;

namespace LojaCamisaGamer.Application.Validations
{
    public class EstoquePositivoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("O estoque é obrigatório");
            }

            if (value is int estoque)
            {
                if (estoque < 0)
                {
                    return new ValidationResult(
                        ErrorMessage ?? "O estoque não pode ser negativo"
                    );
                }
            }

            return ValidationResult.Success;
        }
    }
}