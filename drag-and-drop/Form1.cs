using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drag_and_drop {
    public partial class Form1 : Form {
        

        List<Ship> ships = new List<Ship>();

        bool isMouseDown = false; //is the mouse button down on the picturebox
        bool isKeyDown = false;

        public Form1() {
            this.KeyPreview = true;
            InitializeComponent();

            addShips(3);
        }

        //addRects
        private void addShips(int num) {
            Ship ship = new Ship(num, 60, 1 + num);
            ships.Add(ship);
        }

        //get selected ship
        private Ship getSelectedShip() { 
            foreach(Ship ship in ships) {
                if (ship.selected) return ship;
            }
            
            return null;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e) {
            //brush
            SolidBrush brush = new SolidBrush(Color.LightGray);
            //draw ships
            foreach(Ship ship in ships) {
                //if ship is selected highlight
                if (ship.selected) {
                    brush.Color = Color.Yellow;
                } else {
                    brush.Color = Color.LightGray;
                }


                e.Graphics.FillRectangle(brush, ship.rectangle);

                
            }
        }

        private void refreshButton_Click(object sender, EventArgs e) {
            pictureBox.Refresh();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e) {
            Console.WriteLine(isMouseDown);

            Console.WriteLine(e.Location.X + " " + e.Location.Y);

            Point mouse = new Point(e.Location.X, e.Location.Y);

            //check if mouse is already down
            if(!isMouseDown) {
                //set mouse down tracker
                isMouseDown = true;

                //check if any ship is selected
                foreach(Ship ship in ships) {
                    Console.WriteLine(" mouse: " + mouse.X + " " + mouse.Y + " || ship " + ship.rectangle.Location.X + " " + ship.rectangle.Location.Y);
                    Console.WriteLine("Range X: " + ship.rectangle.Location.X + " - " + (ship.rectangle.Location.X + ship.rectangle.Width));
                    Console.WriteLine("Range Y: " + ship.rectangle.Location.Y + " - " + (ship.rectangle.Location.Y + ship.rectangle.Height));

                    //see if mouse is in ships rectangle
                    if (mouse.X <= ship.rectangle.Location.X + ship.rectangle.Width && mouse.X >= ship.rectangle.Location.X &&
                       mouse.Y <= ship.rectangle.Location.Y + ship.rectangle.Height && mouse.Y >= ship.rectangle.Location.Y) {
                        ship.selected = true;
                    } else {
                        ship.selected = false;
                    }

                    Console.WriteLine("Selected: " + ship.selected);
                }
            }

            pictureBox.Refresh();
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e) {
            //deselect all ships
            foreach (Ship ship in ships) ship.selected = false;

            //set mouse down tracker
            isMouseDown = false;

            //refresh the picturebox
            pictureBox.Refresh();

        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e) {
            //check if mouse is down
            if(isMouseDown) {
                foreach(Ship ship in ships) {
                    if(ship.selected) {
                        ship.rectangle.X = e.Location.X - ship.rectangle.Width / 2; //move ship
                        ship.rectangle.Y = e.Location.Y - ship.rectangle.Height / 2;
                    }
                }

                //refresh picturebox
                pictureBox.Refresh();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            Console.WriteLine("KeyPressed" + e.KeyCode);

            //check if key is down
            if(!isKeyDown) {
                //set key to down
                isKeyDown = true;

                //rotate
                if (e.KeyCode == Keys.R) {
                    //rotate
                    Ship selectedShip = getSelectedShip();

                    if (selectedShip != null) selectedShip.rotate();

                    //refresh
                    pictureBox.Refresh();
                } 
                

            } else {
                //set key up
                isKeyDown = false;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {
            isKeyDown = false;
            pictureBox.Refresh();

        }
    }
}
