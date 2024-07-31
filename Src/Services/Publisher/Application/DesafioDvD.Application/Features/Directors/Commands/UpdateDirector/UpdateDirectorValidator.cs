using DesafioDvD.Core.ValidationMessages;
using DesafioDvD.Domain.Entities;
using FluentValidation;

namespace DesafioDvD.Application.Features.Directors.Commands.UpdateDirector
{
    public class UpdateDirectorValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage(ValidationMessages.ERROR_MESSAGE);
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE)
                .MinimumLength(Director.MIN_LENGTH).WithMessage(ValidationMessages.MIN_LENGTH_ERROR_MESSAGE);
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE)
                .MinimumLength(Director.MIN_LENGTH).WithMessage(ValidationMessages.MIN_LENGTH_ERROR_MESSAGE);
         
        }
    }
}
