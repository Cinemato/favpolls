using Favpolls.DataAccess.Data;
using Favpolls.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IPollRepository Poll { get; private set; }
        public IPollOptionRepository PollOption { get; private set; }
        public IPollSettingRepository PollSetting { get; private set; }

        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context;
            Poll = new PollRepository(context);
            PollOption = new PollOptionRepository(context);
            PollSetting = new PollSettingRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}