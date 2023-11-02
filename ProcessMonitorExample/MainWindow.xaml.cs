using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace ProcessMonitorExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // таймер 
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            LoadProcessInfo();

            //
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            LoadProcessInfo();
        }

        // метод выгрузки информации о процессах в ListBox
        private void LoadProcessInfo()
        {
            processInfoListBox.Items.Clear();
            Process[] processes = Process.GetProcesses().Take(10).ToArray();
            processInfoListBox.Items.Add($"Всего процессов: {processes.Length}");
            processInfoListBox.Items.Add("pid - name - start time - life time");
            foreach (Process process in processes)
            {
                string pid, name, startTime, lifeTime;
                pid = process.Id.ToString();
                name = process.ProcessName;
                try
                {
                    pid = process.Id.ToString();
                } catch
                {
                    pid = "unavailable";
                }
                try
                {
                    name = process.ProcessName;
                } catch
                {
                    name = "unavailable";
                }
                try
                {
                    startTime = process.StartTime.ToString();
                } catch
                {
                    startTime = "unavailable";
                }
                try
                {
                    lifeTime = (DateTime.Now - process.StartTime).TotalSeconds.ToString();
                } catch
                {
                    lifeTime = "unavailable";
                }
                processInfoListBox.Items.Add($"{pid} - {name} - {startTime} - {lifeTime}");
            }
        }

        private void manualUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // обновляем список процессов
            LoadProcessInfo();
        }

        private void timerUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. изменим кнопку
            if (timer.IsEnabled)
            {
                timerUpdateButton.Content = "Запустить автообновление";
                timerUpdateButton.Background = Brushes.LightGreen;
                timer.Stop();
            } else
            {
                timerUpdateButton.Content = "Остановить автообновление";
                timerUpdateButton.Background = Brushes.Red;
                timer.Start();
            }
        }
    }
}
