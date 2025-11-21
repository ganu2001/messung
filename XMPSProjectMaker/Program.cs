using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMPS1000.Core.App;
using XMPS1000.Core.Base;
using XMPS1000.Core.Devices;
using XMPS1000.Core.Serializer;
using XMPS1000.Core.Types;
using XMPS1000.Core.LadderLogic;
namespace XMPSProjectMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            XMProject _xMProject = new XMProject("XM100", "E:\\Project", "NewProject");
            COMDevice cOMDevice = new COMDevice();
            cOMDevice.Name = "COM1";
            Ethernet ethernet = new Ethernet();
            MODBUSTCPServer mODBUSTCPServer = new MODBUSTCPServer();
            MODBUSTCPClient mODBUSTCPClient = new MODBUSTCPClient();
            MODBUSRTUMaster mODBUSRTUMaster = new MODBUSRTUMaster();
            //ApplicationRecs ApplicationRung = new ApplicationRecs();
            XMIOConfig xMIOConfig = new XMIOConfig(IOListType.OnBoardIO, "XM100", IOType.AnalogInput, "A", "B", "C","-");
            XMIOConfig xMIOConfig1 = new XMIOConfig(IOListType.OnBoardIO, "XM100", IOType.AnalogInput, "P", "Q", "R", "-");
            _xMProject.Tags.Add(xMIOConfig);
            _xMProject.Tags.Add(xMIOConfig1);
            _xMProject.Devices.Add(cOMDevice);
            _xMProject.Devices.Add(ethernet);
            _xMProject.Devices.Add(mODBUSTCPServer);
            _xMProject.Devices.Add(mODBUSTCPClient);
            _xMProject.Devices.Add(mODBUSRTUMaster);

            SerializeDeserialize<XMProject> _serializeDeserialize = new SerializeDeserialize<XMProject>();
            string xmlData = _serializeDeserialize.SerializeData(_xMProject);


            ProjectTemplate projectTemplate = new ProjectTemplate("XM100", @".\ProjectTemplates\XM100\XM100.plc");
            ProjectTemplates projectTemplates = new ProjectTemplates();
            projectTemplates.Templates.Add(projectTemplate);

            SerializeDeserialize<ProjectTemplates> _serializeDeserialize1 = new SerializeDeserialize<ProjectTemplates>();
            string xmlData1 = _serializeDeserialize1.SerializeData(projectTemplates);

            RecentProjects recentProjects = new RecentProjects();

            //recentProjects.Projects.Add(new RecentProject { ProjectName = "Project1.xmprj", ProjectPath = @"C:\ProjectPath\Project1.xmprj" });
            //recentProjects.Projects.Add(new RecentProject { ProjectName = "Project2.xmprj", ProjectPath = @"C:\ProjectPath\Project2.xmprj" });
            //recentProjects.Projects.Add(new RecentProject { ProjectName = "Project3.xmprj", ProjectPath = @"C:\ProjectPath\Project3.xmprj" });
            //recentProjects.Projects.Add(new RecentProject { ProjectName = "Project4.xmprj", ProjectPath = @"C:\ProjectPath\Project4.xmprj" });
            //recentProjects.Projects.Add(new RecentProject { ProjectName = "Project5.xmprj", ProjectPath = @"C:\ProjectPath\Project5.xmprj" });
            //recentProjects.Projects.Add(new RecentProject { ProjectName = "Project6.xmprj", ProjectPath = @"C:\ProjectPath\Project6.xmprj" });
            //recentProjects.Projects.Add(new RecentProject { ProjectName = "Project7.xmprj", ProjectPath = @"C:\ProjectPath\Project7.xmprj" });
            //recentProjects.Projects.Add(new RecentProject { ProjectName = "Project8.xmprj", ProjectPath = @"C:\ProjectPath\Project8.xmprj" });
            //recentProjects.Projects.Add(new RecentProject { ProjectName = "Project9.xmprj", ProjectPath = @"C:\ProjectPath\Project9.xmprj" });
            //recentProjects.Projects.Add(new RecentProject { ProjectName = "Project10.xmprj", ProjectPath = @"C:\ProjectPath\Project10.xmprj" });

            SerializeDeserialize<RecentProjects> _serializeDeserialize3 = new SerializeDeserialize<RecentProjects>();
            string xmlData3 = _serializeDeserialize3.SerializeData(recentProjects);
        }
    }
}
