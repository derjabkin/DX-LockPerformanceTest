using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using LockPerformanceTest.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
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
            const int count = 10000;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            CreateViewLoop(count);
            sw.Stop();

            var sequentialTime = sw.Elapsed;
            Trace.TraceInformation("Time for 10000 views: " + sw.Elapsed.ToString());

            sw.Reset();
            sw.Start();

            var hostContext = CallContext.HostContext;
                    
            const int threadCount = 4;
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < threadCount; i++)
            {
                threads.Add(new Thread(() =>
                    {
                        CallContext.HostContext = hostContext;
                        CreateViewLoop(count / threadCount);
                    }));
            }

            foreach (var thread in threads)
                thread.Start();

            foreach (var thread in threads)
                thread.Join();
            
            
            var parallelTime = sw.Elapsed;

            Trace.TraceInformation("Time for 10000 views (parallel): " + parallelTime.ToString());


        }

        private void CreateViewLoop(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CreateView();
            }
        }

        private void CreateView()
        {
            using (var lv = Application.CreateListView(Application.CreateObjectSpace(), typeof(TestObject), true))
            {
                lv.CreateControls();
            }
        }
    }
}
