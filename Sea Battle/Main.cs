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
        const int TILE_UNKOWN = Constants.TILE_UNKOWN;
        const int TILE_EMPTY = Constants.TILE_EMPTY;
        const int TILE_UNHARMED_SHIP = Constants.TILE_UNHARMED_SHIP;
        const int TILE_HARMED_SHIP = Constants.TILE_HARMED_SHIP;
        const int TILE_SUNKEN_SHIP = Constants.TILE_SUNKEN_SHIP;
        const int TILE_SUNKEN_SHIP_SURROUNDINGS = Constants.TILE_SUNKEN_SHIP_SURROUNDINGS;

        const int tilesize = Constants.tilesize;
        const string dictionary = Constants.dictionary;
        const int offset= Constants.offset;

        const int DRAGGING_OVER_FORM = Constants.DRAGGING_OVER_FORM;
        const int DRAGGING_OVER_FIELD = Constants.DRAGGING_OVER_FIELD;
        const int DRAGGING_OVER_PROHIBITED = Constants.DRAGGING_OVER_PROHIBITED;
        const int IDLE_ON_FIELD = Constants.IDLE_ON_FIELD;

        Pen ShipPen = new Pen(Color.Blue, 3);
        SolidBrush ShipBrush = new SolidBrush(SystemColors.Control);
        SolidBrush brush = new SolidBrush(Color.Black);

        Bitmap map, coordinates;
        Graphics g, c;

        Ship[] ships = new Ship[10];
        static DoubleBufferedPictureBox[] shipsPictureBoxes = new DoubleBufferedPictureBox[10];

        public static int[,] field = new int[10, 10];

        public static int left, right, bottom, top;

        public static System.Windows.Forms.Button acceptButton;

        Font h1 = new Font(
           "Times New Roman",
           24,
           FontStyle.Regular,
           GraphicsUnit.Pixel);
        Font h2 = new Font(
           "Microsoft Sans Serif",
           16,
           FontStyle.Regular,
           GraphicsUnit.Pixel);

        /// <summary>
        /// TEST
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                c.DrawString(dictionary[i].ToString(), h1,brush,i * 40+10, 10);
                c.DrawString(i.ToString(), h1,brush,15-(i/10)*5, i * 40 + 10);
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

        private void CreateShips(int count, int startingPoint)
        {
            for (int i = startingPoint; i < count+ startingPoint; i++)
            {
                shipsPictureBoxes[i] = new DoubleBufferedPictureBox{
                    Location = new Point(488, 145 + (count) * 46), 
                    Size = new Size(40*(5-count)-offset, 40-offset),
                BorderStyle = BorderStyle.None};
                Controls.Add(shipsPictureBoxes[i]);
                ships[i] = new Ship(5-count, shipsPictureBoxes[i]);
            }
        }
        public static void CheckButtonLock()
        {
            int counter = 0;
            for (int i = 0; i < 10; i++)
                if ((int)shipsPictureBoxes[i].Tag == IDLE_ON_FIELD)
                    counter++;
            Debug.WriteLine(counter);
            if (counter == 10)
                acceptButton.Enabled = !acceptButton.Enabled;
        }
        private void acceptButton_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < 10; j++)
            {
                Rectangle shipRect = new Rectangle(shipsPictureBoxes[j].Location.X - left + offset,
                    shipsPictureBoxes[j].Location.Y - top + offset, shipsPictureBoxes[j].Width - offset * 3,
                    shipsPictureBoxes[j].Height - offset * 3);
                g.FillRectangle(ShipBrush, shipRect);
                g.DrawRectangle(ShipPen, shipRect);
                shipsPictureBoxes[j].Dispose();
                shipsPictureBoxes[j] = null;
            }
            YourFieldPBox.Image = map;
            acceptButton.Dispose();
            hintLabel.Dispose();
            Refresh();
        }
        /// <summary>
        /// TEST
        /// </summary>
        void update_testLabel()
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

    }
}
