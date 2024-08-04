using Sea_Battle;
using System;
using System.Drawing;
using System.Windows.Forms;

public class IPAddressTextBox : RichTextBox
{
    private const int ByteLength = 3;
    private readonly Color defaultByteColor = Color.Gray;

    public IPAddressTextBox()
    {
        this.MaxLength = 15; 
        this.Text = "255.255.255.255";
        this.ForeColor = defaultByteColor;
        this.Font = Main.DefaultFont;
    }

}
