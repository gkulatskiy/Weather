using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Weather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //Создаем список.
            MessageBox.Show("Выберите город со списка или введите его, смотреть пример списка!");
            comboBox1.Items.Add("Dnipro");
            comboBox1.Items.Add("Lviv");
            comboBox1.Items.Add("Kiev");
            comboBox1.Items.Add("Moscow");
            comboBox1.Items.Add("London");
            comboBox1.Items.Add("Paris");
            comboBox1.Items.Add("Madrid");
            comboBox1.Items.Add("Berlin");
          
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Делаем проверку.
            try
            {
                var text = comboBox1.Text;
                //Запрашиваем информацию с сайта и обрабатываем ее, "text" - город.
                WebRequest request = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?q=" + text + "&APPID=c6f3879e5fc0ee88f1660bd6eb4334f3");
                request.Method = "Post";
                request.ContentType = "application/x-www-urlencoded";
                WebResponse response = await request.GetResponseAsync();
                string ans = string.Empty;
                using (Stream s = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(s))
                    {
                        ans = await reader.ReadToEndAsync();
                    }
                }
                response.Close();
                richTextBox1.Text = ans;

                AllWeather AW = JsonConvert.DeserializeObject<AllWeather>(ans);

                panel1.BackgroundImage = AW.weather[0].Icon;

                //Вывод.
                label1.Text = AW.weather[0].main;
                label3.Text = AW.main.temp.ToString("0.##") + "C";
                label4.Text = AW.main.humidity.ToString() + "%";
                label5.Text = AW.main.temp_max.ToString("0.##") + "C";
                label6.Text = AW.wind.speed.ToString() + "m/s";
                label7.Text = AW.main.temp_min.ToString("0.##") + "C";
                label8.Text = text;
            }
            catch
            {
                MessageBox.Show("Такого города нет в базе. Повторите попытку ввода английскими символами!");
            }
        }

        
    }
}
