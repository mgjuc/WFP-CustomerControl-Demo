using DataSelecterDemo.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataSelecterDemo
{
    internal class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow mainWindow = new();
            //mainWindow.Show(); 不等待窗口关闭
            mainWindow.ShowDialog();    //等待窗口关闭才会返回
        }
    }
}
