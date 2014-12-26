using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using LockPerformanceTest.Module.BusinessObjects;
using System.Globalization;

namespace LockPerformanceTest.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            var objects = ObjectSpace.GetObjects<TestObject>();
            if (objects.Count == 0)
            {
                for (int i = 1; i < 1001; i++)
                {
                    var obj = ObjectSpace.CreateObject<TestObject>();
                    obj.Name = string.Format(CultureInfo.InvariantCulture, "Name {0}", i);
                    obj.Description = string.Format(CultureInfo.InvariantCulture, "Description {0}", i);
                }
            }
            ObjectSpace.CommitChanges();

        }
    }
}
