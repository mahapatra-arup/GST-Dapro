using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace DAPRO
{
    public static class AccessPermission
    {
        public static  void GrantAccess()
        {
            string fullPath = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);

            /////Read only 
            //var di = new DirectoryInfo(fullPath);
            //di.Attributes &= ~FileAttributes.ReadOnly;
        }

        public static string CMDAccessPermission()
        {
            string PermissionStr = "@echo==================Admin Permission============================================\r\n" +
              "@echo off\r\n" +
              ":: BatchGotAdmin (Run as Admin code starts)\r\n" +
              "REM --> Check for permissions\r\n" +
              ">nul 2>&1 \"%SYSTEMROOT%\\system32\\cacls.exe\" \"%SYSTEMROOT%\\system32\\config\\system\"\r\n" +
              "REM --> If error flag set, we do not have admin.\r\n" +
              "if '%errorlevel%' NEQ '0' (\r\n" +
              "echo Requesting administrative privileges...\r\n" +
              "goto UACPrompt\r\n" +
              ") else ( goto gotAdmin )\r\n" +
              ":UACPrompt\r\n" +
              "echo Set UAC = CreateObject^(\"Shell.Application\"^) > \"%temp%\\getadmin.vbs\"\r\n" +
              "echo UAC.ShellExecute \"%~s0\", \"\", \"\", \"runas\", 1 >> \"%temp%\\getadmin.vbs\"\r\n" +
              "\"%temp%\\getadmin.vbs\"\r\n" +
              "exit /B\r\n" +
              ":gotAdmin\r\n" +
              "if exist \"%temp%\\getadmin.vbs\" ( del \"%temp%\\getadmin.vbs\" )\r\n" +
              "pushd \"%CD%\"\r\n" +
              "CD /D \"%~dp0\"\r\n" +
              ":: BatchGotAdmin (Run as Admin code ends)\r\n ";
            return PermissionStr;
        }
    }
}
