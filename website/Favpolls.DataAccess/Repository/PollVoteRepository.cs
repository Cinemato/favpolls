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
    public class PollVoteRepository : Repository<PollVote>, IPollVoteRepository
    {
        private readonly ApplicationDbContext _context;

        public PollVoteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(PollVote pollVote)
        {
            _context.PollVotes.Update(pollVote);
        }
    }
}
