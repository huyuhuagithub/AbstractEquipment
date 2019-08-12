using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbstractEquipment;
using AbstractEquipment.RS232Equipment;
namespace 映台Dome
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //SerialPort sp = new SerialPort("COM6", 19200, Parity.None, 8, StopBits.One);
            //sp.Open();
            //sp.Write(Datatb.Text);
            //Thread.Sleep(400);
            //Displytb.Text = sp.ReadExisting();
            //Thread.Sleep(200);
            //sp.Close();

        }

        private void CancelQure_Click(object sender, EventArgs e)
        {

        }

        private void Sendbut_Click(object sender, EventArgs e)
        {
            Displytb.Text = "";
            safeInvoke(NewMethod);
        }

        private void NewMethod()
        {
            AbstractRS232 abstractRS232 = new J050yingtai();
            SerialPort j050sp = abstractRS232.initializeRS232("COM4", 115200, "\r\n");
            Displytb.Text = abstractRS232.ReadQuery(j050sp, Datatb.Text);
            abstractRS232.CancelSerialPort(j050sp);
        }

        public void safeInvoke(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "提示!");
            }
        }
    }
}
