using System;
using System.Collections.Generic;
using System.Text;

namespace OnePointSupportToolBL
{
    public class NetworkDriveException : Exception
    {
        public static string exMessage = string.Empty;
        public NetworkDriveException(string msg)
        {
            exMessage = msg;
        }
    }
}
