using System.Linq;
using Microsoft.EntityFrameworkCore;
using Salary.Core.Entities;

namespace Salary.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<Employee> IncludePosition(this IQueryable<Employee> employees)
        {
            return employees.Include(e => e.Position);
        }
    }
}