using DesafioDvD.Application.Contracts;
using DesafioDvD.Domain.Entities;
using DesafioDvD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioDvD.Infrastructure.Repositories
{
    public class DirectorsWriteRepository : IDirectorsWriteRepository
    {
        private readonly MoviesRentalWriteContext _context;
        public DirectorsWriteRepository(MoviesRentalWriteContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Director entity)
        {
            await _context.Directors.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0; //Se for maior que 0 vai ser verdadeiro e salva, se for menor significa que nao salvou
        }

        public async Task<bool> Delete(Guid Id)
        {
            await _context.Directors
                .Where(d => d.Id == Id)
                .ExecuteDeleteAsync(); //-> funcionalidade do .net 7, podemos deletar varias entidades ao mesmo tempo, se tiver itens iguais apaga todos
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Director> Get(Guid Id)
        {
            return await _context.Directors.FindAsync(Id); //FindAsync -> busca pela chave primaria(mais rapido), porem poderiamos por um Where e firstOrDefaultAsync
        }

        public async Task<Director> GetDirectorWithMovies(Guid Id)
        {
            return await _context.Directors.AsNoTracking() // AsNoTracking -> para que o EF nao fique fazendo o tracking(rastreamento) no banco, se nao consome mais memoria de ficar acompanahdno todo trajeto no banco de dados
                                                           // AsNoTracking -> Quando usar ? Consulta somente leitura
                .Include(d => d.Dvds) // Include -> Famoso join do banco de dados, incluindo os Dvds 
                .Where(d => d.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Director entity)
        {
           _context.Directors.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
