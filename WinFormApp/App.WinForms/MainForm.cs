﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Service;
using App.IService;

namespace App.Winforms
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AppService<IUserService>.Proxy.GetUserName(this.Name).ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AppService<IUserService>.Proxy.GetUserName2(this.Name).ToString());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
