using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePointSupportTool
{
    internal class FileNames
    {
        public static string LocalDrive = @"\C";
        

        public static string NIBIT2001 = @"\nibit2001\";
        public static string Oracle = @"\Oracle\";
        public static string EmergencyFolder = @"\Users\Administrator\Desktop\";
        public static string Nibit2001INI = @"\nibit2001\software\ini\nibit2001.ini";
        public static string NibitINI = @"\nibit2001\software\bin\nibit.ini";
        public static string ConfigEMV = @"\nibit2001\software\bin\One1Point.EMVClient.lib.dll.config";

        // version 11.2 need to include
        public static string TNSName = @"\oracle\product\10.2.0\client_1\NETWORK\ADMIN\TNSNAMES.ora";
        public static string TNSName11 = @"\oracle\product\11.2.0\dbhome_1\NETWORK\ADMIN\TNSNAMES.ora";
        public static string OracleFolder = @"\oracle\product\";

        public static string LogFolder = @"\nibit2001\software\bin\Nibit.Log\";
        public static string DeclarPath = @"\nibit2001\Software\bin\";

        public static string ProjectPath =  Environment.CurrentDirectory  + "\\";

        public static string PSToolsPath = ProjectPath + @"PSTools\";


        public static string UltraVNVPath = Environment.CurrentDirectory + @"\UltraVNC\vncviewer.exe";

        public enum PSTools
        {
            PsExec,
            PsExec64,
            psfile,
            psfile64,
            PsGetsid,
            PsGetsid64,
            PsInfo,
            PsInfo64,
            pskill,
            pskill64,
            pslist,
            pslist64,
            PsLoggedon,
            PsLoggedon64,
            psloglist,
            psloglist64,
            pspasswd,
            pspasswd64,
            psping,
            psping64,
            PsService,
            PsService64,
            psshutdown,
            psshutdown64,
            pssuspend,
            pssuspend64
        }


        
        
        public static string IpStartVictory = "172.31.";
        public static string IpStartMShuk = "172.30.";
        public static string IpStartBareket = "172.15.";
        public static string IpStartHCohen = "172.30.";

        public static string UserCredential = @"Administrator";
        public static string PasswordCredential = "nibit";




        public static string ClientsBranch = "ClientsFiles";
        public static string Victory = "Victory.txt";
        public static string Mshuk = "Mshuk.txt";
        public static string Bareket = "Bareket.txt";
        public static string HCohen = "HCohen.txt";

        public static string BranchNo = Environment.CurrentDirectory + "\\" + ClientsBranch;

    }
}
