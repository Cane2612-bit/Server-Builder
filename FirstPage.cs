using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Canes_client
{

    public partial class FirstPage : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        public FirstPage()
        {
            InitializeComponent();
        }

        private void FirstPage_Load(object sender, EventArgs e)
        {
            //Transparent Panels
            Title.BackColor = Color.FromArgb(156, 4, 8, 10);
            SideBar.BackColor = Color.FromArgb(156, 4, 8, 10);
            Updates.BackColor = Color.FromArgb(156, 14, 18, 29);
            News.BackColor = Color.FromArgb(156, 14, 18, 29);

        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Title_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Title_Paint(object sender, PaintEventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string discordurl = "https://discord.gg/p6THpC4Hrf"; // Change this link if new server is made

            try
            {
                // Use Process.Start to open the default web browser with the specified URL
                Process.Start(discordurl);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur when trying to open the link
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
