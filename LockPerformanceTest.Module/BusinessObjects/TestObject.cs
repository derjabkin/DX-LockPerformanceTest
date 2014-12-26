using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockPerformanceTest.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class TestObject : BaseObject
    {
        public TestObject(Session session)
            : base(session)
        {

        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }

        private string description;
        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get { return description; }
            set { SetPropertyValue("Description", ref description, value); }
        }
    }
}
