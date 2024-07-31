using DesafioDvD.Application.Contracts;
using MediatR;

namespace DesafioDvD.Application.Features.Directors.Commands.DeleteDirector
{
    public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand, bool>
    {
        private readonly IDirectorsWriteRepository _repository;

        public DeleteDirectorCommandHandler(IDirectorsWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            //um Guid e um valueType por padrao nao pode ser nulo, porem pode ser vazio
            if (request.id == Guid.Empty)
                return false;

            var director = await _repository.GetDirectorWithMovies(request.id);

            if (director is null || director.Dvds.Any(x => x.Available))
                return false;

            return await _repository.Delete(director.Id);
        }
    }
}

/*
 director.Dvds.Any(x => x.Available -> Se os dvd do meu diretor tiver algum(any) que tenha o `Available true` eu nao posso deletar ele
 Porque? - Quando a entidade pai for deletada ela tambem deleta a filha ou seja Director(pai) dvd(filha)
 */