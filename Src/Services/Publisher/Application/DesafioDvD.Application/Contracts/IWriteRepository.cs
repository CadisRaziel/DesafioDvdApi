using DesafioDvD.Core.DomainObjects;

namespace DesafioDvD.Application.Contracts
{
    //Repositorio generico de escrita
    public interface IWriteRepository<T> where T : Entity
    {
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(Guid Id);
        Task<T> Get(Guid Id);
    }
}
