using Favpolls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.DataAccess.Repository.IRepository
{
    public interface IPollSettingRepository : IRepository<PollSetting>
    {
        void Update(PollSetting pollSetting);
    }
}
