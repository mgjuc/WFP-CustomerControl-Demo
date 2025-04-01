using System;
using System.CodeDom.Compiler;
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

    public class SmartData : Control
    {
        private CalendarBox _calendarList;
        private CalendarSwitch _calendarSwitch;


        static SmartData()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SmartData), new FrameworkPropertyMetadata(typeof(SmartData)));
        }




        public DateTime? CurrentMonth
        {
            get { return (DateTime?)GetValue(CurrentMonthProperty); }
            set { SetValue(CurrentMonthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentMonth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentMonthProperty =
            DependencyProperty.Register("CurrentMonth", typeof(DateTime?), typeof(SmartData), new PropertyMetadata(null));




        /// <summary>
        /// 当前选中的日期
        /// </summary>
        public DateTime? SelectedDate
        {
            get { return (DateTime?)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(DateTime?), typeof(SmartData), new PropertyMetadata(null));




        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _calendarList = (CalendarBox)GetTemplateChild("CalendarList");
            _calendarSwitch = (CalendarSwitch)GetTemplateChild("CalendarSwitch");

            _calendarSwitch.Click += _switch_click;
            _calendarList.MouseLeftButtonUp += _calendarList_MouseLeftButtonUp;

            ChevronButton leftBtn = (ChevronButton)GetTemplateChild("BTN_Left");
            ChevronButton rightBtn = (ChevronButton)GetTemplateChild("BTN_Right");

            leftBtn.Click += (s, e) =>
            {
                GenerateCalendar(CurrentMonth?.AddMonths(-1) ?? DateTime.Now);
            };
            rightBtn.Click += (s, e) =>
            {
                GenerateCalendar(CurrentMonth?.AddMonths(1) ?? DateTime.Now);
            };
        }

        private void _calendarList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_calendarList.SelectedItem is CalendarBoxItem item)
            {
                SelectedDate = item.Date;
                GenerateCalendar(item.Date);
            }
        }

        private void _switch_click(object sender, RoutedEventArgs e)
        {
            if (_calendarSwitch.IsChecked == true)
            {
                GenerateCalendar(SelectedDate ?? DateTime.Now);
            }
        }

        private void GenerateCalendar(DateTime currentTime)
        {
            //月份没变不用重新填数据
            if (CurrentMonth?.ToString("yyyyMM") == currentTime.ToString("yyyyMM"))
            {
                return;
            }
            CurrentMonth = currentTime;

            _calendarList.Items.Clear();

            DateTime fDayofMonth = new DateTime(currentTime.Year, currentTime.Month, 1);
            DateTime lDayofMonth = fDayofMonth.AddMonths(1).AddDays(-1);


            /*
             * enum DayOfWeek
             * 
             Sunday = 0,
             Monday = 1,
             Tuesday = 2,
             Wednesday = 3
             Thursday = 4
             Friday = 5,
             Saturday = 6
             */
            int firstOffSet = (int)fDayofMonth.DayOfWeek;
            int lastOffSet = 6 - (int)lDayofMonth.DayOfWeek;

            //补全上下两个月的几天
            DateTime fday = fDayofMonth.AddDays(-firstOffSet);
            DateTime lday = lDayofMonth.AddDays(lastOffSet);

            for (DateTime day = fday; day <= lday; day = day.AddDays(1))
            {
                CalendarBoxItem item = new CalendarBoxItem();
                item.IsCurrentMonth = day.Month == currentTime.Month;
                item.Content = day.Day; //Day是int，在xaml里处理了转成string才能显示
                item.Date = day;
                item.SelectedValueStr = day.ToString("yyyyMMdd");
                _calendarList.Items.Add(item);
            }
            //翻月时设置选中日期
            //这里不用再判月变化了，因为第一次填充时SelectedDate是null，后面只有月变了才会填充
            if (SelectedDate != null)
            {
                //通过selectedValue的形式设置选中项，取巧。如果通过selectedItem会更麻烦
                _calendarList.SelectedValue = SelectedDate.Value.ToString("yyyyMMdd");
            }
        }
    }
}
