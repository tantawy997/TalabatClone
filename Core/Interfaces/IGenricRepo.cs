using Core.entites;
using Core.Specifiactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenricRepo<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetEntitytWithSpecifiaction(ISpecifications<T> specifications);

        Task<int> CountAsync(ISpecifications<T> specifications);

        Task<IReadOnlyList<T>> ListUnderSpecifications(ISpecifications<T> specifications);
        void Update(T entity);

        void Delete(T entity);

        Task AddEntityAsync(T entity);


    }
}
