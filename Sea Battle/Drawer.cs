using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.AxHost;

namespace Sea_Battle
{
    internal class Drawer
    {
        const int TILE_UNKOWN = (int)Constants.tileState.TILE_UNKOWN;
        const int TILE_EMPTY = (int)Constants.tileState.TILE_EMPTY;
        const int TILE_UNHARMED_SHIP = (int)Constants.tileState.TILE_UNHARMED_SHIP;
        const int TILE_HARMED_SHIP = (int)Constants.tileState.TILE_HARMED_SHIP;
        const int TILE_SUNKEN_SHIP = (int)Constants.tileState.TILE_SUNKEN_SHIP;
        const int TILE_SUNKEN_SHIP_SURROUNDINGS = (int)Constants.tileState.TILE_SUNKEN_SHIP_SURROUNDINGS;

        const int tilesize = Constants.tilesize;
        const int offset = Constants.offset;
        const int coordinatesOffset = Constants.coordinatesOffset;
        const string dictionary = Constants.dictionary;

        static private Pen redPen = new Pen(Color.Red, 1);

        static private Pen redHighlightPen = new Pen(Color.Red, 2);

        static private Pen pen = new Pen(Color.LightGray, 1);
        
        static private Brush brush = new SolidBrush(Color.Black);

        static private Pen ShipPen = new Pen(Color.Blue, 3);
        static private SolidBrush ShipBrush = new SolidBrush(SystemColors.Control);

        static int left = Main.left;
        static int right = Main.right;
        static int top = Main.top;
        static int bottom = Main.bottom;

        static private Font h1 = new Font(
           "Times New Roman",
           24,
           FontStyle.Regular,
           GraphicsUnit.Pixel);

        public Drawer() { }

        public static void Draw_Grid(Main.Visuals FV)
        {
            for (int x = 0; x <= 10; x++)
            {
                FV.g.DrawLine(pen, x * tilesize + offset + coordinatesOffset, 
                    offset + coordinatesOffset, 
                    x * tilesize + offset + coordinatesOffset,
                    FV.pBox.Width - offset);
            }
            for (int y = 0; y <= 10; y++)
            {
                FV.g.DrawLine(pen, offset + coordinatesOffset, 
                    y * tilesize + offset + coordinatesOffset,
                    FV.pBox.Height - offset, 
                    y * tilesize + offset + coordinatesOffset);
            }
            FV.pBox.Image = FV.map;
        }

        public static void Draw_Coordinates(Main.Visuals FV)
        {
            for (int i = 1; i <= 10; i++)
            {
                FV.g.DrawString(dictionary[i].ToString(), h1, brush, i * tilesize + 10, 10);
                FV.g.DrawString(i.ToString(), h1, brush, 15 - (i / 10) * 5, i * tilesize + 10);
            }
            FV.pBox.Image = FV.map;
        }

        public static void Draw_Accepted_Ships(Main.Visuals FV)
        {
            for (int j = 0; j < 10; j++)
            {
                Rectangle shipRect = new Rectangle(Main.shipsPBoxes[j].Location.X - left + offset,
                    Main.shipsPBoxes[j].Location.Y - top + offset, Main.shipsPBoxes[j].Width - offset * 3,
                    Main.shipsPBoxes[j].Height - offset * 3);
                FV.g.FillRectangle(ShipBrush, shipRect);
                FV.g.DrawRectangle(ShipPen, shipRect);
                Main.shipsPBoxes[j].Dispose();
                Main.shipsPBoxes[j] = null;
            }
            FV.pBox.Image = FV.map;
        }

        public static void Draw_Tile(Main.Visuals FV, int x, int y, int TILE_TYPE)
        {
            x *= tilesize; y *= tilesize;
            switch (TILE_TYPE)
            {
                case TILE_UNKOWN:
                    break;
                case TILE_EMPTY:
                    FV.g.FillRectangle(new SolidBrush(Color.White), x + 2, y + 2, tilesize - 1, tilesize - 1);
                    FV.g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(x + 17, y + 17, 8, 8));
                    FV.pBox.Image = FV.map;
                    break;
                case TILE_UNHARMED_SHIP:
                    break;
                case TILE_HARMED_SHIP:
                    FV.g.FillRectangle(new SolidBrush(Color.White), x + 2, y + 2, tilesize - 1, tilesize - 1);
                    x += 1; y += 1;
                    for (int i = 3; i < 40; i += 10)
                    {
                        FV.g.DrawLine(redPen, x + i, y, x, y + i);
                        FV.g.DrawLine(redPen, x + 40, y + i, x + i, y + 40);
                    }
                    FV.pBox.Image = FV.map;
                    break;
                case TILE_SUNKEN_SHIP:
                    break;
                case TILE_SUNKEN_SHIP_SURROUNDINGS:
                    FV.g.FillRectangle(new SolidBrush(Color.White), x + 2, y + 2, tilesize - 1, tilesize - 1);
                    x += 1; y += 1;
                    FV.g.DrawLine(redHighlightPen, x + 15, y + 15, x + 25, y + 25);
                    FV.g.DrawLine(redHighlightPen, x + 25, y + 15, x + 15, y + 25);
                    FV.pBox.Image = FV.map;
                    break;
            }
        }

        public static void Draw_FogOfWar(Main.Visuals FV)
        {
            SolidBrush warFogBrush = new SolidBrush(SystemColors.ControlLight);
            FV.g.FillRectangle(warFogBrush, coordinatesOffset + offset, coordinatesOffset + offset,
                FV.pBox.Width - coordinatesOffset - offset*2, 
                FV.pBox.Height - coordinatesOffset - offset*2);
            FV.pBox.Image = FV.map;
        }
    }
}
