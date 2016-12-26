using Stealth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Squirrel;

namespace Stealth.Design
{
    // do not register this in the designer
    public class DesignUpdateService : IUpdateService
    {
        public Task<UpdateInfo> CheckForUpdate(Action<int> progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<ReleaseEntry> UpdateToLatestRelease(Action<int> progress = null)
        {
            throw new NotImplementedException();
        }
    }
}
