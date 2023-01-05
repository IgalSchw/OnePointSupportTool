using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace OnePointSupportToolBL
{
	//public class NetworkDrive
	//{
	//    public enum ResourceScope
	//    {
	//        RESOURCE_CONNECTED = 1,
	//        RESOURCE_GLOBALNET,
	//        RESOURCE_REMEMBERED,
	//        RESOURCE_RECENT,
	//        RESOURCE_CONTEXT
	//    }

	//    public enum ResourceType
	//    {
	//        RESOURCETYPE_ANY,
	//        RESOURCETYPE_DISK,
	//        RESOURCETYPE_PRINT,
	//        RESOURCETYPE_RESERVED
	//    }

	//    public enum ResourceUsage
	//    {
	//        RESOURCEUSAGE_CONNECTABLE = 0x00000001,
	//        RESOURCEUSAGE_CONTAINER = 0x00000002,
	//        RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
	//        RESOURCEUSAGE_SIBLING = 0x00000008,
	//        RESOURCEUSAGE_ATTACHED = 0x00000010,
	//        RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
	//    }

	//    public enum ResourceDisplayType
	//    {
	//        RESOURCEDISPLAYTYPE_GENERIC,
	//        RESOURCEDISPLAYTYPE_DOMAIN,
	//        RESOURCEDISPLAYTYPE_SERVER,
	//        RESOURCEDISPLAYTYPE_SHARE,
	//        RESOURCEDISPLAYTYPE_FILE,
	//        RESOURCEDISPLAYTYPE_GROUP,
	//        RESOURCEDISPLAYTYPE_NETWORK,
	//        RESOURCEDISPLAYTYPE_ROOT,
	//        RESOURCEDISPLAYTYPE_SHAREADMIN,
	//        RESOURCEDISPLAYTYPE_DIRECTORY,
	//        RESOURCEDISPLAYTYPE_TREE,
	//        RESOURCEDISPLAYTYPE_NDSCONTAINER
	//    }

	//    [StructLayout(LayoutKind.Sequential)]
	//    private class NETRESOURCE
	//    {
	//        public ResourceScope dwScope = 0;
	//        public ResourceType dwType = 0;
	//        public ResourceDisplayType dwDisplayType = 0;
	//        public ResourceUsage dwUsage = 0;
	//        public string lpLocalName = null;
	//        public string lpRemoteName = null;
	//        public string lpComment = null;
	//        public string lpProvider = null;
	//    }

	//    [DllImport("mpr.dll")]
	//    private static extern int WNetAddConnection2(NETRESOURCE lpNetResource, string lpPassword, string lpUsername, int dwFlags);

	//    public int MapNetworkDrive(string unc, string drive, string user, string password)
	//    {
	//        int result = 0;
	//        NETRESOURCE myNetResource = new NETRESOURCE();
	//        myNetResource.lpLocalName = drive;
	//        myNetResource.lpRemoteName = unc;
	//        myNetResource.lpProvider = null;
	//        try
	//        {
	//            result = WNetAddConnection2(myNetResource, password, user, 0);
	//        }
	//        catch
	//        {
	//            throw new NetworkDriveException("Error with connection to this PC");
	//        }

	//        return result;
	//    }


	//}


	public class NetworkDrive
	{
		#region API
		[DllImport("mpr.dll")] private static extern int WNetAddConnection2A(ref structNetResource pstNetRes, string psPassword, string psUsername, int piFlags);
		[DllImport("mpr.dll")] private static extern int WNetCancelConnection2A(string psName, int piFlags, int pfForce);
		[DllImport("mpr.dll")] private static extern int WNetConnectionDialog(int phWnd, int piType);
		[DllImport("mpr.dll")] private static extern int WNetDisconnectDialog(int phWnd, int piType);
		[DllImport("mpr.dll")] private static extern int WNetRestoreConnectionW(int phWnd, string psLocalDrive);

		[StructLayout(LayoutKind.Sequential)]
		private struct structNetResource
		{
			public int iScope;
			public int iType;
			public int iDisplayType;
			public int iUsage;
			public string sLocalName;
			public string sRemoteName;
			public string sComment;
			public string sProvider;
		}

		private const int RESOURCETYPE_DISK = 0x1;

		//Standard	
		private const int CONNECT_INTERACTIVE = 0x00000008;
		private const int CONNECT_PROMPT = 0x00000010;
		private const int CONNECT_UPDATE_PROFILE = 0x00000001;
		//IE4+
		private const int CONNECT_REDIRECT = 0x00000080;
		//NT5 only
		private const int CONNECT_COMMANDLINE = 0x00000800;
		private const int CONNECT_CMD_SAVECRED = 0x00001000;

		#endregion

		#region Propertys and options
		private bool lf_SaveCredentials = false;
		/// <summary>
		/// Option to save credentials are reconnection...
		/// </summary>
		public bool SaveCredentials
		{
			get { return (lf_SaveCredentials); }
			set { lf_SaveCredentials = value; }
		}
		private bool lf_Persistent = false;
		/// <summary>
		/// Option to reconnect drive after log off / reboot ...
		/// </summary>
		public bool Persistent
		{
			get { return (lf_Persistent); }
			set { lf_Persistent = value; }
		}
		private bool lf_Force = false;
		/// <summary>
		/// Option to force connection if drive is already mapped...
		/// or force disconnection if network path is not responding...
		/// </summary>
		public bool Force
		{
			get { return (lf_Force); }
			set { lf_Force = value; }
		}
		private bool ls_PromptForCredentials = false;
		/// <summary>
		/// Option to prompt for user credintals when mapping a drive
		/// </summary>
		public bool PromptForCredentials
		{
			get { return (ls_PromptForCredentials); }
			set { ls_PromptForCredentials = value; }
		}

		private string ls_Drive = "s:";
		/// <summary>
		/// Drive to be used in mapping / unmapping...
		/// </summary>
		public string LocalDrive
		{
			get { return (ls_Drive); }
			set
			{
				if (value.Length >= 1)
				{
					ls_Drive = value.Substring(0, 1) + ":";
				}
				else
				{
					ls_Drive = "";
				}
			}
		}
		private string ls_ShareName = "\\\\Computer\\C$";
		/// <summary>
		/// Share address to map drive to.
		/// </summary>
		public string ShareName
		{
			get { return (ls_ShareName); }
			set { ls_ShareName = value; }
		}
		#endregion

		#region Function mapping
		/// <summary>
		/// Map network drive
		/// </summary>
		public void MapDrive() { zMapDrive(null, null); }
		/// <summary>
		/// Map network drive (using supplied Password)
		/// </summary>
		public void MapDrive(string Password) { zMapDrive(null, Password); }
		/// <summary>
		/// Map network drive (using supplied Username and Password)
		/// </summary>
		public void MapDrive(string Username, string Password) { zMapDrive(Username, Password); }
		/// <summary>
		/// Unmap network drive
		/// </summary>
		public void UnMapDrive() { zUnMapDrive(this.lf_Force); }
		/// <summary>
		/// Check / restore persistent network drive
		/// </summary>
		public void RestoreDrives() { zRestoreDrive(); }
		///// <summary>
		///// Display windows dialog for mapping a network drive
		///// </summary>
		//public void ShowConnectDialog(Form ParentForm) { zDisplayDialog(ParentForm, 1); }
		///// <summary>
		///// Display windows dialog for disconnecting a network drive
		///// </summary>
		//public void ShowDisconnectDialog(Form ParentForm) { zDisplayDialog(ParentForm, 2); }
		#endregion

		#region Core functions

		// Map network drive
		private void zMapDrive(string psUsername, string psPassword)
		{
			//create struct data
			structNetResource stNetRes = new structNetResource();
			stNetRes.iScope = 2;
			stNetRes.iType = RESOURCETYPE_DISK;
			stNetRes.iDisplayType = 3;
			stNetRes.iUsage = 1;
			stNetRes.sRemoteName = ls_ShareName;
			stNetRes.sLocalName = ls_Drive;
			//prepare params
			int iFlags = 0;
			if (lf_SaveCredentials) { iFlags += CONNECT_CMD_SAVECRED; }
			if (lf_Persistent) { iFlags += CONNECT_UPDATE_PROFILE; }
			if (ls_PromptForCredentials) { iFlags += CONNECT_INTERACTIVE + CONNECT_PROMPT; }
			if (psUsername == "") { psUsername = null; }
			if (psPassword == "") { psPassword = null; }
			//if force, unmap ready for new connection
			if (lf_Force) { try { zUnMapDrive(true); } catch { } }
			//call and return
			int i = WNetAddConnection2A(ref stNetRes, psPassword, psUsername, iFlags);
			if (i > 0) { throw new System.ComponentModel.Win32Exception(i); }
		}

		// Unmap network drive
		private void zUnMapDrive(bool pfForce)
		{
			//call unmap and return
			int iFlags = 0;
			if (lf_Persistent) { iFlags += CONNECT_UPDATE_PROFILE; }
			int i = WNetCancelConnection2A(ls_Drive, iFlags, Convert.ToInt32(pfForce));
			if (i > 0) { throw new System.ComponentModel.Win32Exception(i); }
		}
		// Check / Restore a network drive
		private void zRestoreDrive()
		{
			//call restore and return
			int i = WNetRestoreConnectionW(0, null);
			if (i > 0) { throw new System.ComponentModel.Win32Exception(i); }
		}
		
		// Display windows dialog
		//private void zDisplayDialog(Form poParentForm, int piDialog)
		//{
		//	int i = -1;
		//	int iHandle = 0;
		//	//get parent handle
		//	if (poParentForm != null)
		//	{
		//		iHandle = poParentForm.Handle.ToInt32();
		//	}
		//	//show dialog
		//	if (piDialog == 1)
		//	{
		//		i = WNetConnectionDialog(iHandle, RESOURCETYPE_DISK);
		//	}
		//	else if (piDialog == 2)
		//	{
		//		i = WNetDisconnectDialog(iHandle, RESOURCETYPE_DISK);
		//	}
		//	if (i > 0) { throw new System.ComponentModel.Win32Exception(i); }
		//	//set focus on parent form
		//	poParentForm.BringToFront();
		//}
		#endregion
	}

}
