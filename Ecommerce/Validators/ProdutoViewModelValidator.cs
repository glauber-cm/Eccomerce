using Ecommerce.Web.ViewModels;
using FluentValidation;

namespace Ecommerce.Web.Validators
{
    public class ProdutoViewModelValidator : AbstractValidator<ProdutoViewModel>
    {
        public ProdutoViewModelValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Informe o nome!");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Informe a descrição!");

            RuleFor(x => x.Preco)
                .GreaterThan(0)
                .WithMessage("O Preço deve ser maior que zero!");

            RuleFor(x => x.Estoque)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Estoque inválido!");
        }
    }
}
