using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using VentileClient.JSON_Template_Classes;
using System.Text.Json;
namespace VentileClient
{
    class RPC
    {
        public void Toast(string title, string msg)
        {
            Toast toast = new Toast();
            toast.showToast(title, msg, configCS, themeCS);
        }

        #region Downloading Code

        public void download(string link, string path, string name)
        {
            using (WebClient Client = new WebClient())
            {
                if (path.EndsWith(@"\"))
                {
                    Client.DownloadFile(link, path + name);
                }
                else
                {
                    Client.DownloadFile(link, path + @"\" + name);
                }
            }
        }

        #endregion Downloading Code

        #region Stream Read/Write

        ConfigTemplate configCS = new ConfigTemplate();
        CosmeticsTemplate cosmeticsCS = new CosmeticsTemplate();
        ThemeTemplate themeCS = new ThemeTemplate();
        PresetColorsTemplate presetCS = new PresetColorsTemplate();

        private void readPresetColors(string path)
        {
            try
            {
                string temp = File.ReadAllText(path);
                presetCS = JsonSerializer.Deserialize<PresetColorsTemplate>(temp);
            }
            catch
            {
                this.Toast("Error", "There was an error :(");
            }
        }

        private async void writePresetColors(string path)
        {
            await Task.Run(() =>
            {
                try
                {
                    PresetColorsTemplate temp = new PresetColorsTemplate()
                    {
                        p1 = presetCS.p1,
                        p2 = presetCS.p2,
                        p3 = presetCS.p3,
                        p4 = presetCS.p4,
                        p5 = presetCS.p5,
                        p6 = presetCS.p6,
                        p7 = presetCS.p7,
                        p8 = presetCS.p8,
                    };

                    string json = JsonSerializer.Serialize(temp, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(path, json);
                }
                catch
                {
                    this.Toast("Error", "There was an error :(");
                }
            });
        }

        private void readConfig(string path)
        {
            try
            {
                string temp = File.ReadAllText(path);
                configCS = JsonSerializer.Deserialize<ConfigTemplate>(temp);
            }
            catch
            {
                this.Toast("Error", "There was an error :(");
            }
        }

        private async void writeConfig(string path)
        {
            await Task.Run(() =>
            {
                try
                {
                    ConfigTemplate temp = new ConfigTemplate()
                    {
                        WindowState = configCS.WindowState,
                        AutoInject = configCS.AutoInject,
                        RichPresence = configCS.RichPresence,
                        RpcText = configCS.RpcText,
                        RpcButton = configCS.RpcButton,
                        RpcButtonLink = configCS.RpcButtonLink,
                        RpcButtonText = configCS.RpcButtonText,
                        CustomDLL = configCS.CustomDLL,
                        DefaultDLL = configCS.DefaultDLL,
                        ResourcePackLoc = configCS.ResourcePackLoc,
                        BackgroundImage = configCS.BackgroundImage,
                        BackgroundImageLoc = configCS.BackgroundImageLoc,
                        Toasts = configCS.Toasts,
                        ToastsLoc = configCS.ToastsLoc,
                        RoundedButtons = configCS.RoundedButtons

                    };

                    string json = JsonSerializer.Serialize(temp, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(path, json);
                }
                catch
                {
                    this.Toast("Error", "There was an error :(");
                }
            });
        }

        private void readTheme(string path)
        {
            try
            {
                string temp = File.ReadAllText(path);
                themeCS = JsonSerializer.Deserialize<ThemeTemplate>(temp);
            }
            catch
            {
                this.Toast("Error", "There was an error :(");
            }
        }

        private async void writeTheme(string path)
        {
            await Task.Run(() =>
            {
                try
                {
                    ThemeTemplate temp = new ThemeTemplate()
                    {
                        Theme = themeCS.Theme,
                        Background = themeCS.Background,
                        SecondBackground = themeCS.SecondBackground,
                        Foreground = themeCS.Foreground,
                        Accent = themeCS.Accent,
                        Outline = themeCS.Outline,
                        Faded = themeCS.Faded
                    };

                    string json = JsonSerializer.Serialize(temp, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(path, json);
                }
                catch
                {
                    this.Toast("Error", "There was an error :(");
                }
            });
        }

        private void readCosmetics(string path)
        {
            try
            {
                string temp = File.ReadAllText(path);
                cosmeticsCS = JsonSerializer.Deserialize<CosmeticsTemplate>(temp);
            }
            catch
            {
                this.Toast("Error", "There was an error :(");
            }
        }

        private async void writeCosmetics(string path)
        {
            await Task.Run(() =>
            {
                try
                {
                    CosmeticsTemplate temp = new CosmeticsTemplate()
                    {

                    };

                    string json = JsonSerializer.Serialize(temp, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(path, json);
                }
                catch
                {
                    this.Toast("Error", "There was an error :(");
                }
            });
        }
        #endregion

        public DiscordRpcClient client = new DiscordRpcClient("832806990953840710");

        public static bool initialized = false;

        public void Initialize()
        {
            if (File.Exists(@"C:\temp\VentileClient\Presets\Config.txt"))
            {
                readConfig(@"C:\temp\VentileClient\Presets\Config.json");
            }
            else
            {
                try
                {
                    client.Dispose();
                }
                catch
                {

                }
            }

            if (configCS.RichPresence)
            {
                client = new DiscordRpcClient("832806990953840710");

                Thread.Sleep(100);
                client.Initialize();

                //Set the rich presence
                //Call this as many times as you want and anywhere in your code.
                try
                {
                    if (configCS.RpcButton)
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Idling In Launcher...",
                            State = configCS.RpcText,
                            Timestamps = new Timestamps(),
                            Assets = new Assets()
                            {
                                LargeImageKey = "logo",
                                LargeImageText = Properties.Ventile.Default.Version,
                            },
                            Buttons = new DiscordRPC.Button[]
                            {
                                new DiscordRPC.Button() { Label = configCS.RpcButtonText, Url = configCS.RpcButtonLink },
                                new DiscordRPC.Button() { Label = "Ventile's Server", Url = "https://discord.gg/mCyHtD9twt" }
                            }
                        });
                    }
                    else
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Idling In Launcher...",
                            State = configCS.RpcText,
                            Timestamps = new Timestamps(),
                            Assets = new Assets()
                            {
                                LargeImageKey = "logo",
                                LargeImageText = Properties.Ventile.Default.Version,
                            },
                            Buttons = new DiscordRPC.Button[]
                            {
                                new DiscordRPC.Button() { Label = "Ventile's Server", Url = "https://discord.gg/mCyHtD9twt" }
                            }
                        });
                    }
                }
                catch
                {
                    this.Toast("Rich Presence", "There was an error with RPC");
                }
            }
        }

        public void Deinitialize()
        {
            Thread.Sleep(500);
            try
            {
                Thread.Sleep(150);
                client.Dispose();
            }
            catch
            {

            }
        }

        //Home
        public void Home()
        {
            if (File.Exists(@"C:\temp\VentileClient\Presets\Config.txt"))
            {
                readConfig(@"C:\temp\VentileClient\Presets\Config.json");
            } else
            {
                try
                {
                    Thread.Sleep(150);
                    client.Dispose();
                }
                catch
                {

                }
            }

            if (configCS.RichPresence)
            {
                try
                {
                    if (configCS.RpcButton)
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Idling In Launcher...",
                            State = configCS.RpcText,
                            Timestamps = new Timestamps(),
                            Assets = new Assets()
                            {
                                LargeImageKey = "logo",
                                LargeImageText = Properties.Ventile.Default.Version,
                            },
                            Buttons = new DiscordRPC.Button[]
                        {
                        new DiscordRPC.Button() { Label = configCS.RpcButtonText, Url = configCS.RpcButtonLink },
                        new DiscordRPC.Button() { Label = "Ventile's Server", Url = "https://discord.gg/mCyHtD9twt" }
                        }
                        });
                    }
                    else
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Idling In Launcher...",
                            State = configCS.RpcText,
                            Timestamps = new Timestamps(),
                            Assets = new Assets()
                            {
                                LargeImageKey = "logo",
                                LargeImageText = Properties.Ventile.Default.Version,
                            },
                            Buttons = new DiscordRPC.Button[]
                        {
                        new DiscordRPC.Button() { Label = "Ventile's Server", Url = "https://discord.gg/mCyHtD9twt" }
                        }
                        });
                    }
                    this.Toast("Rich Prescence", "In the Launcher!");
                }
                catch
                {
                    this.Toast("Rich Presence", "There was an error with RPC");
                }
            }
        }

        //Settings
        public void Settings()
        {

            if (File.Exists(@"C:\temp\VentileClient\Presets\Config.txt"))
            {
                readConfig(@"C:\temp\VentileClient\Presets\Config.json");
            }
            else
            {
                try
                {
                    Thread.Sleep(150);
                    client.Dispose();
                }
                catch
                {

                }
            }

            if (configCS.RichPresence)
            {
                try
                {
                    if (configCS.RpcButton)
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Changing Settings...",
                            State = configCS.RpcText,
                            Timestamps = new Timestamps(),
                            Assets = new Assets()
                            {
                                LargeImageKey = "logo",
                                LargeImageText = Properties.Ventile.Default.Version,
                            },
                            Buttons = new DiscordRPC.Button[]
                            {
                                new DiscordRPC.Button() { Label = configCS.RpcButtonText, Url = configCS.RpcButtonLink },
                                new DiscordRPC.Button() { Label = "Ventile's Server", Url = "https://discord.gg/mCyHtD9twt" }
                            }
                        });
                    }
                    else
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Changing Settings...",
                            State = configCS.RpcText,
                            Timestamps = new Timestamps(),
                            Assets = new Assets()
                            {
                                LargeImageKey = "logo",
                                LargeImageText = Properties.Ventile.Default.Version,
                            },
                            Buttons = new DiscordRPC.Button[]
                            {
                                new DiscordRPC.Button() { Label = "Ventile's Server", Url = "https://discord.gg/mCyHtD9twt" }
                            }
                        });
                    }
                }
                catch
                {
                    this.Toast("Rich Presence", "There was an error with RPC");
                }
            }
        }

        //In Game
        public void InGame()
        {
            if (File.Exists(@"C:\temp\VentileClient\Presets\Config.txt"))
            {
                readConfig(@"C:\temp\VentileClient\Presets\Config.json");
            }
            else
            {
                try
                {
                    Thread.Sleep(150);
                    client.Dispose();
                }
                catch
                {

                }
            }

            if (configCS.RichPresence)
            {
                try
                {
                    if (configCS.RpcButton)
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "In the Game!",
                            State = configCS.RpcText,
                            Timestamps = new Timestamps(),
                            Assets = new Assets()
                            {
                                LargeImageKey = "logo",
                                LargeImageText = Properties.Ventile.Default.Version,
                            },
                            Buttons = new DiscordRPC.Button[]
                            {
                                new DiscordRPC.Button() { Label = configCS.RpcButtonText, Url = configCS.RpcButtonLink },
                                new DiscordRPC.Button() { Label = "Ventile's Server", Url = "https://discord.gg/mCyHtD9twt" }
                            }
                        });
                    }
                    else
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "In the Game!",
                            State = configCS.RpcText,
                            Timestamps = new Timestamps(),
                            Assets = new Assets()
                            {
                                LargeImageKey = "logo",
                                LargeImageText = Properties.Ventile.Default.Version,
                            },
                            Buttons = new DiscordRPC.Button[]
                            {
                                new DiscordRPC.Button() { Label = "Ventile's Server", Url = "https://discord.gg/mCyHtD9twt" }
                            }
                        });
                    }

                    this.Toast("Rich Prescence", "In the Game!");
                }
                catch
                {
                    this.Toast("Rich Presence", "There was an error with RPC");
                }
            }
        }
    }
}
