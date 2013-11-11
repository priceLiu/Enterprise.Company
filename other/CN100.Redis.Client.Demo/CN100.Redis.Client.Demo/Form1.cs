using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CN100.Redis.Client.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CN100.Redis.Client.RedisClientUtility.GetData<CN100.ProductDetail.BLL.Model.ProductInfoModel>("Pro_Info_35798");
            MessageBox.Show(CN100.Redis.Client.RedisClientUtility.GetData("ceshi"));
        }
    }
}
