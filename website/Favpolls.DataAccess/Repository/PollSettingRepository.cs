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
    public class PollSettingRepository : Repository<PollSetting>, IPollSettingRepository
    {
        private readonly ApplicationDbContext _context;
        public PollSettingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(PollSetting pollSetting)
        {
            _context.PollSettings.Update(pollSetting);

        }
    }
}
