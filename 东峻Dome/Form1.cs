﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbstractEquipment.CANEquipment;
using BaseModule.Helper;
using BaseModule.Helper.ConvertFrom;

namespace 东峻Dome
{
  

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Sendbut_Click(object sender, EventArgs e)
        {
            //Displytb.Text = "";
            //CANAbstract USBCAN2I = new USBCAN_2I();
            //Displytb.Text = CANG(USBCAN2I, Datatb.Text);
            //List<byte> sbytet = new List<byte> { 160, 151, 12, 12, 19, 255, 01, 00, 02, 03 };
            //Displytb.Text = ConvertFrom.ByteArrayToHexString(sbytet.ToArray());



            //Displytb.Text = ConvertFrom.ByteArrayToString(new byte[] { 48, 49, 50, 51, 52, 53, 65, 66, 67, 68 }, Encoding.ASCII);
            string btadd = "21 62 09 00 00 0D E9 08 22 62 A5 B7 00 00 00 00 22 62 A5 B7 00 00 00 00";
           string ssss= substrings(btadd);
            btadd.Substring(0);
               string ss = ConvertFrom.HexStringToString("ec a0 45 2d 43 61 72 78 ec a0 45 2d 43 61 72 78", Encoding.ASCII);
            
            Displytb.Text = ss;
            //77B 91 70 35 00 31 39 30 33 


            //string ss = "31 39 30 33 32 36 31 38 30 33 32 38 31 30 31 31 34 2E 31 32 36 34 4E 30 30 30 33 30 2E 34 37 38 30 45 30 30 2E 38 30 30 30 2E 38 30 30 30 30 33 35 00 00 00";

            //Displytb.Text += ConvertFrom.HexStringToString(ss, Encoding.ASCII);

            CANAbstract USBCAN2I = new USBCAN_2I();
            CANG(USBCAN2I, Datatb.Text);


            //test test1 = new test();
            //test1.TestItem.Add("123", "test1");
            //test1.TestItem.Add("345", "test2");
            //test1.TestItem.Add("456", "test3");

            //test1.CANIndex = 0;B
            //string s = JsonHelper.ObjToJsonString(test1);
            //WriteLog.WriteLogFile(s);
            //test test2 = JsonHelper.JsonToObj<test>(s);
            ////test test1 = new test();
            //test1 = SimpleFactory.Create<test>();
            //foreach (var item in test1.TestItem)
            //{
            //    string key = item.Key;
            //    string value = item.Value;
            //}

        }
        string newstr = null;
        public string substrings(string str)
        {
            string repp = str.Replace(" ", "");
            newstr = repp.Remove(repp.Length / 2, 4);
            int i = newstr.Length;
            if (repp.Length > repp.Length-4)
            {
                substrings(newstr);
            }
            return newstr.Substring(8,newstr.Length-8);
        }
        
        public string  CANG<T>(T t) where T : CANAbstract
        {
            string dataResult="";
            test test1 = new test();
            test1 = SimpleFactory.Create<test>();
            t.Query("ec a0 45 2d 43 61 72 78", 0x77B, test1.deviceType, test1.DevicesIndex, test1.CANIndex, 0x77a);
            Thread.Sleep(100);
            if (t.initializeCAN(test1.deviceType, test1.DevicesIndex, test1.CANIndex, 0X1C))
            {
                foreach (var item in test1.TestItem)
                {
                    string key = item.Key;
                    string value = item.Value;
                    dataResult = t.Query(value, 0x77B, test1.deviceType, test1.DevicesIndex, test1.CANIndex, 0x77a);
                    WriteLog.WriteLogFile(key + value + "\r\n" + dataResult);
                    updateTextBoxUI(Displytb, key + value + "\r\n" + dataResult + "\r\n");
                }
                t.CancelCAN(test1.deviceType, test1.DevicesIndex, test1.CANIndex);
                return dataResult;
            }
            else
            {
                return "初始化失败!";
            }
        }

        public string CANG<T>(T t, string data) where T : CANAbstract
        {
            string dataResult = "";
            test test1 = new test();
            test1 = SimpleFactory.Create<test>();

            if (t.initializeCAN(test1.deviceType, test1.DevicesIndex, test1.CANIndex, 0X1C))
            {
                t.Query("ec a0 45 2d 43 61 72 78", 0x77B, test1.deviceType, test1.DevicesIndex, test1.CANIndex, 0x77a);
                Thread.Sleep(100);
                dataResult = t.Query(data, 0x77B, test1.deviceType, test1.DevicesIndex, test1.CANIndex, 0x77a);
                updateTextBoxUI(Displytb, data + "\r\n" + dataResult + "\r\n");
                t.CancelCAN(test1.deviceType, test1.DevicesIndex, test1.CANIndex);
                return dataResult;
            }
            else
            {
                return "初始化失败!";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Displytb.Text = "";
            Action action = new Action(NewMethod);
            action.BeginInvoke(callback => { button1.Invoke(new Action(() => { button1.Enabled = true; })); }, null);
            button1.Enabled = false;
        }

        private void NewMethod()
        {
            CANAbstract USBCAN2I = new USBCAN_2I();
            CANG(USBCAN2I);

            //updateTextBoxUI(Displytb, "查询开机完成:11 B5 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 B5 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（ACC电压)信息:11 C0 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C0 00 00 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "设置（音量)信息:11 83 02 01 14 00 00 00\r\n" + CANG(USBCAN2I, "11 83 02 01 14 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（私有CAN)信息:11 BD 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 BD 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询(IHU MCU)版本信息:11 12 01 02 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 02 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（软件MPU)版本信息:11 12 01 03 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 03 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（硬件)版本信息:11 12 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 01 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（高德地图包)版本信息:11 12 01 04 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 04 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（MCUboot)版本信息:11 12 01 0D 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 0D 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（倒车Flash)版本信息:11 12 01 0E 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 0E 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（ICCID)版本信息:11 13 01 02 00 00 00 00\r\n" + CANG(USBCAN2I, "11 13 01 02 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IMSI)版本信息:11 13 01 03 00 00 00 00\r\n" + CANG(USBCAN2I, "11 13 01 03 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（MSISND)版本信息:11 13 01 04 00 00 00 00\r\n" + CANG(USBCAN2I, "11 13 01 04 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（XDSN)版本信息:11 A2 02 03 01 00 00 00\r\n" + CANG(USBCAN2I, "11 A2 02 03 01 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IHUID)版本信息:11 A2 02 03 04 00 00 00\r\n" + CANG(USBCAN2I, "11 A2 02 03 04 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（UUID 配置字)信息:11 A2 02 03 06 00 00 00\r\n" + CANG(USBCAN2I, "11 A2 02 03 06 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（打开蓝牙)信息:11 60 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 60 01 01 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（蓝牙MAC地址)信息:11 62 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 62 00 00 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（进入USB界面)信息:11 50 01 09 00 00 00 00\r\n" + CANG(USBCAN2I, "11 50 01 09 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "设置（播放第一曲USB歌曲)信息:11 54 04 01 00 01 00 00\r\n" + CANG(USBCAN2I, "11 54 04 01 00 01 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（切换到收音状态)信息:11 50 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 50 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（FM97.5MHz)信息:11 40 03 03 16 26 00 00\r\n" + CANG(USBCAN2I, "11 40 03 03 16 26 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（4G信号强度)信息:11 31 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 31 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（4G自诊断)信息:11 32 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 32 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（清除DTC故障码)信息:11 B2 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 B2 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（DTC查询)信息:11 B2 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 B2 01 01 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（获取陀螺仪GYRO ID)信息:11 BC 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 BC 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（打开WIFI测试)信息:11 20 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 20 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（WIFI测试)信息:11 21 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 21 01 00 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（GPS 信息)信息:11 70 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 70 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（GPS 搜星)信息:11 71 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 71 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（查询第一路方控)信息:11 BA 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 BA 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（查询第二路方控)信息:11 BA 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 BA 01 01 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（Lin通信)信息:11 C6 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C6 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（雷达串口通信)信息:11 C7 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C7 00 00 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（USB过流)信息:11 CA 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 CA 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（音量值)信息:11 83 01 02 00 00 00 00\r\n" + CANG(USBCAN2I, "11 83 01 02 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（IO测试 CAR_ACC_DET)信息:11 C8 01 04 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C8 01 04 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 MTK_ACC_DET)信息:11 C8 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C8 01 01 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 MCU_PULES_DET)信息:11 C8 01 03 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C8 01 03 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 MTK_PULES_DET)信息:11 C8 01 02 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C8 01 02 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 MTK_VCC_DET)信息:11 C8 01 05 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C8 01 05 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 CSD_ACC-EN OFF)信息:11 C9 02 03 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C9 01 03 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 CSD_ACC-EN ON)信息:11 C9 02 03 01 00 00 00\r\n" + CANG(USBCAN2I, "11 C9 02 03 01 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（关闭蓝牙)信息:11 60 01 02 00 00 00 00\r\n" + CANG(USBCAN2I, "11 60 01 02 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（关闭WIFI)信息:11 20 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 20 01 01 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（清除蓝牙连接)信息:11 6B 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 6B 01 01 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（睡眠电流)信息:11 90 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 90 01 00 00 00 00 00") + "\r\n");

        }

        public static void updateTextBoxUI(TextBox textBox, string text)
        {
            textBox.Invoke(new Action(() =>
            {
                textBox.Text += text;
                textBox.Focus();//获取焦点
                textBox.Select(textBox.TextLength, 0);//光标定位到文本最后
                textBox.ScrollToCaret();//滚动到光标处
            }));
        }

        public void updatebutUI(Button button, string text)
        {
            button.Invoke(new Action(() => { button.Enabled = true; }));
        }
        int i = 0;
        public void updatedgvUI(DataGridView textBox, string text)
        {
            textBox.Invoke(new Action(() => { textBox.Rows.Add(i++,text); }));
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CANAbstract USBCAN2I = new USBCAN_2I();
            USBCAN2I.CancelCAN(4, 0, 0);
            USBCAN2I.CancelCAN(4, 0, 1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            test test1 = new test();
            test1 = SimpleFactory.Create<test>();
            label1.Text = test1.deviceType + "--" + test1.DevicesIndex + "--" + test1.CANIndex;
        }
    }
}
