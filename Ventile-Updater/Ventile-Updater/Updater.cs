using IWshRuntimeLibrary;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Ventile_Updater
{
    public partial class Updater : Form
    {
        public Updater()
        {
            InitializeComponent();
        }

        #region InternetTest

        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool OnlineCheck()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://github.com/");
            request.Timeout = 15000;
            request.Method = "HEAD";
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        public static bool InternetCheck()
        {
            int desc;
            bool internetcheckone = InternetGetConnectedState(out desc, 0);
            bool internetchecktwo = OnlineCheck();

            if (internetcheckone == true && internetchecktwo == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion InternetTest

        #region Downloading Code

        public void download(string link, string path, string name)
        {
            bool internet = InternetCheck();
            if (internet == true)
            {
                using (WebClient Client = new WebClient())
                {
                    Client.DownloadFile(link, path + @"\" + name);
                }
            }
            else
            {
                MessageBox.Show("No Internet!\nCannot install!");
            }
        }

        #endregion Downloading Code

        #region Shortcut

        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation, string iconLoc, string desc)
        {
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = desc;   // The description of the shortcut
            shortcut.IconLocation = iconLoc;           // The icon of the shortcut
            shortcut.TargetPath = targetFileLocation;                 // The path of the file that will launch when the shortcut is run
            shortcut.Save();
        }

        #endregion Shortcut

        private void Updater_Load(object sender, EventArgs e)
        {
            //Folders
            if (!(Directory.Exists(@"C:\temp")))
            {
                Directory.CreateDirectory(@"C:\temp");
            }

            if (Directory.Exists(@"C:\temp\VentileClient"))
            {
                Directory.Delete(@"C:\temp\VentileClient", true);
            }

            Directory.CreateDirectory(@"C:\temp\VentileClient");

            Thread.Sleep(100);

            download(@"https://github.com/DeathlyBower959/Ventile-Client-Downloads/blob/main/VentileClient.zip?raw=true", @"C:\temp\VentileClient\", "VentileClient.zip");
            ZipFile.ExtractToDirectory(@"C:\temp\VentileClient\VentileClient.zip", @"C:\temp\VentileClient");
            System.IO.File.Delete(@"C:\temp\VentileClient\VentileClient.zip");

            //Shortcuts
            var desk = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var start = Environment.GetFolderPath(Environment.SpecialFolder.Programs);

            if (System.IO.File.Exists(desk + @"\Ventile Client.lnk")) {
                System.IO.File.Delete(desk + @"\Ventile Client.lnk");
            }

            if (System.IO.File.Exists(start + @"\Ventile Client.lnk")) {
                System.IO.File.Delete(start + @"\Ventile Client.lnk");
            }

            CreateShortcut("Ventile Client", desk, @"C:\temp\VentileClient\VentileClient.exe\", @"C:\temp\VentileClient\transparent_logo_white.ico", "Ventile Client Launcher for MCBE");
            CreateShortcut("Ventile Client", start, @"C:\temp\VentileClient\VentileClient.exe\", @"C:\temp\VentileClient\transparent_logo_white.ico", "Ventile Client Launcher for MCBE");

            this.Close();
        }
    }
}