using System.Collections.Generic;

namespace XMPS2000.Core.App
{
    public class RecentProjects
    {
        public List<RecentProject> Projects { get; set; }

        public RecentProjects()
        {
            Projects = new List<RecentProject>();
        }
    }
}