using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle
{
    public class Ship
    {
        public int size;
        public PictureBox pBox;
        public Ship(int tsize, PictureBox tpBox) 
        {
            this.size = tsize;
            this.pBox = tpBox;
        }

    }
}
