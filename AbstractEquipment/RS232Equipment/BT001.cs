using AbstractEquipment.RS232Equipment;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbstractEquipment
{
    public class BT001 : AbstractRS232
    {
        StringBuilder str = new StringBuilder();
        public override void CancelSerialPort(SerialPort serialPort)
        {
            serialPort.Close();
        }

        public override SerialPort initializeRS232(string portName, int BaudRatio, string NewLine)
        {
            SerialPort serialPort = new SerialPort() { PortName = portName, BaudRate = BaudRatio, DataBits = 8, StopBits = StopBits.One, Parity = Parity.None, NewLine = NewLine };
            try
            {
                if (!serialPort.IsOpen)
                {
                    IsOpen = true;
                    serialPort.Open();
                    serialPort.DataReceived += SerialPort_DataReceived;
                }
                else
                {
                    serialPort.Close();
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e); ;
            }
            return serialPort;
        }

        public override string Read(SerialPort serialPort)
        {
              return str.ToString();
        }

        public  override string ReadQuery(SerialPort serialPort,string command)
        {
            //str.Clear();
            serialPort.WriteLine(command);
            Thread.Sleep(1300);
            return str.ToString();
        }

        
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = sender as SerialPort;
            str.Append(port.ReadExisting());
        }

        public override void WriteCommand(SerialPort serialPort, string command)
        {
            if (!IsOpen==true)
            {
                serialPort.Open();
            }
            else
            {
                serialPort.WriteLine(command);
            }
        }

    }
}
