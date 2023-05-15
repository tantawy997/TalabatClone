using Core.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifiactions
{
    public class BaseSpecifiactions<T> : ISpecifications<T>
    {
        public BaseSpecifiactions(Expression<Func<T, bool>> _Creiteria) 
        {
            Creiteria = _Creiteria;
        }

        public Expression<Func<T, bool>> Creiteria { get;}

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        public int skip { get; private set; }

        public int take { get; private set; }

        public bool IspagingEnabled { get; private set; }

        protected void AddIncludes(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        protected void AddOrderByDesc(Expression<Func<T, object>> OrderByExpression)
        {
            OrderByDesc = OrderByExpression;
        }

        protected void AddPagination(int _skip, int _take)
        {
            skip = _skip;
            take = _take;
            IspagingEnabled = true;
        }
    }
}
