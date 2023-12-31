﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPollRepository Poll { get; }
        IPollOptionRepository PollOption { get; }
        IPollSettingRepository PollSetting { get; }
        IPollVoteRepository PollVote { get; }

        void Save();
    }
}