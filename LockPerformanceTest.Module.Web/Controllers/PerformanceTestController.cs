using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using LockPerformanceTest.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace LockPerformanceTest.Module.Web.Controllers
{
    public class PerformanceTestController : ViewController<ListView>
    {
        private readonly SimpleAction action = new SimpleAction();

        public PerformanceTestController()
        {
            action.Id = "PeformanceTestAction";
            action.Caption = "Performance Test";
            action.Execute += action_Execute;
            RegisterActions(action);
        }

        void action_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                using (var lv = Application.CreateListView(Application.CreateObjectSpace(), typeof(TestObject), true))
                {
                    lv.CreateControls();
                }
            }
            sw.Stop();
            Trace.TraceInformation("Time for 10000 views: " + sw.Elapsed.ToString());
        }
    }
}
