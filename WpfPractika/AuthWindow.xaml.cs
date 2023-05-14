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
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        string registy_login = "";
        string registy_password = "";
        public AuthWindow()
        {
            InitializeComponent();
            Closed += AuthWindow_Closed;
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            try
            {
                this.Top = double.Parse(Encode.EncodeDecrypt(key.GetValue("topAuthWindow").ToString()));
                this.Left = double.Parse(Encode.EncodeDecrypt(key.GetValue("leftAuthWindow").ToString()));
            }
            catch { }
        }
        private void AuthWindow_Closed(object sender, EventArgs e)
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            try
            {
                string top = Encode.EncodeDecrypt(Convert.ToString(this.Top));
                key.SetValue("topAuthWindow", top);
                //Password_vvod.Password = Encode.EncodeDecrypt(Password_vvod.Password);

                string left = Encode.EncodeDecrypt(Convert.ToString(this.Left));
                key.SetValue("leftAuthWindow", left);
                //Login_vvod.Text = Encode.EncodeDecrypt(Login_vvod.Text);
            }
            catch
            {

            }
        }

        private async void AuthorizationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryKey registry = Registry.CurrentUser;
                RegistryKey key = registry.CreateSubKey("PraktikaMay");
                try
                {
                    registy_password = key.GetValue("Password").ToString();
                    registy_password = Encode.EncodeDecrypt(registy_password);
                    registy_login = key.GetValue("login").ToString();
                    registy_login = Encode.EncodeDecrypt(registy_login);
                }
                catch
                {
                    registy_password = "";
                    registy_login = "";
                }
                API api = new API();
                if (registy_password != "" && registy_login != "")
                {
                    string answer = await api.Auth(registy_login, registy_login);
                    if (answer == "OK")
                    {
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
                    //MessageBox.Show("Заполните все поля!", "Ошибка!");
                }
            }
            catch
            {

            }
        }

        private async void Auth_Click(object sender, RoutedEventArgs e)
        {
            API api = new API();
            if (Login_vvod.Text != "" && Password_vvod.Password != "")
            {
                string answer = await api.Auth(Login_vvod.Text, Password_vvod.Password);
                if (answer == "OK")
                {
                    RegistryKey registry = Registry.CurrentUser;
                    RegistryKey key = registry.CreateSubKey("PraktikaMay");
                    try
                    {
                        Password_vvod.Password = Encode.EncodeDecrypt(Password_vvod.Password);
                        key.SetValue("Password", Password_vvod.Password);
                        Password_vvod.Password = Encode.EncodeDecrypt(Password_vvod.Password);

                        Login_vvod.Text = Encode.EncodeDecrypt(Login_vvod.Text);
                        key.SetValue("Login", Login_vvod.Text);
                        Login_vvod.Text = Encode.EncodeDecrypt(Login_vvod.Text);
                    }
                    catch
                    {

                    }
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

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }
    }
}
