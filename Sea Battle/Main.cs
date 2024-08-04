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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace Sea_Battle
{
    public partial class Main : Form
    {
        const int TILE_UNKOWN = (int)Constants.tileState.TILE_UNKOWN;
        const int TILE_EMPTY = (int)Constants.tileState.TILE_EMPTY;
        const int TILE_UNHARMED_SHIP = (int)Constants.tileState.TILE_UNHARMED_SHIP;
        const int TILE_HARMED_SHIP = (int)Constants.tileState.TILE_HARMED_SHIP;
        const int TILE_SUNKEN_SHIP = (int)Constants.tileState.TILE_SUNKEN_SHIP;
        const int TILE_SUNKEN_SHIP_SURROUNDINGS = (int)Constants.tileState.TILE_SUNKEN_SHIP_SURROUNDINGS;

        const int tilesize = Constants.tilesize;
        const string dictionary = Constants.dictionary;
        const int offset= Constants.offset;
        const int coordinatesOffset = Constants.coordinatesOffset;

        const int DRAGGING_OVER_FORM = (int)Constants.pBoxState.DRAGGING_OVER_FORM;
        const int DRAGGING_OVER_FIELD = (int)Constants.pBoxState.DRAGGING_OVER_FIELD;
        const int DRAGGING_OVER_PROHIBITED = (int)Constants.pBoxState.DRAGGING_OVER_PROHIBITED;
        const int IDLE_ON_FIELD = (int)Constants.pBoxState.IDLE_ON_FIELD;

        Font h1=Constants.h1, h2 =Constants.h2;

        public struct Visuals
        {
            public PictureBox pBox;
            public Bitmap map;
            public Graphics g;
            public Visuals(PictureBox tpBox)
            {
                this.pBox = tpBox;
                map = new Bitmap(pBox.Width, pBox.Height);
                g = Graphics.FromImage(map);
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
            }
            
        }
        Visuals[] Fields_Visuals = new Visuals[2];
        public static int[][,] Fields_Values = new int[2][,];

        public static DoubleBufferedPictureBox[] shipsPBoxes = new DoubleBufferedPictureBox[10];
        Random rnd = new Random();
        public static int left, right, bottom, top;
        public static System.Windows.Forms.Button acceptButton = new System.Windows.Forms.Button();

        private void updaterTick_Tick(object sender, EventArgs e)
        {
            update_testLabel();
        }

        public Main()
        {
            InitializeComponent();

            left=YourFieldPBox.Left;
            right=YourFieldPBox.Right;
            bottom=YourFieldPBox.Bottom;
            top=YourFieldPBox.Top;

            Fields_Visuals[0] = new Visuals(YourFieldPBox);
            Fields_Visuals[1] = new Visuals(OpponentFieldPBox);

            Fields_Values[0] = new int[10, 10];
            Fields_Values[1] = new int[10, 10]; 

            acceptButton = new System.Windows.Forms.Button
            {
                Location = new Point(166, 565),
                Size = new Size(222, 44),
                Text = "Принять",
                Font = h1,
                Enabled = false,
            };
            acceptButton.MouseDown += new MouseEventHandler(this.acceptButton_Click);
            Controls.Add(acceptButton);

            hintLabel.Font = h2;
            hintLabel.Text = "Перетаскивайте корабли на поле.\nКак определитесь с расстановкой, нажмите \"Принять\".";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            int tcounter = 0;
            for (int i = 0; i < 4; i++) { CreateShips(4 - i, tcounter); tcounter += (4 - i); }
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    Fields_Values[0][x, y] = TILE_EMPTY;
            Drawer.Draw_FogOfWar(Fields_Visuals[1]);
            foreach (Visuals FieldVisual in Fields_Visuals)
            {
                Drawer.Draw_Coordinates(FieldVisual);
                Drawer.Draw_Grid(FieldVisual);
            }
        }

        private void OpponentFieldPBox_Click(object sender, EventArgs e)
        {
            Point cursor = this.PointToClient(Cursor.Position);
            int x = (cursor.X - OpponentFieldPBox.Left - coordinatesOffset - offset);
            int y = (cursor.Y - OpponentFieldPBox.Top - coordinatesOffset - offset);
            if (x < 0 || y < 0)
                return;
            x = Math.Min(x / tilesize, 9); y = Math.Min(y / tilesize, 9);
            Fields_Values[1][x, y] = TILE_EMPTY;
            Drawer.Draw_Tile(Fields_Visuals[1], x + 1, y + 1, rnd.Next(0, 6));
            update_testLabel();
        }


        private void acceptButton_Click(object sender, MouseEventArgs e)
        {
            Drawer.Draw_Accepted_Ships(Fields_Visuals[0]);
            acceptButton.Dispose();
            hintLabel.Dispose();
            Refresh();
        }

        private void CreateShips(int count, int startingPoint)
        {
            for (int i = startingPoint; i < count+ startingPoint; i++)
            {
                shipsPBoxes[i] = new DoubleBufferedPictureBox{
                    Location = new Point(488, 145 + (count) * 46), 
                    Size = new Size(tilesize * (5-count)-offset, tilesize - offset),
                BorderStyle = BorderStyle.None};
                Controls.Add(shipsPBoxes[i]);
                shipsPBoxes[i].BringToFront();
            }
        }
        public static void CheckButtonLock()
        {
            int counter = 0;
            for (int i = 0; i < 10; i++)
                if ((int)shipsPBoxes[i].Tag == IDLE_ON_FIELD)
                    counter++;
            if (counter == 10)
                acceptButton.Enabled = !acceptButton.Enabled;
        }

        void update_testLabel()
        {
            string tstring = "";
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    tstring += Fields_Values[0][y, x].ToString() + " ";
                }
                tstring += "                     ";
                for (int y = 0; y < 10; y++)
                {
                    tstring += Fields_Values[1][y, x].ToString() + " ";
                }
                tstring += '\n';
            }
            testLabel.Text = tstring;
        }

    }
}
