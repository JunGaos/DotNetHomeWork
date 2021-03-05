using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Complex
{
    public partial class Calculator : Form
    {
        private decimal leftNumber; //第一次输入的数
        private String operatorStr; //运算符
        private Boolean isDot; //小数点是否已输入过

        public Calculator()
        {
            InitializeComponent();
        }
        //按下+、-、*、/按钮
        private void bntOperation_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex("^[0-9.]*$");
            if (!regex.IsMatch(txtBResult.Text))
            {
                MessageBox.Show("输入数据不合法");
                txtBResult.Focus();
                return;
            } //判断是否只含数字和小数点
            leftNumber = Decimal.Parse(txtBResult.Text);
            txtBResult.Text = "0";
            isDot = false;
            Button btn = (Button)sender;
            if (btn.Equals(btnPlus))
                operatorStr = "+";
            else if (btn.Equals(btnMinus))
                operatorStr = "-";
            else if (btn.Equals(btnMod))
                operatorStr = "%";
            else if (btn.Equals(btnMultioly))
                operatorStr = "*";
            else
                operatorStr = "/";
        }
        //按下=按钮
        private void bntEqual_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex("^[0-9.]*$");
            if (!regex.IsMatch(txtBResult.Text))
            {
                MessageBox.Show("输入数据不合法");
                txtBResult.Focus();
                return;
            } //判断是否只含数字和小数点
            decimal crtNumber = Decimal.Parse(txtBResult.Text);
            switch (operatorStr)
            {
                case "+": txtBResult.Text = (crtNumber + leftNumber).ToString(); break;
                case "-": txtBResult.Text = (leftNumber - crtNumber).ToString(); break;
                case "*": txtBResult.Text = (crtNumber * leftNumber).ToString(); break;
                case "/": txtBResult.Text = (leftNumber / crtNumber).ToString(); break;
                case "%": txtBResult.Text = (leftNumber % crtNumber).ToString(); break;
            }
            
        }
        //按下数字键
        private void btn_Click(object sender, EventArgs e)
        {
            int t = 0;
            Button btn = (Button)sender;
            if (btn.Equals(btn1))
                t = 1;
            else if (btn.Equals(btn1))
                       t = 1;
            else if(btn.Equals(btn2))
                       t = 2;
            else if(btn.Equals(btn3))
                       t = 3;
            else if (btn.Equals(btn4))
                       t = 4;
            else if (btn.Equals(btn5))
                       t = 5;
            else if (btn.Equals(btn6))
                       t = 6;
            else if (btn.Equals(btn7))
                       t = 7;
            else if (btn.Equals(btn8))
                       t = 8;
            else if (btn.Equals(btn9))
                       t = 9;
            if (txtBResult.Text == "0")
                txtBResult.Text = t.ToString();
            else 
                txtBResult.Text = txtBResult.Text + t.ToString();
        }
        //按下BackSpace按钮
        private void bntBackSpace_Click(object sender, EventArgs e)
        {
            if (txtBResult.Text.Substring(0, txtBResult.TextLength - 1) == "")
                txtBResult.Text = "0";
            else
            {
                if (txtBResult.Text[txtBResult.TextLength - 1] == '.')
                    isDot = false;
                txtBResult.Text = txtBResult.Text.Substring(0, txtBResult.TextLength - 1);
            }
        }
        //按下Clear按钮
        private void bntClear_Click(object sender, EventArgs e)
        {
            txtBResult.Text = "0";
        }
        //按下小数点.按钮
        private void bntDot_Click(object sender, EventArgs e)
        {
            if (isDot == false)
            {
                isDot = true;
                txtBResult.Text += ".";
            }
        }

    }
}
