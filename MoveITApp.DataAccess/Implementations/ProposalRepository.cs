using Microsoft.EntityFrameworkCore;
using MoveITApp.DataAccess.Interfaces;
using MoveITApp.Domain.Models;

namespace MoveITApp.DataAccess.Implementations
{
    public class ProposalRepository : IRepository<Proposal>
    {
        private MoveITDbContext _moveItDbContext;

        public ProposalRepository(MoveITDbContext moveItDbContext)
        {
            _moveItDbContext = moveItDbContext;
        }
        public async Task AddAsync(Proposal entity)
        {
            _moveItDbContext.Proposals.Add(entity);
            await _moveItDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Proposal entity)
        {
            _moveItDbContext.Proposals.Remove(entity);
            await _moveItDbContext.SaveChangesAsync();
        }

        public async Task<List<Proposal>> GetAllAsync()
        {
            return await _moveItDbContext.Proposals.ToListAsync();
        }

        public async Task<Proposal> GetByIdAsync(int id)
        {
            return await _moveItDbContext.Proposals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Proposal entity)
        {
            _moveItDbContext.Proposals.Update(entity);
            await _moveItDbContext.SaveChangesAsync();
        }
    }
}
