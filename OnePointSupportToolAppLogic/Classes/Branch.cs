using OnePointSupportTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePointSupportToolAppLogic.Classes
{
    public class Branch : Branches
    {
        public string _IpAddress { get; set; }
        public string _BranchName { get; set; }
        public int _BranchNumber { get; set; }
        public string _VncPassword { get; set; }

    }
}
