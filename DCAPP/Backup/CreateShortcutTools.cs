using IWshRuntimeLibrary;
using System;
using System.Windows.Forms;

namespace DAPRO
{
    public class CreateShortcutTools
    {
        public static void CreateStartupShortcut(string WorkingDirectory, string TargetPath)
        {
            try
            {
                string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                WshShell shell = new WshShell();
                string shortcutAddress = startupFolder + @"\DATABASE_BACKUP.lnk";
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                shortcut.Description = "A startup shortcut."; // set the description of the shortcut
                shortcut.WorkingDirectory = WorkingDirectory;//Application.StartupPath; /* working directory */
                shortcut.TargetPath = TargetPath;  //Application.ExecutablePath; /* path of the executable */
                                                   // optionally, you can set the arguments in shortcut.Arguments
                                                   // For example, shortcut.Arguments = "/a 1";
                shortcut.Save();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
    }
}
