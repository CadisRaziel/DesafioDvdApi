using DesafioDvD.Query.Application.Contracts;
using MediatR;

namespace DesafioDvD.Query.Application.Features.Directors.Commands.DeleteDirector
{
  
        public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand, bool>
        {
            private readonly IDirectorsQueryRepository _repository;

            public DeleteDirectorCommandHandler(IDirectorsQueryRepository repository)
            {
                _repository = repository;
            }

            public async Task<bool> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.Id)) //Validacao para id nullo ou vazio
                    return false;

                var director = await _repository.Get(request.Id); //Verifica se o diretor existe
                if (director is null)
                    return false;

                return await _repository.Delete(request.Id); //E parecido com o EF porem e do mongoDb
            }
        }
    }

