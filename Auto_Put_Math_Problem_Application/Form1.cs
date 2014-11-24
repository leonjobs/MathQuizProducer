using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Auto_Put_Math_Problem_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int para_one, para_two;//两个因数
        string para_op;//操作符
        int Machine_Cop_Result;//机器结果数
        Random random_Number = new Random();

        bool HaveNumIn = false;
        bool OnlyNum = true;
        int RadioNumberSelected;
        static Regex regexInText = new Regex("^-?((\\d|\b)+)$");

        int timeLeft; //用于倒数计时

        int NumOfRightQuiz = 0;//连对题目个数

        int WaNum = 0;
        int RaNum = 0;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!regexInText.IsMatch(textBox1.Text) && textBox1.Text != "")
            {
                MessageBox.Show("请输入数字，小数向下取整");
                OnlyNum = false;

            }
            else
                OnlyNum = true;

        }
        //private void listbox1_DrawItem(object sender, DrawItemEventArgs e)
        //{

        //    string tempstr = listBox1.Items[e.Index].ToString();
        //    int tempstrLen = tempstr.Length;
        //    if (tempstr.Substring(tempstr.Length - 3, 3) == "-10")//如果扣分
        //    {

        //        e.Graphics.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
        //        e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font,
        //            new SolidBrush(Color.Black), e.Bounds);
        //    }
        //    else
        //    {
        //        e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
        //        e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font,
        //            new SolidBrush(Color.Black), e.Bounds);
        //    }
        //    e.DrawFocusRectangle();
        //}  

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r') && HaveNumIn && textBox1.Text != "" && OnlyNum)
            {
                string str = textBox1.Text;

                double d = double.Parse(str);
                string disp = "" + para_one + para_op + para_two + "=" + str + " ";
                if (d == Machine_Cop_Result) // if( Math.Abs(d-result)< 1e-3 )
                {
                    disp += " ✓";
                    disp += "       ------  +10";
                    NumOfRightQuiz++;
                    RaNum++;
                }
                else
                {
                    disp += " ╳";
                    disp += "       ------  -10"+"  Wrong Answer";
                    NumOfRightQuiz = 0;
                    WaNum++;
                }
                if (NumOfRightQuiz > 0 && NumOfRightQuiz % 5 == 0)
                {
                    MessageBox.Show("☺ 恭喜您，已经连续答对"+NumOfRightQuiz.ToString()+"道");
                }
                listBox1.Items.Add(disp);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.SelectedIndex = -1;
                label5.Text = "正确：" + RaNum.ToString() + " " + "错误："+WaNum.ToString();
                HaveNumIn = false;
                if (checkBox1.Checked == true)
                    button1.PerformClick();
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//出题按钮事件
        {
            if (checkBox1.Checked && checkBox2.Checked)
            {
                timeLeft = 25;
                timeLabel.Text = "25 seconds";
                progressBar2.Maximum = 1000;//设置最大长度值
                progressBar2.Value = 0;//设置当前值
                progressBar2.Step = progressBar2.Maximum / timeLeft;//设置每次增长多少
                timer1.Start();
            }
            else
            {
                timeLabel.Text = "剩余时间";
                progressBar2.Value = 0;
            }
            if (radioButton1.Checked)
                RadioNumberSelected = 10;
            else if (radioButton2.Checked)
                RadioNumberSelected = 20;
            else if (radioButton3.Checked)
                RadioNumberSelected = 30;
            else
                RadioNumberSelected = 0;
            if (RadioNumberSelected != 0)
            {
                OnlyNum = true;
                para_one = random_Number.Next(RadioNumberSelected) + 1;
                para_two = random_Number.Next(RadioNumberSelected) + 1;
                int c = 0;
                if (radioButton4.Checked)
                    c = random_Number.Next(2);
                else if (radioButton5.Checked)
                    c = random_Number.Next(2) + 2;
                else if (radioButton6.Checked)
                    c = random_Number.Next(4);
                else
                    c = random_Number.Next(2);
                switch (c)
                {
                    case 0: para_op = "+"; Machine_Cop_Result = para_one + para_two; break;
                    case 1: para_op = "-"; Machine_Cop_Result = para_one - para_two; break;
                    case 2: para_op = "*"; Machine_Cop_Result = para_one * para_two; break;
                    case 3: para_op = "÷"; Machine_Cop_Result = para_one / para_two; break;
                }

                label1.Text = para_one.ToString();
                label2.Text = para_op;
                label3.Text = para_two.ToString();
                textBox1.Text = "";
                HaveNumIn = true;

                progressBar1.Maximum = 100;//设置最大长度值
                progressBar1.Value = 0;//设置当前值
                progressBar1.Step = 100;//设置每次增长多少

                progressBar1.Value += progressBar1.Step;//让进度条增加一次
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton4.Checked = true;
            radioButton1.Checked = true;
           // listBox1.DrawMode = DrawMode.OwnerDrawFixed; // 属性里设置  
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (timeLeft > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
                progressBar2.Value += progressBar2.Step;//让进度条增加一次
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
              //System.Threading.Thread.Sleep(500);
                button1.PerformClick();
            }
        }
    }
}
