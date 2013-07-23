using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pms.Presentation.Mvc.Helper.Attribute
{
    public class LocalizedDisplayName : DisplayNameAttribute
    {
        private readonly string _resourceName;
        public LocalizedDisplayName(string resourceName) : base()
        {
            this._resourceName = resourceName;
        }

        public override string DisplayName
        {
            get
            {
                return Resources.Pms.ResourceManager.GetString(this._resourceName);
            }
        }
    }

}
