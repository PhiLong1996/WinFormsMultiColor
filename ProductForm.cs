﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsMultiColor
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
            
        }

        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.primaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.secondaryColor;
                }
            }
            label4.ForeColor = ThemeColor.secondaryColor;
            label5.ForeColor = ThemeColor.primaryColor;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }
    }
}
