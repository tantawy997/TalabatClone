using Core.entites;
using Core.Interfaces;
using Core.Specifiactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenricRepo<T> : IGenricRepo<T> where T : BaseEntity
    {
        private readonly StoreDbContext context;

        public GenricRepo(StoreDbContext _Context)
        {
            context = _Context;
        }
        public async Task AddEntityAsync(T entity)
        {
           await context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {

            context.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
           return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntitytWithSpecifiaction(ISpecifications<T> specifications)
        {
            return await ApplySpecifactions(specifications).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecifactions(ISpecifications<T> specifications)
        {
            return SpecifiactionsEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), specifications);
        }
        public async Task<IReadOnlyList<T>> ListUnderSpecifications(ISpecifications<T> specifications)
        {
            return await ApplySpecifactions(specifications).ToListAsync();
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public async Task<int> CountAsync(ISpecifications<T> specifications)
        {
           return await ApplySpecifactions(specifications).CountAsync();

        }
    }
}
