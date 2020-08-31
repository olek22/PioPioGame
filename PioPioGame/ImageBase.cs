using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PioPioGame
{
    class ImageBase //: IDisposable
    {
        bool disposed = false;

        Bitmap _bitmap;
        private int X;
        private int Y;

        public int Left { get { return X; } set { X = value; } } //odlaeglosc od lewej
        public int Top { get { return Y; } set { Y = value; } } //odleglosc od prawej

        public ImageBase(Bitmap _resource)//konstruktor image base- tworzy pusta bitmape
        {
            _bitmap = new Bitmap(_resource);
        }

        public void DrawImage(Graphics gfx)//rysuje na bitmapie w miescu X i Y
        {
            gfx.DrawImage(_bitmap, X, Y);
        }
        /*
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                _bitmap.Dispose();
            }
            disposed = true;
        }

    */

    }
}
