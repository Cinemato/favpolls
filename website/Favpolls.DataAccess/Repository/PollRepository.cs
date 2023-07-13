using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Favpolls.DataAccess.Data;
using Favpolls.DataAccess.Repository.IRepository;
using Favpolls.Models;

namespace Favpolls.DataAccess.Repository
{
    public class PollRepository : Repository<Poll>, IPollRepository
    {
        private readonly ApplicationDbContext _context;

        public PollRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Poll poll)
        {
            _context.Polls.Update(poll);
        }
    }
}
