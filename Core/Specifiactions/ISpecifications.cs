using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifiactions
{
    public interface ISpecifications<T>
    {
        Expression<Func<T, bool>> Creiteria { get; }

        List<Expression<Func<T, Object>>> Includes { get; }

        Expression<Func<T, Object>> OrderBy { get; }

        Expression<Func<T, Object>> OrderByDesc { get; }


        int skip { get; }

        int take { get; }

        bool IspagingEnabled { get; }


    }

}
