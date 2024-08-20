using DesafioDvD.Query.Application.Contracts;
using MediatR;

namespace DesafioDvD.Query.Application.Features.Dvds.Commands.DeleteDvd
{
    public class DeleteDvdCommandHandler : IRequestHandler<DeleteDvdCommand, bool>
    {
        private readonly IDvdsQueryRepository _repository;

        public DeleteDvdCommandHandler(IDvdsQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteDvdCommand request, CancellationToken cancellationToken)
        {
            //request.DeletedAt > DateTime.Now -> Pelo rabbitMq alguem pode passar uma data que e maior (ou seja a pessoa pode dizer que foi deletado 3 dias pra frente do dia de hoje
            if (string.IsNullOrEmpty(request.Id) || request.DeletedAt > DateTime.Now)
                return false;

            var dvd = await _repository.Get(request.Id);
            if (dvd is null)
                return false;

            dvd.Available = false;
            dvd.DeletedAt = request.DeletedAt;
            dvd.Copies = 0;

            return await _repository.Update(dvd);
        }
    }
}
