using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Sea_Battle
{
    public partial class Form1 : Form
    {
        //pBoxes drawing consts
        const int DRAGGING_OVER_FORM = 0;
        const int DRAGGING_OVER_FIELD = 1;
        const int DRAGGING_OVER_PROHIBITED = 2;
        //field tiles consts
        const int TILE_UNKOWN = 0;
        const int TILE_EMPTY = 1;
        const int TILE_UNHARMED_SHIP = 2;
        const int TILE_HARMED_SHIP = 3;
        const int TILE_SUNKEN_SHIP = 4;
        const int TILE_SUNKEN_SHIP_SURROUNDINGS = 5;
        /*
         *0 - туман войны(пустая клетка), 1 - раскрытая пустая клетка (попадание)
         *2 - клетка (невредимая) корабля
         *3 - клетка попадания, 4 - клетка потопленного корабля
         */

        Point location = Point.Empty;
        int tilesize = 40;
        int offset = 1;
        Pen ShipPen = new Pen(Color.Blue, 3);
        SolidBrush ShipBrush = new SolidBrush(Color.White);
        Bitmap map, coordinates;
        Graphics g, c;
        Ship[] ships = new Ship[10];
        DoubleBufferedPictureBox[] shipsPictureBoxes = new DoubleBufferedPictureBox[10];
        int[,] field = new int[10, 10];
        string dictionary = ".АБВГДЕЖЗИК";
        Font font = new Font(
           "Times New Roman",
           24,
           FontStyle.Regular,
           GraphicsUnit.Pixel);
        SolidBrush brush = new SolidBrush(Color.Black);
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int tcounter = 0;
            Pen pen = new Pen(Color.LightGray, 1);
            map = new Bitmap(YourFieldPBox.Width, YourFieldPBox.Height);
            coordinates=new Bitmap(CoordinatesPBox.Width, CoordinatesPBox.Height);
            g = Graphics.FromImage(map);c = Graphics.FromImage(coordinates);
            c.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            c.TextRenderingHint = TextRenderingHint.AntiAlias;
            for (int i=1;i<=10;i++)
            {
                c.DrawString(dictionary[i].ToString(), font,brush,i * 40+10, 10);
                c.DrawString(i.ToString(), font,brush,15-(i/10)*5, i * 40 + 10);
            }
            CoordinatesPBox.Image = coordinates;
            for (int i = 0; i < 4; i++) { CreateShips(4 - i, tcounter); tcounter += (4 - i); }
            for (int j = 0; j < 10; j++) shipsPictureBoxes[j].BringToFront();
            for (int x=0;x < 10;x++) for (int y=0;y<10;y++) field[x,y] = TILE_EMPTY;
            for (int x = 0; x <= 10; x++) { 
                g.DrawLine(pen, x * tilesize + offset, offset, x * tilesize + offset, 
                    YourFieldPBox.Width-offset); }
            for (int y = 0; y <= 10; y++) { 
                g.DrawLine(pen, offset, y * tilesize + offset, 
                    YourFieldPBox.Height - offset, y * tilesize + offset); }
            YourFieldPBox.Image = map;
        }

        private void CreateShips(int count, int startingPoint)
        {
            for (int i = startingPoint; i < count+ startingPoint; i++)
            {
                shipsPictureBoxes[i] = new DoubleBufferedPictureBox{
                    Location = new Point(488, 145 + (count) * 46), 
                    Size = new Size(40*(5-count)-offset, 40-offset),
                BorderStyle = BorderStyle.None};
                shipsPictureBoxes[i].MouseDown += new MouseEventHandler(this.ShipsPBox_MouseDown);
                shipsPictureBoxes[i].MouseMove += new MouseEventHandler(this.ShipsPBox_MouseMove);
                shipsPictureBoxes[i].MouseUp += new MouseEventHandler(this.ShipsPbox_MouseUp);
                shipsPictureBoxes[i].MouseClick += new MouseEventHandler(this.ShipsPBox_MouseClick);
                shipsPictureBoxes[i].MouseDoubleClick += new MouseEventHandler(this.ShipsPBox_MouseClick);
                shipsPictureBoxes[i].Paint += new PaintEventHandler(this.ShipOutline_Paint);
                shipsPictureBoxes[i].Tag = DRAGGING_OVER_FORM;
                Controls.Add(shipsPictureBoxes[i]);
                ships[i] = new Ship(5-count, shipsPictureBoxes[i]);
            }
        }

        private void RevertSize(DoubleBufferedPictureBox pBox)
        {
            pBox.Size = new Size(pBox.Height, pBox.Width);
        }
        private void ShipsPBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
            if (location == Point.Empty) return;
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            Point relocation = pBox.Location;
            if (CheckIfInField(pBox))
            {
                if (CheckIfProhibited(pBox))
                    pBox.Tag = DRAGGING_OVER_PROHIBITED;
                else
                    pBox.Tag = DRAGGING_OVER_FIELD;
                relocation.X += e.X - location.X; relocation.X -= relocation.X % tilesize; relocation.X += 25 + offset*2;
                relocation.Y += e.Y - location.Y; relocation.Y -= relocation.Y % tilesize; relocation.Y += 20 + offset*2;
            }
            else
            {
                pBox.Tag = DRAGGING_OVER_FORM;
                relocation.X += e.X - location.X;
                relocation.Y += e.Y - location.Y;
            }
            pBox.Location = relocation;
            Refresh();
        }
        private void ShipsPBox_MouseClick(object sender, MouseEventArgs e)
        {
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            if (CheckIfInField(pBox) && e.Button == MouseButtons.Right)
            {
                
                FillOnField(pBox, TILE_EMPTY);
                RevertSize(pBox);
                if (CheckIfProhibited(pBox))
                { 
                    RevertSize(pBox);
                    FillOnField(pBox, TILE_UNHARMED_SHIP);
                    pBox.Tag = DRAGGING_OVER_FORM; 
                    return; 
                }
                FillOnField(pBox, TILE_UNHARMED_SHIP);
                update_testLabel();
                pBox.Refresh();
            }
        }

        private void ShipsPBox_MouseDown(object sender, MouseEventArgs e)
        {
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            pBox.BringToFront();
            Cursor.Clip = Bounds;
            if (e.Button == MouseButtons.Left) location = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left&&CheckIfInField(pBox))
                FillOnField(pBox, TILE_EMPTY);
            update_testLabel();
        }

        private void ShipsPbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            Cursor.Clip = new Rectangle();
            pBox.Tag = DRAGGING_OVER_FORM;
            Refresh();
            location = Point.Empty;
            if (!CheckIfInField(pBox)||CheckIfProhibited(pBox))
            {
                pBox.Tag = DRAGGING_OVER_FORM;
                if (pBox.Height>pBox.Width)
                    pBox.Size=new Size(pBox.Height, pBox.Width);
                Refresh();
                pBox.Location = new Point(488, 145 + (4 - pBox.Width / 40) * 46);
            }
            else
            {
                FillOnField(pBox, TILE_UNHARMED_SHIP);
                update_testLabel();
            }

        }
        private bool CheckIfInField(DoubleBufferedPictureBox pBox)
        {
            if ((pBox.Left > YourFieldPBox.Left && pBox.Right < YourFieldPBox.Right)
                && (pBox.Top > YourFieldPBox.Top && pBox.Bottom < YourFieldPBox.Bottom))
                return true;
            return false;
        }
        private bool CheckIfProhibited(object sender)
        {
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            int txp = (pBox.Location.X - 65) / 40;
            int txy = (pBox.Location.Y - 140) / 40;
            int width = pBox.Width / 40 + 1;
            int height = pBox.Height/ 40 + 1;
            Debug.WriteLine(dictionary[txp + 1] + " " + (txy + 1).ToString()+ " " + width.ToString() + " " +
                height.ToString());
                for (int x = Math.Max(txp - 1, 0); x <= Math.Min(txp + width, 9); x++)
                    for (int y = Math.Max(txy - 1, 0); y <= Math.Min(txy + height, 9); y++)
                        if (field[x, y] != 1)
                        {
                            pBox.Tag = DRAGGING_OVER_PROHIBITED;
                            return true;
                        }
            return false;
        }
        private void FillOnField(object sender, int filler)
        {
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            int tx = (pBox.Location.X - 65) / 40;
            int ty = (pBox.Location.Y - 140) / 40;
            if (pBox.Width >= pBox.Height)
                for (int i = 0; i <= pBox.Width / 40; i++)
                    field[tx + i, ty] = filler;
            else
                for (int i = 0; i <= pBox.Height / 40; i++)
                    field[tx, ty + i] = filler;
        }


        private void update_testLabel()
        {
            string tstring = "";
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    tstring += field[y, x].ToString() + " ";
                }
                tstring += '\n';
            }
            testLabel.Text = tstring;
        }
        private void ShipOutline_Paint(object sender, PaintEventArgs e)
        {
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            {
                int thickness = 3;
                int halfThickness = thickness / 2;
                Pen p=new Pen(Color.Black);switch (pBox.Tag)
                {
                    case DRAGGING_OVER_FORM:
                        p = new Pen(Color.Blue, thickness);
                        break;
                    case DRAGGING_OVER_FIELD:
                        p = new Pen(Color.LightGreen, thickness);
                        break;
                    case DRAGGING_OVER_PROHIBITED:
                        p = new Pen(Color.Red, thickness);
                        break;
                }
                using (p)
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                         halfThickness,
                                                         pBox.ClientSize.Width - thickness,
                                                         pBox.ClientSize.Height - thickness));
                }
            }
        }

    }
}
