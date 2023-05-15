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
using System.Net;
using System.Net.Mail;
using Microsoft.Win32;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;

namespace WpfPractika
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            Closed += RegistrationWindow_Closed;
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            try
            {
                this.Top = double.Parse(Encode.EncodeDecrypt(key.GetValue("topRegistrationWindow").ToString()));
                this.Left = double.Parse(Encode.EncodeDecrypt(key.GetValue("leftRegistrationWindow").ToString()));
            }
            catch { }
        }

        private void RegistrationWindow_Closed(object sender, EventArgs e)
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            try
            {
                string top = Encode.EncodeDecrypt(Convert.ToString(this.Top));
                key.SetValue("topRegistrationWindow", top);
                //Password_vvod.Password = Encode.EncodeDecrypt(Password_vvod.Password);

                string left = Encode.EncodeDecrypt(Convert.ToString(this.Left));
                key.SetValue("leftRegistrationWindow", left);
                //Login_vvod.Text = Encode.EncodeDecrypt(Login_vvod.Text);
            }
            catch
            {

            }
        }

        private async void Registration_Click(object sender, RoutedEventArgs e)
        {
            API api = new API();
            if (Login_vvod.Text != "" && Password_vvod.Password != "" && Password_vvod_again.Password != "" && 
                Second_name_vvod.Text != "" && First_name_vvod.Text != "" && Father_name_vvod.Text != "")
            {
                string answer = await api.Registration(Login_vvod.Text, Password_vvod.Password, Second_name_vvod.Text, 
                                                       First_name_vvod.Text, Father_name_vvod.Text);
                if (answer == "OK")
                {
                    MessageBox.Show("Вы зарегистрировались!", "Успех!");
                    SelectTableWindow selectTableWindow = new SelectTableWindow();
                    selectTableWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show(answer, "Ошибка!");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!");
            }
        }
    }
}
