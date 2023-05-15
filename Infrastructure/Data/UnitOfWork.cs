using Core.entites;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext context;
        private Hashtable Repastories;

        public UnitOfWork(StoreDbContext _context)
        {
            context = _context;
        }
        public Task<int> Complete()
        {
            throw new NotImplementedException();
        }

        public IGenricRepo<TEntity> Repositary<TEntity>() where TEntity : BaseEntity
        {
            if(Repastories is null)
                Repastories = new Hashtable();

            var type = typeof(TEntity).Name;
            if (!Repastories.ContainsKey(type))
            {
                var reposatoryType = typeof(IGenricRepo<>);
                var repoInstance = Activator.CreateInstance(reposatoryType.MakeGenericType(typeof(TEntity)),context);

                Repastories.Add(type, repoInstance);
            }

            return (IGenricRepo<TEntity>)Repastories[type];
        }
    }
}
