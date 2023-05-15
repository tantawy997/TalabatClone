using Core.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> Complete();

        IGenricRepo<TEntity> Repositary<TEntity>() where TEntity : BaseEntity; 
    }
}
