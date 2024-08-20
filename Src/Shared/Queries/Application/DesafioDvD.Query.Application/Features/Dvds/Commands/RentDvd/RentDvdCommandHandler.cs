using DesafioDvD.Query.Application.Contracts;
using MediatR;

namespace DesafioDvD.Query.Application.Features.Dvds.Commands.RentDvd
{
    public class RentDvdCommandHandler : IRequestHandler<RentDvdCommand, bool>
    {
        private readonly IDvdsQueryRepository _repository;

        public RentDvdCommandHandler(IDvdsQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RentDvdCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id) || request.UpdatedAt > DateTime.Now)
                return false;

            //dvd is { Copies: 0 } -> Pattern matching, a propriedade Copies no meu dvd tiver o valor 0 ele entra no return
            var dvd = await _repository.Get(request.Id);
            if (dvd is null || dvd is { Copies: 0 })
                return false;

            dvd.Copies -= 1;

            return await _repository.Update(dvd);
        }
    }
}
