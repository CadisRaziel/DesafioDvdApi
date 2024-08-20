
using DesafioDvD.Core.ValidationMessages;
using FluentValidation;

namespace DesafioDvD.Query.Application.Features.Dvds.Commands.CreateDvd
{
    public class CreateDvdCommandValidator : AbstractValidator<CreateDvdCommand>
    {
        public CreateDvdCommandValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE);

            RuleFor(d => d.Title)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE);

            RuleFor(d => d.Published)
                .NotEmpty().WithMessage(ValidationMessages.ERROR_MESSAGE)
                .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);

            RuleFor(d => d.Genre)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE);

            //Equal(true) -> Nao posso criar um dvd que o available ja ta setado como falso(se tam falso em teoria da excluido)
            RuleFor(d => d.Available)
                .Equal(true).WithMessage(ValidationMessages.ERROR_MESSAGE);

            //GreaterThan(-1) -> precisa ser maior que 0
            RuleFor(d => d.Copies)
                .GreaterThan(-1).WithMessage(ValidationMessages.ERROR_MESSAGE);

            RuleFor(d => d.CreatedAt)
                .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);

            RuleFor(d => d.UpdatedAt)
                .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);

            RuleFor(d => d.DirectorId)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE);
        }
    }
}
