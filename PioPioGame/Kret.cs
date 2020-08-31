using PioPioGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PioPioGame
{
    class Kret : ImageBase 
    {
        private Rectangle _obszarDoStrzalu = new Rectangle();//tworzenie obszaru
        
        
        private static Bitmap obrazekLos() //losownie obrazka z resources (zasobow)
        {
            Random r = new Random();
            int liczba = r.Next(0, 5);
            Bitmap wylosowany;

            Bitmap[] tablica = new Bitmap[] { Resources.manu1, Resources.juventus1, Resources.inter1,Resources.roma1,Resources.napoli1 };

            wylosowany = tablica[liczba];

            return wylosowany;
        }
        //stara wersja z jednym kretem
        /*
        public Kret() : base(obrazekLos())
        {
            _obszarDoStrzalu.X = Left + 10;
            _obszarDoStrzalu.Y = Top +10;
            _obszarDoStrzalu.Width = 25;
            _obszarDoStrzalu.Height = 40;
        }

        public void Update(int X, int Y)
        {

            Left = X;
            Top = Y;
            _obszarDoStrzalu.X = Left + 10;
            _obszarDoStrzalu.Y = Top +10;
        }
        */
        public Kret(int X,int Y) : base(obrazekLos())//konstruktor kreta
        {
            Left = X;
            Top = Y;
            //okreslenie obszaru obrazka z kretem
            _obszarDoStrzalu.X = Left + 10;
            _obszarDoStrzalu.Y = Top + 10;
            _obszarDoStrzalu.Width = 25;
            _obszarDoStrzalu.Height = 40;
        }



        public bool Hit(int X, int Y)
        {
            Rectangle c = new Rectangle(X, Y, 1, 1);

            if(_obszarDoStrzalu.Contains(c))
            {
                return true;//trafienie kreta
            }
            return false;
        }
    }
}
