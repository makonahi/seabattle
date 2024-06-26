using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Diagnostics;

namespace Sea_Battle
{
    public class DoubleBufferedPictureBox : PictureBox
    {
        const int TILE_UNKOWN = Constants.TILE_UNKOWN;
        const int TILE_EMPTY = Constants.TILE_EMPTY;
        const int TILE_UNHARMED_SHIP = Constants.TILE_UNHARMED_SHIP;
        const int TILE_HARMED_SHIP = Constants.TILE_HARMED_SHIP;
        const int TILE_SUNKEN_SHIP = Constants.TILE_SUNKEN_SHIP;
        const int TILE_SUNKEN_SHIP_SURROUNDINGS = Constants.TILE_SUNKEN_SHIP_SURROUNDINGS;

        const int tilesize = Constants.tilesize;
        const string dictionary = Constants.dictionary;
        const int offset = Constants.offset;

        const int DRAGGING_OVER_FORM = Constants.DRAGGING_OVER_FORM;
        const int DRAGGING_OVER_FIELD = Constants.DRAGGING_OVER_FIELD;
        const int DRAGGING_OVER_PROHIBITED = Constants.DRAGGING_OVER_PROHIBITED;

        /// <summary>
        /// Импорт переменных из других классов.
        /// </summary>
        static int left = Main.left;
        static int right = Main.right;
        static int top = Main.top;
        static int bottom = Main.bottom;

        Point location = Point.Empty;

        public DoubleBufferedPictureBox()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            this.Tag = DRAGGING_OVER_FORM;
            this.Paint += new PaintEventHandler(this.ShipOutline_Paint);
            this.MouseDown += new MouseEventHandler(this.ShipsPBox_MouseDown);
            this.MouseMove += new MouseEventHandler(this.ShipsPBox_MouseMove);
            this.MouseUp += new MouseEventHandler(this.ShipsPbox_MouseUp);
            this.MouseClick += new MouseEventHandler(this.ShipsPBox_MouseClick);
            this.MouseDoubleClick += new MouseEventHandler(this.ShipsPBox_MouseClick);
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
                relocation.X += e.X - location.X; relocation.X -= relocation.X % tilesize; relocation.X += 25 + offset * 2;
                relocation.Y += e.Y - location.Y; relocation.Y -= relocation.Y % tilesize; relocation.Y += 20 + offset * 2;
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
                if (CheckIfProhibited(pBox)||!CheckIfInField(pBox))
                {
                    RevertSize(pBox);
                    FillOnField(pBox, TILE_UNHARMED_SHIP);
                    pBox.Tag = DRAGGING_OVER_FORM;
                    return;
                }
                FillOnField(pBox, TILE_UNHARMED_SHIP);
                pBox.Refresh();
            }
        }

        private void ShipsPBox_MouseDown(object sender, MouseEventArgs e)
        {
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            pBox.BringToFront();
            if (e.Button == MouseButtons.Left) location = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left && CheckIfInField(pBox))
                FillOnField(pBox, TILE_EMPTY);
        }

        private void ShipsPbox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            pBox.Tag = DRAGGING_OVER_FORM;
            Refresh();
            location = Point.Empty;
            if (!CheckIfInField(pBox) || CheckIfProhibited(pBox))
            {
                pBox.Tag = DRAGGING_OVER_FORM;
                if (pBox.Height > pBox.Width)
                    pBox.Size = new Size(pBox.Height, pBox.Width);
                Refresh();
                pBox.Location = new Point(488, 145 + (4 - pBox.Width / 40) * 46);
            }
            else
            {
                FillOnField(pBox, TILE_UNHARMED_SHIP);
            }
        }
        private bool CheckIfInField(DoubleBufferedPictureBox pBox)
        {
            if ((pBox.Left > left && pBox.Right < right)
                && (pBox.Top > top && pBox.Bottom < bottom))
                return true;
            return false;
        }
        private bool CheckIfProhibited(object sender)
        {
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            int txp = (pBox.Location.X - 65) / 40;
            int txy = (pBox.Location.Y - 140) / 40;
            int width = pBox.Width / 40 + 1;
            int height = pBox.Height / 40 + 1;
            for (int x = Math.Max(txp - 1, 0); x <= Math.Min(txp + width, 9); x++)
                for (int y = Math.Max(txy - 1, 0); y <= Math.Min(txy + height, 9); y++)
                    if (Main.field[x, y] != 1)
                    {
                        pBox.Tag = DRAGGING_OVER_PROHIBITED;
                        return true;
                    }
            return false;
        }
        private void FillOnField(object sender, int filler)
        {
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            int tx = (pBox.Location.X - left) / 40;
            int ty = (pBox.Location.Y - top) / 40;
            if (pBox.Width >= pBox.Height)
                for (int i = 0; i <= pBox.Width / 40; i++)
                    Main.field[tx + i, ty] = filler;
            else
                for (int i = 0; i <= pBox.Height / 40; i++)
                    Main.field[tx, ty + i] = filler;
        }

        private void ShipOutline_Paint(object sender, PaintEventArgs e)
        {
            DoubleBufferedPictureBox pBox = sender as DoubleBufferedPictureBox;
            {
                int thickness = 3;
                int halfThickness = thickness / 2;
                Pen p = new Pen(Color.Black); switch (pBox.Tag)
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
