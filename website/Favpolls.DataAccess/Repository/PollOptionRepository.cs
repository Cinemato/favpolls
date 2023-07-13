using Favpolls.DataAccess.Data;
using Favpolls.DataAccess.Repository.IRepository;
using Favpolls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.DataAccess.Repository
{
    public class PollOptionRepository : Repository<PollOption>, IPollOptionRepository
    {
        private readonly ApplicationDbContext _context;
        public PollOptionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(PollOption pollOption)
        {
            _context.Update(pollOption);
        }
    }
}
