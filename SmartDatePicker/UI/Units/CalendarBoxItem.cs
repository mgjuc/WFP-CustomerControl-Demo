using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartDatePicker.UI.Units
{
   
    public class CalendarBoxItem : ListBoxItem
    {
        /// <summary>
        /// 保存当前显示日期
        /// </summary>
        public DateTime Date;

        /// <summary>
        /// 绑定ListBox的selectedValuePath，方便设置选中
        /// </summary>
        public string SelectedValueStr { get; set; }

        static CalendarBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarBoxItem), new FrameworkPropertyMetadata(typeof(CalendarBoxItem)));
        }



        /// <summary>
        /// 当前显示的值是否为本月，可能是上月或者下月的日期
        /// </summary>
        public bool IsCurrentMonth
        {
            get { return (bool)GetValue(IsCurrentMonthProperty); }
            set { SetValue(IsCurrentMonthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCurrentMonth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCurrentMonthProperty =
            DependencyProperty.Register("IsCurrentMonth", typeof(bool), typeof(CalendarBoxItem), new PropertyMetadata(false));


    }
}
