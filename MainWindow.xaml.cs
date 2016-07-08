﻿using System;
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
using Time;
using PCManagment;
using System.Windows.Threading;
using WPFTimer.AdditionalButtons;
using WPFTimer.Enums;


namespace WPFTimer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {





        DispatcherTimer Timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            //после инициализации окна обновляется информация в textbox-ах.


            Timer.Tick += new EventHandler(Timer_tick);
            Timer.Interval = new TimeSpan(0, 0, 1);

            RefreshFavButtons(MemoryBuffer.FavTimeButtons, MemoryBuffer.FavDelButtons);

            MemoryBuffer.TotalTimeChanged += ChangeTextBoxData;

        }

        void ChangeTextBoxData(object sender, TimeEventArgs e)
        {

            HourTXTB.Text = MemoryBuffer.ReturnHours().ToString();
            MinTXTB.Text = MemoryBuffer.ReturnMinutes().ToString();
            SecTXTB.Text = MemoryBuffer.ReturnSeconds().ToString();


        }

        #region Изменения значения текстбоксов
        private void HourUpBTN_Click(object sender, RoutedEventArgs e)
        {

            MemoryBuffer.PlusHour();
        }

        private void HourDownBTN_Click(object sender, RoutedEventArgs e)
        {

            MemoryBuffer.MinusHour();
        }

        private void MinUpBTN_Click(object sender, RoutedEventArgs e)
        {

            MemoryBuffer.PlusMinute();
        }

        private void MinDownBTN_Click(object sender, RoutedEventArgs e)
        {

            MemoryBuffer.MinusMinute();
        }

        private void SecUpBTN_Click(object sender, RoutedEventArgs e)
        {

            MemoryBuffer.PlusSecond();
        }

        private void SecDownBTN_Click(object sender, RoutedEventArgs e)
        {

            MemoryBuffer.MinusSecond();
        }




        #endregion

        private void StartBTN_Click(object sender, RoutedEventArgs e)
        {

            switch (MemoryBuffer.CurrentState)
            {
                case TimerState.Off:
                    {
                        MemoryBuffer.StartingTime = MemoryBuffer.TotalSeconds;
                        if (MemoryBuffer.TotalSeconds != 0)
                        {
                            Timer.Start();
                            VisualUpdateTimerON();
                            MemoryBuffer.CurrentState = TimerState.On;

                        }
                    }
                    break;
                case TimerState.On:
                    {
                        Timer.Stop();
                        MemoryBuffer.TotalSeconds = MemoryBuffer.StartingTime;
                        VisualUpdateTimerOFF();
                        MemoryBuffer.CurrentState = TimerState.Off;
                    }
                    break;
                case TimerState.Paused:
                    {
                        MemoryBuffer.TotalSeconds = MemoryBuffer.StartingTime;


                        VisualUpdateTimerOFF();
                        MemoryBuffer.CurrentState = TimerState.Off;

                    }
                    break;
            }
        }
        private void Timer_tick(object sender, EventArgs e)
        {
            MemoryBuffer.TotalSeconds--;


            if (MemoryBuffer.TotalSeconds == 0)
            {
                MemoryBuffer.CurrentState = TimerState.Off;
                Timer.Stop();
                VisualUpdateTimerOFF();
                var warningWindow = new TurnOffWarning();
                warningWindow.AdditionalTimeButtonClick += ClickStartButton;
                warningWindow.ShowDialog();
            }
        }

        private void ClickStartButton(object sender, EventArgs e)
        {
            switch (MemoryBuffer.CurrentState)
            {
                case TimerState.Off:
                    {
                        MemoryBuffer.StartingTime = MemoryBuffer.TotalSeconds;
                        if (MemoryBuffer.TotalSeconds != 0)
                        {
                            Timer.Start();
                            VisualUpdateTimerON();
                            MemoryBuffer.CurrentState = TimerState.On;

                        }
                    }
                    break;
                case TimerState.On:
                    {
                        Timer.Stop();
                        MemoryBuffer.TotalSeconds = MemoryBuffer.StartingTime;
                        VisualUpdateTimerOFF();
                        MemoryBuffer.CurrentState = TimerState.Off;
                    }
                    break;
                case TimerState.Paused:
                    {
                        MemoryBuffer.TotalSeconds = MemoryBuffer.StartingTime;


                        VisualUpdateTimerOFF();
                        MemoryBuffer.CurrentState = TimerState.Off;

                    }
                    break;
            }
        }
        #region Визуальные преображения с включением таймера
        private void VisualUpdateTimerON()
        {
            BTNStartContent.Text = "Clear";
            PauseBTNContent.Text = "Pause";
            Grid.SetRow(HourTxtViwbx, 2);
            Grid.SetRowSpan(HourTxtViwbx, 4);
            Grid.SetRow(MinTxtViwbx, 2);
            Grid.SetRowSpan(MinTxtViwbx, 4);
            Grid.SetRow(SecTxtViwbx, 2);
            Grid.SetRowSpan(SecTxtViwbx, 4);
            Grid.SetColumnSpan(HourTxtViwbx, 2);
            Grid.SetColumnSpan(MinTxtViwbx, 2);
            Grid.SetColumnSpan(SecTxtViwbx, 2);
            hVewbx.Visibility = Visibility.Hidden;
            mVewBx.Visibility = Visibility.Hidden;
            sVewBx.Visibility = Visibility.Hidden;
            HourTxtViwbx.HorizontalAlignment = HorizontalAlignment.Center;
            MinTxtViwbx.HorizontalAlignment = HorizontalAlignment.Center;
            SecTxtViwbx.HorizontalAlignment = HorizontalAlignment.Center;
            MinSecDoubleDot.Visibility = Visibility.Visible;
            HourMinDoubleDot.Visibility = Visibility.Visible;


            HourDownBTN.Visibility = Visibility.Hidden;
            HourUpBTN.Visibility = Visibility.Hidden;
            MinDownBTN.Visibility = Visibility.Hidden;
            MinUpBTN.Visibility = Visibility.Hidden;
            SecDownBTN.Visibility = Visibility.Hidden;
            SecUpBTN.Visibility = Visibility.Hidden;


            PauseBTN.IsEnabled = true;
            Expander.IsEnabled = false;

        }
        private void VisualUpdateTimerOFF()
        {
            BTNStartContent.Text = "Start";
            PauseBTNContent.Text = "Pause";

            Grid.SetRow(HourTxtViwbx, 3);
            Grid.SetRowSpan(HourTxtViwbx, 2);
            Grid.SetRow(MinTxtViwbx, 3);
            Grid.SetRowSpan(MinTxtViwbx, 2);
            Grid.SetRow(SecTxtViwbx, 3);
            Grid.SetRowSpan(SecTxtViwbx, 2);
            Grid.SetColumnSpan(HourTxtViwbx, 1);
            Grid.SetColumnSpan(MinTxtViwbx, 1);
            Grid.SetColumnSpan(SecTxtViwbx, 1);
            MinSecDoubleDot.Visibility = Visibility.Hidden;
            HourMinDoubleDot.Visibility = Visibility.Hidden;
            HourTxtViwbx.HorizontalAlignment = HorizontalAlignment.Right;
            MinTxtViwbx.HorizontalAlignment = HorizontalAlignment.Right;
            SecTxtViwbx.HorizontalAlignment = HorizontalAlignment.Right;

            
            hVewbx.Visibility = Visibility.Visible;
            mVewBx.Visibility = Visibility.Visible;
            sVewBx.Visibility = Visibility.Visible;
            HourDownBTN.Visibility = Visibility.Visible;
            HourUpBTN.Visibility = Visibility.Visible;
            MinDownBTN.Visibility = Visibility.Visible;
            MinUpBTN.Visibility = Visibility.Visible;
            SecDownBTN.Visibility = Visibility.Visible;
            SecUpBTN.Visibility = Visibility.Visible;

            PauseBTN.IsEnabled = false;
            Expander.IsEnabled = true;

        }
        private void VisualUpdateTimerPaused()
        {
            BTNStartContent.Text = "Clear";
            PauseBTNContent.Text = "Go On";

            // HourTXTB

            Grid.SetRow(HourTxtViwbx, 2);
            Grid.SetRowSpan(HourTxtViwbx, 4);
            Grid.SetRow(MinTxtViwbx, 2);
            Grid.SetRowSpan(MinTxtViwbx, 4);
            Grid.SetRow(SecTxtViwbx, 2);
            Grid.SetRowSpan(SecTxtViwbx, 4);
            Grid.SetColumnSpan(HourTxtViwbx, 2);
            Grid.SetColumnSpan(MinTxtViwbx, 2);
            Grid.SetColumnSpan(SecTxtViwbx, 2);
            MinSecDoubleDot.Visibility = Visibility.Visible;
            HourMinDoubleDot.Visibility = Visibility.Visible;
            HourTxtViwbx.HorizontalAlignment = HorizontalAlignment.Center;
            MinTxtViwbx.HorizontalAlignment = HorizontalAlignment.Center;
            SecTxtViwbx.HorizontalAlignment = HorizontalAlignment.Center;
            hVewbx.Visibility = Visibility.Hidden;
            mVewBx.Visibility = Visibility.Hidden;
            sVewBx.Visibility = Visibility.Hidden;

            HourDownBTN.Visibility = Visibility.Hidden;
            HourUpBTN.Visibility = Visibility.Hidden;
            MinDownBTN.Visibility = Visibility.Hidden;
            MinUpBTN.Visibility = Visibility.Hidden;
            SecDownBTN.Visibility = Visibility.Hidden;
            SecUpBTN.Visibility = Visibility.Hidden;



            PauseBTN.IsEnabled = true;
            Expander.IsEnabled = false;

        }
        #endregion

        private void PauseBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryBuffer.CurrentState == TimerState.On)
            {
                Timer.Stop();
                MemoryBuffer.CurrentState = TimerState.Paused;
                VisualUpdateTimerPaused();
            }
            else
            {
                Timer.Start();
                MemoryBuffer.CurrentState = TimerState.On;
                VisualUpdateTimerON();
            }
        }

        private void AddFavBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MemoryBuffer.FavTimeButtons.Count <= 6)
            {
                var str = MemoryBuffer.ReturnHours() + ":" + MemoryBuffer.ReturnMinutes() + ":" + MemoryBuffer.ReturnSeconds();
                MemoryBuffer.FavTimeButtons.Add(new MyTimeButton(str));
                MemoryBuffer.FavDelButtons.Add(new MyDeleteButton());
                MemoryBuffer.FavTimeData.Add(MemoryBuffer.TotalSeconds);




                RefreshFavButtons(MemoryBuffer.FavTimeButtons, MemoryBuffer.FavDelButtons);

                MemoryBuffer.FavTimeButtons.Last().Click += (object sender2, RoutedEventArgs empty2) =>
                    {
                        var temp = (MyTimeButton)sender2;
                        int index = MemoryBuffer.FavTimeButtons.IndexOf(temp);
                        MemoryBuffer.TotalSeconds = MemoryBuffer.FavTimeData[index];


                    };

                MemoryBuffer.FavDelButtons.Last().Click += (object sender1, RoutedEventArgs empty) =>
                {
                    var a = (MyDeleteButton)sender1;

                    int index = MemoryBuffer.FavDelButtons.IndexOf(a);
                    MemoryBuffer.FavDelButtons.RemoveAt(index);
                    MemoryBuffer.FavTimeButtons.RemoveAt(index);
                    MemoryBuffer.FavTimeData.RemoveAt(index);

                    RefreshFavButtons(MemoryBuffer.FavTimeButtons, MemoryBuffer.FavDelButtons);

                };
            }

        }
        private void RefreshFavButtons(List<MyTimeButton> t, List<MyDeleteButton> d)
        {
            ExpanderGrid.Children.Clear();
            for (int i = 0; i < d.Count; i++)
            {
                Grid.SetColumn(t[i], 0);
                Grid.SetRow(t[i], i);
                Grid.SetColumn(d[i], 1);
                Grid.SetRow(d[i], i);
                ExpanderGrid.Children.Add(d[i]);
                ExpanderGrid.Children.Add(t[i]);
            }
        }

        private void SettingsBTN_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow SetWin = new SettingsWindow();
            SetWin.ShowDialog();
        }

    }
}
