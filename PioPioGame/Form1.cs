

using PioPioGame.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PioPioGame
{
    //zakomentowany kod- wczesniejsza wersja programu gdzie mamy jednego kreta; obecnie ciągle sie zmienia :)
    public partial class PioPioGame : Form
    {
        bool krew=false;

        int _krewTime=0;

        //wyniki
        int _trafienia =0;
        int _pudla = 0;
        public int _strzaly = 0;
        double _srednia = 0;



        int iks = 0;
        int igrek = 0;

        Kret _kret;
        Krew _krew;
        Menu _menu;
        Wanted _wanted;
        Wanted2 _wanted2;

        Random r = new Random();
        public PioPioGame()
        {
            //usuniecie błedu migotania obrazu
            this.DoubleBuffered = true;

            InitializeComponent();
  
            //tworzenie kursora
            Bitmap b = new Bitmap(Resources.celownik);
            this.Cursor = Kursor.CreateCursor(b, b.Height / 2, b.Width / 2);

            _menu = new Menu() { Left = 466, Top = 10 };
            //_kret = new Kret() { Left = 10, Top = 220 };
            _kret = new Kret(
                r.Next(-3, 520),
                r.Next(255, 332)
            );
            _wanted = new Wanted() { Left = 150, Top = 130 };
            _wanted2 = new Wanted2() { Left = 300, Top = 40 };
            _krew = new Krew();
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            updateKret();

            if(krew)
            {
                if(_krewTime>=1.5)
                {
                    krew = false;
                    _krewTime = 0;
                    updateKret();
                }
                _krewTime++;
            }



            this.Refresh();
        }

        private void updateKret()//aktualizacja- tworzenie nowego kreta
        {
            /*
            _kret.Update(
                r.Next(-3,520),
                r.Next(255,332)
            );
            */
            _kret = new Kret(
                r.Next(-3, 520),
                r.Next(255, 332)
            ); 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;

            if (krew==true)
            {
                _krew.DrawImage(dc);
            }
            else
            {
                _kret.DrawImage(dc);
            }

            //dodanie wlasnej czcionki
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(@"DFMincho.ttf");

            TextFormatFlags flags = TextFormatFlags.Left;
            Font font = new Font(pfc.Families[0], 14, FontStyle.Bold);

            //wspolrzedne- potrzebne do ustalania obszarow-pomocniczo; w rozgrywce można ukryc
            //TextRenderer.DrawText(dc, "X=" + iks.ToString() + " Y=" + igrek.ToString(), font, new Rectangle(0, 0, 220, 50), SystemColors.ControlText, flags);

            base.OnPaint(e);

            //wyniki
            TextRenderer.DrawText(dc, "STRZALY=" + _strzaly.ToString(), font, new Rectangle(0, 110, 220, 40), SystemColors.ControlText, flags);
            TextRenderer.DrawText(dc, "TRAFIENIA=" + _trafienia.ToString(), font, new Rectangle(0, 130, 220, 40), SystemColors.ControlText, flags);
            TextRenderer.DrawText(dc, "PUDLA=" + _pudla.ToString(), font, new Rectangle(0, 150, 220, 40), SystemColors.ControlText, flags);
            TextRenderer.DrawText(dc, "SREDNIA=" + _srednia.ToString("F0")+"%", font, new Rectangle(0, 170, 220, 40), SystemColors.ControlText, flags);
            TextRenderer.DrawText(dc, "POZIOM="+Poziom(), font, new Rectangle(0, 92, 260, 40), SystemColors.ControlText, flags);

            //rysownanie przygotownych obrazkow
            _menu.DrawImage(dc);
            _wanted.DrawImage(dc);
            _wanted2.DrawImage(dc);
        }

        private void PioPioGame_MouseMove(object sender, MouseEventArgs e)
        {
            iks = e.X;
            igrek = e.Y;

            this.Refresh();    
        }

        private void PioPioGame_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.X>483 && e.X<563 && e.Y>25 && e.Y < 56)//START
            {
                Click();
                timerGameLoop.Start();
            }
            else if(e.X > 499 && e.X < 563 && e.Y > 57 && e.Y < 84)//STOP
            {
                Click();
                timerGameLoop.Stop();
            }
            else if (e.X > 481 && e.X < 563 && e.Y > 88 && e.Y < 117)//RESET
            {
                Click();
                _trafienia = 0;
                _pudla = 0;
                _strzaly = 0;
                _srednia = 0;
            }
            else if (e.X > 451 && e.X < 563 && e.Y > 119 && e.Y < 148)//WYJSCIE
            {
                Click();
                System.Threading.Thread.Sleep(800);
                System.Windows.Forms.Application.Exit();   
            }
            else
            {
                if(_kret.Hit(e.X,e.Y))
                {
                    krew = true;
                    _krew.Left = _kret.Left - Resources.krew.Width/3+5;
                    _krew.Top = _kret.Top - Resources.krew.Height/3;

                    _trafienia++;
                }
                else
                {
                    _pudla++;
                }
                _strzaly = _pudla+_trafienia;
                _srednia = ((double)_trafienia / (double)_strzaly)*100.0;
                Dzwiek();
            }
        }

        private void Dzwiek()
        {
            SoundPlayer strzal = new SoundPlayer(Resources.shot);
            strzal.Play();
        }

        private void Click()
        {
            SoundPlayer klik= new SoundPlayer(Resources.click);
            klik.Play();
        }

        //poziomy
        private String Poziom()
        {
            if(_strzaly <= 7)
            {
                return "NOWICJUSZ";
            }
            else if((_srednia >= 0 && _srednia < 20)&& _strzaly > 7)
            {
                return "NOOB";
            }
            else if ((_srednia >= 20 && _srednia < 40) && _strzaly > 7)
            {
                return "BEZ SZALU";
            }
            else if ((_srednia >= 40 && _srednia < 60)&& _strzaly > 7)
            {
                return "SREDNIO";
            }
            else if ((_srednia >= 60 && _srednia < 80) && _strzaly > 7)
            {
                return "CALKIEM DOBRZE";
            }
            else if ((_srednia >= 80 && _srednia < 90) && _strzaly > 7)
            {
                return "SUPPER";
            }
            else if ((_srednia >= 90 && _srednia < 100) && _strzaly > 7)
            {
                return "STRZELEC WYBOROWY";
            }


            return null;
        }



    }
}
