﻿

using DesafioDvD.Query.Application.Contracts;
using MediatR;

namespace DesafioDvD.Query.Application.Features.Dvds.Queries.GetDvd
{
    public class GetDvdQueryHandler : IRequestHandler<GetDvdQuery, GetDvdResponse>
    {
        private readonly IDvdsQueryRepository _repository;

        public GetDvdQueryHandler(IDvdsQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetDvdResponse> Handle(GetDvdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.title))
                return default;

            var dvd = await _repository.GetByTitle(request.title);
            if (dvd is null)
                return default;

            return new GetDvdResponse(dvd.Id, dvd.Title, dvd.Genre, dvd.Published, dvd.Copies, dvd.DirectorId, dvd.CreatedAt, dvd.UpdatedAt);
        }
    }
}
