using DesafioDvD.Query.Application.Features.Dvds.Queries.GetDvd;

namespace DesafioDvD.WebApi.Cache
{
    public interface ICacheRepository
    {
        //O cache e um registro temporario, ele sera apagado em algum momento, por isso nao necessita de um delete
        Task<GetDvdResponse> Get(string title);
        Task Update(GetDvdResponse response);
    }
}
