using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace WpfPractika
{
    /// <summary>
    /// Логика взаимодействия для SelectTableWindow.xaml
    /// </summary>
    public partial class SelectTableWindow : Window
    {
        public SelectTableWindow()
        {
            InitializeComponent();
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            try
            {
                this.Top = double.Parse(Encode.EncodeDecrypt(key.GetValue("topSelectTableWindow").ToString()));
                this.Left = double.Parse(Encode.EncodeDecrypt(key.GetValue("leftSelectTableWindow").ToString()));
            }
            catch { }
            Closed += SelectTableWindow_Closed;
        }

        private void SelectTableWindow_Closed(object sender, EventArgs e)
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            try
            {
                string top = Encode.EncodeDecrypt(Convert.ToString(this.Top));
                key.SetValue("topSelectTableWindow", top);
                //Password_vvod.Password = Encode.EncodeDecrypt(Password_vvod.Password);

                string left = Encode.EncodeDecrypt(Convert.ToString(this.Left));
                key.SetValue("leftSelectTableWindow", left);
                //Login_vvod.Text = Encode.EncodeDecrypt(Login_vvod.Text);
            }
            catch
            {

            }
        }

        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("Bookings");
            mainWindow.Show();
        }
        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("Feedbacks");
            mainWindow.Show();
        }
        private void Game_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("Games");
            mainWindow.Show();
        }
        private void GameConsole_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("GameConsoles");
            mainWindow.Show();
        }
        private void GameOnPc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("GameOnPcs");
            mainWindow.Show();
        }
        private void HallSeat_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("HallSeats");
            mainWindow.Show();
        }
        private void HistoryBooking_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("HistoryBookings", true);
            mainWindow.Show();
        }
        private void Pc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("Pcs");
            mainWindow.Show();
        }
        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("Rates");
            mainWindow.Show();
        }
        private void Sale_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("Sales");
            mainWindow.Show();
        }
        private void SaleBooking_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("SaleBookings");
            mainWindow.Show();
        }
        private void Service_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("Services");
            mainWindow.Show();
        }
        private void ServiceOfBooking_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("ServiceOfBookings");
            mainWindow.Show();
        }
        private void Subscription_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("Subscriptions");
            mainWindow.Show();
        }
        private void SubscriptionUser_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("SubscriptionUsers");
            mainWindow.Show();
        }
        private void Users_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("Users", true);
            mainWindow.Show();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            key.SetValue("Login", "");
            key.SetValue("Password", "");
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}
