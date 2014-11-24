using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Auto_Put_Math_Problem_Application
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

       //public static int para_one, para_two;//两个因数
       //public static string para_op;//操作符
       //public static int Machine_Cop_Result;//机器结果数

       //public static Random random_Number = new Random();

    }
}
