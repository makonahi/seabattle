using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public static class Constants
{
    // 
    // Значения ячеек поля с кораблями. Значения:
    // 0 - туман войны(пустая клетка)
    // 1 - раскрытая пустая клетка (попадание)
    // 2 - клетка (невредимая) корабля
    // 3 - клетка попадания
    // 4 - клетка потопленного корабля
    public enum tileState
    {
        TILE_UNKOWN,
        TILE_EMPTY,
        TILE_UNHARMED_SHIP,
        TILE_HARMED_SHIP,
        TILE_SUNKEN_SHIP,
        TILE_SUNKEN_SHIP_SURROUNDINGS
    }

    // Константы для отрисовки поля и координатной сетки.
    public const string dictionary = ".АБВГДЕЖЗИК";
    public const int tilesize = 40;
    public const int offset = 1;
    public const int coordinatesOffset = 40;

    // Тэги PictureBox`ов
    public enum pBoxState
    {
        DRAGGING_OVER_FORM,
        DRAGGING_OVER_FIELD,
        DRAGGING_OVER_PROHIBITED,
        IDLE_ON_FIELD
    }

    // Шрифты

    public static Font h1 = new Font(
       "Times New Roman",
       24,
       FontStyle.Regular,
       GraphicsUnit.Pixel);
    public static Font h2 = new Font(
       "Microsoft Sans Serif",
       16,
       FontStyle.Regular,
       GraphicsUnit.Pixel);
}

