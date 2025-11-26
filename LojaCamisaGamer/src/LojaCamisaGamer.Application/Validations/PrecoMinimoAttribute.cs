using System.ComponentModel.DataAnnotations;

namespace LojaCamisaGamer.Application.Validations
{
    public class PrecoMinimoAttribute : ValidationAttribute
    {
        private readonly decimal _precoMinimo;

        public PrecoMinimoAttribute(double precoMinimo)
        {
            _precoMinimo = (decimal)precoMinimo;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("O preço é obrigatório");
            }

            if (value is decimal preco)
            {
                if (preco < _precoMinimo)
                {
                    return new ValidationResult(
                        ErrorMessage ?? $"O preço deve ser no mínimo R$ {_precoMinimo:F2}"
                    );
                }
            }

            return ValidationResult.Success;
        }
    }
}