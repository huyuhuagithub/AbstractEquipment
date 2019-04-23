using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbstractEquipment;
using AbstractEquipment.CANEquipment;
using AbstractEquipment.RS232Equipment;
using System.IO.Ports;
namespace BT盒Dome
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbstractRS232 abstractRS232 = new BT001();
            SerialPort serialPort = abstractRS232.initializeRS232("COM2", 9600, "\r\n");
            textBox1.Text= abstractRS232.ReadQuery(serialPort, "AT+MRST=1");
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
