using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkingPicture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Image open = null;
        Image closed = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDeviceCollection devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            comboBox1.Items.AddRange(devices.ToArray());
            textBox1.Text = 0 + "";
            textBox2.Text = 1 + "";
            textBox3.Text = 0 + "";
            textBox4.Text = 0 + "";
            textBox5.Text = 1000 + "";
            open = Image.FromFile("open.png");
            closed = Image.FromFile("closed.png");
            pictureBox1.Image = closed;
            timer1.Start();
        }

        int currentNoiseDuration = 0;
        int currentSilenceDuration = 0;
        float previousVol = 0.0f;
        float currentVol = 0.0f;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (selectedDevice != null)
            {
                //label1.Text = "not null";
                previousVol = currentVol;
                currentVol = selectedDevice.AudioMeterInformation.MasterPeakValue * 100f;
                label1.Text = "Current Vol: " + currentVol;
                if (absDiff > (currentVol - previousVol))
                {
                    if (currentVol > min && currentVol < max)
                    {
                        currentSilenceDuration = 0;
                        if (currentNoiseDuration < minConsNoiseDuration)
                        {
                            currentNoiseDuration++;
                            pictureBox1.Image = closed;
                            //label3.Text = "Close";
                        }
                        else
                        {
                            pictureBox1.Image = open;
                            //label3.Text = "Open";
                        }
                    }
                    else
                    {
                        if (currentSilenceDuration > minConsSilenceDuration)
                        {
                            pictureBox1.Image = closed;
                        }
                        else
                        {
                            currentSilenceDuration++;
                        }
                        currentNoiseDuration = 0;
                        pictureBox1.Image = closed;
                        //label3.Text = "Close";
                    }
                }
                else
                {
                    currentVol = previousVol;
                }
            }
            else {
                currentVol = 0.0f;
            }
        }

        MMDevice selectedDevice = null;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                selectedDevice = (MMDevice) comboBox1.SelectedItem;
            }
            else
            {
                selectedDevice = null;
            }
        }

        float min = 0.0f;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(!float.TryParse(textBox1.Text, out min))
            {
                textBox1.Text = min + "";
            }
        }

        float max = 0.0f;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!float.TryParse(textBox2.Text, out max))
            {
                textBox2.Text = max + "";
            }
        }

        int minConsNoiseDuration = 0;
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out minConsNoiseDuration))
            {
                textBox3.Text = minConsNoiseDuration + "";
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        int minConsSilenceDuration = 0;
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox4.Text, out minConsSilenceDuration))
            {
                textBox4.Text = minConsSilenceDuration + "";
            }
        }

        float absDiff = 0.0f;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!float.TryParse(textBox5.Text, out absDiff))
            {
                textBox5.Text = absDiff + "";
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            textBox4_TextChanged(sender, e);
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            textBox3_TextChanged(sender, e);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            textBox2_TextChanged(sender, e);
        }
    }
}
