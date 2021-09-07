using IWshRuntimeLibrary;
using Octokit;
using System;
using System.Diagnostics;
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

        public static LinkSettings linkSettings = new LinkSettings()
        {
            DiscordInvite = @"https://discord.gg/T2QtgdrtAY",
            WebsiteLink = @"https://ventile-client.github.io/Web/",
            RepoOwner = "Ventile-Client",
            VersionsRepo = "VersionChanger",
            DownloadRepo = "Download",
            GithubProductHeader = "VentileClientUpdater"
        };

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

        private async void Updater_Load(object sender, EventArgs e)
        {
            //Folders
            if (Directory.Exists(@"C:\temp\VentileClient\Launcher"))
            {
                Directory.Delete(@"C:\temp\VentileClient\Launcher", true);
            }

            Directory.CreateDirectory(@"C:\temp\VentileClient\Launcher");

            GitHubClient github = new GitHubClient(new ProductHeaderValue(linkSettings.GithubProductHeader)); // New Github Client

            var releases = await github.Repository.Release.GetAll(linkSettings.RepoOwner, linkSettings.DownloadRepo); // Gets all releases from the VersionChanger repo

            download(string.Format(@"https://github.com/" + linkSettings.RepoOwner + "/" + linkSettings.DownloadRepo + "/releases/download/{0}/{1}", releases[0].TagName, "VentileClient.zip"), @"C:\temp\VentileClient\Launcher", "VentileClient.zip");
            ZipFile.ExtractToDirectory(@"C:\temp\VentileClient\Launcher\VentileClient.zip", @"C:\temp\VentileClient\Launcher");
            System.IO.File.Delete(@"C:\temp\VentileClient\Launcher\VentileClient.zip");

            Process.Start(@"C:\temp\VentileClient\Launcher\VentileClient.exe");

            this.Close();
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