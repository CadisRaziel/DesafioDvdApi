
using DesafioDvD.Query.Application.Contracts;
using DesafioDvD.Query.Domain.Models;
using MediatR;

namespace DesafioDvD.Query.Application.Features.Directors.Commands.CreateDirector
{  
        public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, bool>
        {
            private readonly IDirectorsQueryRepository _repository;
            private readonly CreateDirectorCommandValidator _validator;

            public CreateDirectorCommandHandler(
                IDirectorsQueryRepository repository,
                CreateDirectorCommandValidator validator)
            {
                _repository = repository;
                _validator = validator;
            }

            public async Task<bool> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                    return false;

                var director = await _repository.Get(request.Id); //Verifico se ja existe esse Id, caso exista eu retorno falso
                if (director is not null)
                    return false;

                //Se o diretor nao existe, criamos um novo
                director = new Director { Id = request.Id, FullName = request.FullName, CreatedAt = request.CreatedAt, UpdatedAt = request.UpdatedAt };

                var result = await _repository.Create(director);
                if (result is null)
                    return false;

                return true;
            }
        }
    }

