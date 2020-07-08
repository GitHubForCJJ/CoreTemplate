using Arch.EntityFrameworkCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Temp.Data;

namespace Temp.Repository
{
    public class LoginTokenRepository : Repository<LoginToken>,IRepository<LoginToken>
    {
        public LoginTokenRepository(BloggingContext context) : base(context)
        {

        }
    }
}
