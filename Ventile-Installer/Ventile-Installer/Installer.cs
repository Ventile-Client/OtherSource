using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using IWshRuntimeLibrary;

/*INSTRUCTIONS\\

 - When creating a new version, upload all cosmetic.zip files to release.
 - Upload aboutInfo.txt in a release that contains:

    launcherVersion
    textureVersion
    clientVersion

 - Upload individual stuff in bin\x64\Debug or Release to the main github page
 - Upload VentileClient.lnk to main github page

 - Change the version in Ventile.settings

*/

namespace Ventile_Installer
{
    public partial class Installer : Form
    {
        public Installer()
        {
            InitializeComponent();
        }

        //Fade

        private void FadeIn_Tick(object sender, EventArgs e)
        {
            if (Opacity >= 1)
            {
                fadeIn.Stop();
            }
            Opacity += 0.04;
        }

        private void FadeOut_Tick(object sender, EventArgs e)
        {
            if (Opacity <= 0)
            {
                fadeOut.Stop();
                this.Close();
            }
            Opacity -= 0.04;
        }

        //Move Window

        private int mouseX = 0;
        private int mouseY = 0;
        private int mouseinX = 0;
        private int mouseinY = 0;
        private bool mouseDown;

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mouseinX = MousePosition.X - Bounds.X;
            mouseinY = MousePosition.Y - Bounds.Y;
        }

        private void topPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                mouseX = MousePosition.X - mouseinX;
                mouseY = MousePosition.Y - mouseinY;

                this.SetDesktopLocation(mouseX, mouseY);
            }
        }

        private void topPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void close_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            fadeOut.Start();
        }

        private void minus_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Visible = false;
                label1.Visible = true;

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
                download(@"https://github.com/DeathlyBower959/Ventile-Client-Downloads/blob/main/Ventile-Updater.exe?raw=true", @"C:\temp\", "Ventile-Updater.exe");

                Thread.Sleep(50);

                ZipFile.ExtractToDirectory(@"C:\temp\VentileClient\VentileClient.zip", @"C:\temp\VentileClient");
                System.IO.File.Delete(@"C:\temp\VentileClient\VentileClient.zip");

                Thread.Sleep(50);

                //Shortcuts
                var desk = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var start = Environment.GetFolderPath(Environment.SpecialFolder.Programs);

                if (System.IO.File.Exists(desk + @"\Ventile Client.lnk"))
                {
                    System.IO.File.Delete(desk + @"\Ventile Client.lnk");
                }

                if (System.IO.File.Exists(start + @"\Ventile Client.lnk"))
                {
                    System.IO.File.Delete(start + @"\Ventile Client.lnk");
                }

                CreateShortcut("Ventile Client", desk, @"C:\temp\VentileClient\VentileClient.exe\", @"C:\temp\VentileClient\transparent_logo_white.ico", "Ventile Client Launcher for MCBE");
                CreateShortcut("Ventile Client", start, @"C:\temp\VentileClient\VentileClient.exe\", @"C:\temp\VentileClient\transparent_logo_white.ico", "Ventile Client Launcher for MCBE");

                label1.Visible = false;
                button2.Visible = true;
                Properties.Settings.Default.Installed = true;

                Properties.Settings.Default.Save();
                Thread.Sleep(100);
                fadeOut.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;

            var desk = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var start = Environment.GetFolderPath(Environment.SpecialFolder.Programs);

            if (Directory.Exists(@"C:\temp\VentileClient\"))
            {
                Directory.Delete(@"C:\temp\VentileClient\", true);
            }

            if (System.IO.File.Exists(@"C:\temp\Ventile-Updater.exe"))
            {
                System.IO.File.Delete(@"C:\temp\Ventile-Updater.exe");
            }

            if (System.IO.File.Exists(start + @"\Ventile Client.lnk"))
            {
                System.IO.File.Delete(start + @"\Ventile Client.lnk");
            }
            if (System.IO.File.Exists(desk + @"\Ventile Client.lnk"))
            {
                System.IO.File.Delete(desk + @"\Ventile Client.lnk");
            }

            button1.Visible = true;
            label1.Visible = true;
            Properties.Settings.Default.Installed = false;
        }

        private void Installer_Load(object sender, EventArgs e)
        {
            bool internet = InternetCheck();
            
            if (!internet)
            {
                var msg = MessageBox.Show("No active connection", "Connection Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (msg == DialogResult.Retry)
                {
                    retry();
                    return;
                } else
                {
                    fadeOut.Start();
                }
            }

            if (!(Directory.Exists(@"C:\temp")))
            {
                Directory.CreateDirectory(@"C:\temp");
            }

            if (System.IO.File.Exists(@"C:\temp\VentileClient\VentileClient.exe"))
            {
                Properties.Settings.Default.Installed = true;
            }
            else
            {
                Properties.Settings.Default.Installed = false;
            }

            if (Properties.Settings.Default.Installed == true)
            {
                button1.Visible = false;
                button2.Visible = true;
                label1.Visible = false;
            }
            else
            {
                button1.Visible = true;
                button2.Visible = false;
                label1.Visible = true;
            }
        }

        private void retry()
        {
            bool internet = InternetCheck();

            if (!internet)
            {
                var msg = MessageBox.Show("No active connection", "Connection Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (msg == DialogResult.Retry)
                {
                    retry();
                    return;
                }
                else
                {
                    fadeOut.Start();
                }
            }

            if (!(Directory.Exists(@"C:\temp")))
            {
                Directory.CreateDirectory(@"C:\temp");
            }

            if (System.IO.File.Exists(@"C:\temp\VentileClient\VentileClient.exe"))
            {
                Properties.Settings.Default.Installed = true;
            }
            else
            {
                Properties.Settings.Default.Installed = false;
            }

            if (Properties.Settings.Default.Installed == true)
            {
                button1.Visible = false;
                button2.Visible = true;
                label1.Visible = false;
            }
            else
            {
                button1.Visible = true;
                button2.Visible = false;
                label1.Visible = true;
            }
        }
    }
}
