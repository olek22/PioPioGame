using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using PioPioGame.Properties;

namespace PioPioGame
{
    public partial class Form2 : Form
    {
        Thread th;
        int iks = 0;
        int igrek = 0;

        public Form2()
        {
            //usuniecie błedu migotania obrazu
            this.DoubleBuffered = true;
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics dc = e.Graphics;
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(@"DFMincho.ttf");

            TextFormatFlags flags = TextFormatFlags.Left;
            Font font = new Font(pfc.Families[0], 14, FontStyle.Bold);
            //TextRenderer.DrawText(dc, "X=" + iks.ToString() + " Y=" + igrek.ToString(), font, new Rectangle(0, 0, 220, 50), SystemColors.ControlText, flags);

            base.OnPaint(e);
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            iks = e.X;
            igrek = e.Y;
            this.Refresh();
        }


        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            //po kliknieciu "Przejdz do gry" zamyka okno Form2 i uruchamia Form1
            if (e.X > 170 && e.X < 374 && e.Y > 280 && e.Y < 304)
            { 
                Click();
                this.Cursor = Cursors.WaitCursor;
                System.Threading.Thread.Sleep(800);
                this.Close();
                th = new Thread(StartPio);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();

            }
        }

        private void Click()
        {
            SoundPlayer klik = new SoundPlayer(Resources.click);
            klik.Play();
        }

        private void StartPio()
        {
            Application.Run(new PioPioGame());
        }

    }
}
