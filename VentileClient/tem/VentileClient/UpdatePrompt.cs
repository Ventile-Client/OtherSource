using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VentileClient
{
    public partial class UpdatePrompt : Form
    {
        private Updater ths;
        private MainWindow ths2;

        public UpdatePrompt(Updater frm, MainWindow frm2)
        {
            InitializeComponent();
            ths = frm;
            ths2 = frm2;
        }

        private void no_Click(object sender, EventArgs e)
        {
            ths2.Opacity = 0;
            ths.TopMost = false;
            TopMost = false;
            ths2.TopMost = true;
            ths2.TopMost = false;

            for (int i = 0; i < 10; i++)
            {
                ths2.Opacity += 0.1;
                Thread.Sleep(10);
            }
            ths.Close();
            this.Close();
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"C:\temp\VentileClient\Ventile-Updater.exe");
            }
            catch (Exception err)
            {
                MessageBox.Show("Please open Ventile-Updater.exe manually\n   Error: " + err.Message, "Error", MessageBoxButtons.OK);
            }
            finally
            {
                ths2.Close();
            }
        }

        private void UpdatePrompt_Load(object sender, EventArgs e)
        {
            string[] VersionLines = System.IO.File.ReadAllLines(@"C:\temp\VentileClient\Version.txt");
            version.Text = VersionLines[0];
        }
    }
}
