using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Favpolls.Models;

namespace Favpolls.DataAccess.Repository.IRepository
{
    public interface IPollRepository : IRepository<Poll>
    {
        void Update(Poll poll);
    }
}
