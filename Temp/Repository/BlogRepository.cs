using Arch.EntityFrameworkCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Temp.Repository
{
    public class BlogRepository : Repository<Blog>, IRepository<Blog>
    {
        public BlogRepository(BloggingContext context) : base(context)
        {

        }
    }
}
