using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    /// <summary>
    /// Класс для хранения констант использующихся в нескольких других классах.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Константы для хранения значения ячеек поля с кораблями. Значения:
        /// 0 - туман войны(пустая клетка)
        /// 1 - раскрытая пустая клетка (попадание)
        /// 2 - клетка (невредимая) корабля
        /// 3 - клетка попадания, 4 - клетка потопленного корабля
        /// </summary>
        public const int TILE_UNKOWN = 0;
        public const int TILE_EMPTY = 1;
        public const int TILE_UNHARMED_SHIP = 2;
        public const int TILE_HARMED_SHIP = 3;
        public const int TILE_SUNKEN_SHIP = 4;
        public const int TILE_SUNKEN_SHIP_SURROUNDINGS = 5;

        /// <summary>
        /// Константы для отрисовки поля и координатной сетки.
        /// </summary>
        public const string dictionary = ".АБВГДЕЖЗИК";
        public const int tilesize = 40;
        public const int offset = 1;

        /// <summary>
        /// Тэги PictureBox`ов
        /// </summary>
        public const int DRAGGING_OVER_FORM = 0;
        public const int DRAGGING_OVER_FIELD = 1;
        public const int DRAGGING_OVER_PROHIBITED = 2;
    }
}
