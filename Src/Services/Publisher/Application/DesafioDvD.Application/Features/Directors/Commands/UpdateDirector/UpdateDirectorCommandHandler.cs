using DesafioDvD.Application.Contracts;
using MediatR;

namespace DesafioDvD.Application.Features.Directors.Commands.UpdateDirector
{
    public class UpdateDirectorCommandHandler : IRequestHandler<UpdateDirectorCommand, UpdateDirectorResponse>
    {
        private readonly IDirectorsWriteRepository _repository;
        private readonly UpdateDirectorValidator _validator;

        public UpdateDirectorCommandHandler(IDirectorsWriteRepository repository, UpdateDirectorValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<UpdateDirectorResponse> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) //-> Se validationResult nao for valido
                return default;

            var director = await _repository.Get(request.Id); // -> Primeiro buscamos um director Id que exista
            if (director is null)
                return default;

            director.UpdateName(request.Name);
            director.UpdateSurname(request.Surname);

            var result = await _repository.Update(director); //-> Atualizamos o metodo Update

            if (!result) //-> caso nao salvar no banco retorna default
                return default;

            return new UpdateDirectorResponse(director.Id.ToString(), director.FullName(), director.UpdatedAt); //-> retornamo para o usuario o Id, o nome inteiro, e a data de update

        }
    }
}
