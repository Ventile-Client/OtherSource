using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Octokit;

namespace Ventile_Installer
{
    
    public partial class Installer : Form
    {
        public Installer()
        {
            InitializeComponent();
        }

        GitHubClient github = new GitHubClient(new ProductHeaderValue(linkSettings.GithubProductHeader)); // New Github Client

        public static LinkSettings linkSettings = new LinkSettings()
        {
            DiscordInvite = @"https://discord.gg/T2QtgdrtAY",
            WebsiteLink = @"https://ventile-client.github.io/Web/",
            RepoOwner = "Ventile-Client",
            VersionsRepo = "VersionChanger",
            DownloadRepo = "Download",
            GithubProductHeader = "VentileClientInstaller"
        };

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
                try
                {
                    Process.Start(@"C:\temp\VentileClient\Launcher\VentileClient.exe");
                }
                catch
                {

                }
                this.Close();
            }
            Opacity -= 0.04;
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
                label1.Text = "Installing...";
                label1.Visible = true;
                button2.Visible = false;  

                //Folders
                if (!(Directory.Exists(@"C:\temp")))
                {
                    Directory.CreateDirectory(@"C:\temp");
                }

                if (!Directory.Exists(@"C:\temp\VentileClient"))
                {
                    Directory.CreateDirectory(@"C:\temp\VentileClient");
                }

                if (!Directory.Exists(@"C:\temp\VentileClient\Presets"))
                {
                    Directory.CreateDirectory(@"C:\temp\VentileClient\Presets");
                }

                if (Directory.Exists(@"C:\temp\VentileClient\Launcher"))
                {
                    Directory.Delete(@"C:\temp\VentileClient\Launcher", true);
                }

                if (System.IO.File.Exists(@"C:\temp\VentileClient\Ventile-Updater.exe"))
                {
                    System.IO.File.Delete(@"C:\temp\VentileClient\Ventile-Updater.exe");
                }

                Directory.CreateDirectory(@"C:\temp\VentileClient\Launcher");

                int index = 0;
                foreach (Release release in releases)
                {
                    if (release.TagName == selectedRelease.Text)
                    {
                        break;
                    }
                    index++;
                }

                download(string.Format(@"https://github.com/" + linkSettings.RepoOwner + "/" + linkSettings.DownloadRepo + "/releases/download/{0}/{1}", releases[index].TagName, "VentileClient.zip"), @"C:\temp\VentileClient\Launcher", "VentileClient.zip");
                download(@"https://github.com/" + linkSettings.RepoOwner + "/" + linkSettings.DownloadRepo + "/blob/main/Ventile-Updater.exe?raw=true", @"C:\temp\VentileClient", "Ventile-Updater.exe");
                download(string.Format(@"https://github.com/" + linkSettings.RepoOwner + "/" + linkSettings.DownloadRepo + "/releases/download/{0}/{1}", releases[index].TagName, "Presets.zip"), @"C:\temp\VentileClient\Presets", "Presets.zip");

                ZipFile.ExtractToDirectory(@"C:\temp\VentileClient\Launcher\VentileClient.zip", @"C:\temp\VentileClient\Launcher");
                System.IO.File.Delete(@"C:\temp\VentileClient\Launcher\VentileClient.zip");

                ZipFile.ExtractToDirectory(@"C:\temp\VentileClient\Presets\Presets.zip", @"C:\temp\VentileClient\Presets");
                System.IO.File.Delete(@"C:\temp\VentileClient\Presets\Presets.zip");

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

                CreateShortcut("Ventile Client", desk, @"C:\temp\VentileClient\Launcher\VentileClient.exe\", @"C:\temp\VentileClient\Launcher\transparent_logo_white.ico", "Ventile Client Launcher for MCBE");
                CreateShortcut("Ventile Client", start, @"C:\temp\VentileClient\Launcher\VentileClient.exe\", @"C:\temp\VentileClient\Launcher\transparent_logo_white.ico", "Ventile Client Launcher for MCBE");

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
            label1.Text = "Uninstalling...";
            label1.Visible = true;
            button1.Visible = false;

            var desk = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var start = Environment.GetFolderPath(Environment.SpecialFolder.Programs);

            if (Directory.Exists(@"C:\temp\VentileClient\"))
            {
                Directory.Delete(@"C:\temp\VentileClient\", true);
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
            label1.Visible = false;
            selectedRelease.Visible = true;
            Properties.Settings.Default.Installed = false;
        }

        System.Collections.Generic.IReadOnlyList<Release> releases;

        private async void Installer_Load(object sender, EventArgs e)
        {
            releases = await github.Repository.Release.GetAll(linkSettings.RepoOwner, linkSettings.DownloadRepo); // Gets all releases from the VersionChanger repo

            selectedRelease.Text = releases[0].TagName;

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

            if (System.IO.File.Exists(@"C:\temp\VentileClient\Launcher\VentileClient.exe"))
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
                selectedRelease.Visible = false;
            }
            else
            {
                button1.Visible = true;
                button2.Visible = false;
                label1.Visible = true;
                selectedRelease.Visible = true;
            }

            foreach (Release release in releases)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Click += VersionChanged;
                item.Name = release.TagName;
                item.Text = item.Name;

                versionSelector.Items.Add(item);
            }
        }

        private void VersionChanged(object sender, EventArgs e)
        {
            selectedRelease.Text = ((ToolStripMenuItem)sender).Name;
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

            if (System.IO.File.Exists(@"C:\temp\VentileClient\Launcher\VentileClient.exe"))
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

    public class LinkSettings
    {
        public string RepoOwner;
        public string DownloadRepo;
        public string VersionsRepo;

        public string WebsiteLink;
        public string DiscordInvite;
        public string GithubProductHeader;
    }
}
