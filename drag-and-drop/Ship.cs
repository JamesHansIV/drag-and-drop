using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace drag_and_drop {
    class Ship {
        public Rectangle rectangle; //rectangle used in drawing, will be passed in methods to picturebox
        public bool selected;       //is the ship selected
        public bool isRotated;      //true when ship is verticle

        public Ship(int length, int x, int y) {
            rectangle = new Rectangle(x, y, length * 60, 30);
            selected = false;
            isRotated = false;
        }

        public void rotate() {
            Console.WriteLine("rotate called!");
            int oldWidth = rectangle.Width;
            rectangle.Width = rectangle.Height;
            rectangle.Height = oldWidth;
            Console.WriteLine("Width: " + rectangle.Width + "Height: " + rectangle.Height);

            
        }


    }
}
