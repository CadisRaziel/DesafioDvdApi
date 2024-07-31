using DesafioDvD.Application.Contracts;
using DesafioDvD.Domain.Entities;
using MediatR;

namespace DesafioDvD.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, CreateDirectorResponse> //-> Obs: nosso objeto de resposta nao precisa ser necessariamente um record, poderiamos retornar um int ID ou um bool
    {
        private readonly IDirectorsWriteRepository _repository;
        private readonly CreateDirectorCommandValidator _validator;

        public CreateDirectorCommandHandler(IDirectorsWriteRepository repository, CreateDirectorCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CreateDirectorResponse> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            //Primeiro validamos nossa requisicao(vai passar por todas regras criadas dentro do `CreateDirectorCommandValidator`
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return default; //-> Se nao for valido eu retorno um defaul que e o estado primordial(null) do objeto (Preferivel retornar default do que null, por ser mais elegante, porem poderiamos retornar null sem problema)

            var director = new Director(request.Name, request.Surname); //-> Lembrando que dentro do Director eu tenho varios metodos no construtor que serao validados antes
            var result = await _repository.Create(director); //-> Vai retornar um bool

            //Validamos se foi salvo no banco, caso nao foi retorna um default
            if (!result)
                return default;


            return new CreateDirectorResponse(director.Id.ToString(), director.FullName(), director.CreatedAt, director.UpdatedAt);
        }
    }
}


/*
 IRequestHandler<CreateDirectorCommand, CreateDirectorResponse>
 - 1 parametro CreateDirectorCommand -> Manipulador da requisicao `CreateDirectorCommand`
 - 2 parametro CreateDirectorResponse -> E a resposta do `CreateDirectorCommand` sera o `CreateDirectorResponse` 
 */