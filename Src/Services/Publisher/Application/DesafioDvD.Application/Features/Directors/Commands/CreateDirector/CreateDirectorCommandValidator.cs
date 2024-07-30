using DesafioDvD.Core.ValidationMessages;
using DesafioDvD.Domain.Entities;
using FluentValidation;

namespace DesafioDvD.Application.Features.Directors.Commands.CreateDirector
{
    //AbstractValidator -> Classe do FluentValidation
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            //RuleFor -> vem com a herenca do AbstractValidator
            //Abaixo tera as regras dos parametros que estao no CreateDirectorCommand
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE)
                .MinimumLength(Director.MIN_LENGTH).WithMessage(ValidationMessages.MIN_LENGTH_ERROR_MESSAGE)
                .MaximumLength(Director.MAX_LENGTH).WithMessage(ValidationMessages.MAX_LENGTH_ERROR_MESSAGE);  
            
            RuleFor(s => s.Surname)
                .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE)
                .MinimumLength(Director.MIN_LENGTH).WithMessage(ValidationMessages.MIN_LENGTH_ERROR_MESSAGE)
                .MaximumLength(Director.MAX_LENGTH).WithMessage(ValidationMessages.MAX_LENGTH_ERROR_MESSAGE);
        }
    }
}

//NotEmpty().WithMessage -> Se for vazio nos passamos uma mensagem
//MIN_LENGTH_ERROR_MESSAGE - MAX_LENGTH_ERROR_MESSAGE -> ambas tem um {PropertyName} o FluentValidation vai pegar esse `d.Name` e por no lugar do {PropertyName}