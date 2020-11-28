using CoreApi.Entities.Master;
using FluentValidation;
using System;

namespace CoreApi.Validations
{
    public class EntregasValidator : AbstractValidator<Entregas>
    {
        public EntregasValidator() : this(string.Empty) { }

        public EntregasValidator(string mensagemFixa)
        {
            RuleFor(e => e.DtEntrega)
                .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage(mensagemFixa + "A data de entrega deve ser preenchida.")
                    .GreaterThan(DateTime.Today)
                    .WithMessage(mensagemFixa + "A data de entrega não pode ser menor ou igual ao dia atual.");
            RuleFor(e => e.NmProduto)
                .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage(mensagemFixa + "O nome do produto deve ser preenchido.")
                    .MaximumLength(50)
                    .WithMessage(mensagemFixa + "O nome do produto deve ter no máximo {MaxLength} caracteres.");
            RuleFor(e => e.QtdProduto)
                .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage(mensagemFixa + "A quantidade deve ser preenchida.")
                    .GreaterThan(0)
                    .WithMessage(mensagemFixa + "A quantidade tem que ser maior que {ComparisonValue}.");
            RuleFor(e => e.VlUnitario)
                .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage(mensagemFixa + "O valor unitário deve ser preenchido.")
                    .GreaterThan(0)
                    .WithMessage(mensagemFixa + "O valor unitário tem que ser maior que {ComparisonValue}.");
        }
    }
}
