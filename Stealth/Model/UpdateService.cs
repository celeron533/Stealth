using Squirrel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Model
{
    public class UpdateService : IUpdateService
    {
        private const string GitHubRepositoryURL = @"https://github.com/celeron533/Stealth";
        private Task<UpdateManager> updateManager = UpdateManager.GitHubUpdateManager(GitHubRepositoryURL);

        public UpdateService()
        {
        }

        public async Task<UpdateInfo> CheckForUpdate(Action<int> progress = null)
        {
            try
            {
                return await updateManager.Result.CheckForUpdate(progress: progress);
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public async Task<ReleaseEntry> UpdateToLatestRelease(Action<int> progress = null)
        {
            try
            {
                return await updateManager.Result.UpdateApp(progress);
            }
            catch (Exception e)
            {

            }
            return null;
        }

    }
}
