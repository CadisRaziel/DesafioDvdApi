
using DesafioDvD.Application.Contracts;
using DesafioDvD.Domain.Entities;
using DesafioDvD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioDvD.Infrastructure.Repositories
{
    public class DvdsWriteRepository : IDvdsWriteRepository
    {

        private readonly MoviesRentalWriteContext _context;
        public DvdsWriteRepository(MoviesRentalWriteContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Dvd entity)
        {
            await _context.Dvds.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await _context.Dvds
                .Where(d => d.Id == Id)
                .ExecuteDeleteAsync();

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Dvd> Get(Guid Id)
        {
            return await _context.Dvds.FindAsync(Id);
        }

        public async Task<bool> Update(Dvd entity)
        {
            _context.Dvds.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
