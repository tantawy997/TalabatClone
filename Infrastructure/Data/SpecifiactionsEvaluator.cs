using Core.entites;
using Core.Specifiactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecifiactionsEvaluator<TEntity> where TEntity : BaseEntity
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery , ISpecifications<TEntity> specifications)
        {
            var query = InputQuery;
            if (specifications.Creiteria != null)
                query = query.Where(specifications.Creiteria);
            if (specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);
            if (specifications.OrderByDesc != null)
                query = query.OrderByDescending(specifications.OrderByDesc);
            if (specifications.IspagingEnabled)
                query = query.Skip(specifications.skip).Take(specifications.take);
            query = specifications.Includes.Aggregate(query, (current, include) => current.Include(include));

            
            return query;
        } 



    }
}
