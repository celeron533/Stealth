using Squirrel;
using Stealth.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Model
{
    public interface IUpdateService
    {
        Task<UpdateInfo> CheckForUpdate(Action<int> progress = null);
        Task<ReleaseEntry> UpdateToLatestRelease(Action<int> progress = null);
    }
}