using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfPractika.Models;

namespace WpfPractika
{
    /// <summary>
    /// Логика взаимодействия для AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        int numColumns;
        string table;
        object tableClass;
        List<Label> labels = new List<Label>();
        List<TextBox> textBoxs = new List<TextBox>();
        public AddItemWindow(DataGrid dataGrid, string table, object tableClass, MainWindow mainWindow)
        {
            InitializeComponent();
            this.table = table;
            this.tableClass = tableClass;
            Grid grid = new Grid();
            numColumns = dataGrid.Columns.Count;

            ColumnDefinition columnDefinition = new ColumnDefinition();
            grid.ColumnDefinitions.Add(columnDefinition);
            columnDefinition = new ColumnDefinition();
            grid.ColumnDefinitions.Add(columnDefinition);

            for (int i = 0; i < numColumns-1; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                grid.RowDefinitions.Add(rowDefinition);
            }

            for (int i = 0; i < numColumns; i++)
            {
                if (i != 0)
                {
                    Label label = new Label();
                    label.Content = dataGrid.Columns[i].Header.ToString();
                    Grid.SetRow(label, i-1);
                    Grid.SetColumn(label, 0);
                    labels.Add(label);

                    TextBox textBox = new TextBox();
                    Grid.SetRow(textBox, i-1);
                    Grid.SetColumn(textBox, 1);
                    textBoxs.Add(textBox);

                    grid.Children.Add(label);
                    grid.Children.Add(textBox);
                }
            }

            RowDefinition rowDefinitionn = new RowDefinition();
            grid.RowDefinitions.Add(rowDefinitionn);

            Button btnAdd = new Button();
            btnAdd.Content = "Добавить запись";
            Grid.SetRow(btnAdd, numColumns);
            Grid.SetColumn(btnAdd, 0);
            Grid.SetColumnSpan(btnAdd, 2);
            btnAdd.Click += async (sender, e) =>
            {
                string json = jsonConverter(tableClass);
                if (json != "ошибка")
                {
                    API api = new API();
                    await api.Post(table, json);
                    mainWindow.FillDatagrid();
                    //MessageBox.Show("Данные добавлены.");
                    Close();
                }
                
            };
            grid.Children.Add(btnAdd);

            this.Content = grid;

            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            try
            {
                this.Top = double.Parse(Encode.EncodeDecrypt(key.GetValue("topAddItemWindow").ToString()));
                this.Left = double.Parse(Encode.EncodeDecrypt(key.GetValue("leftAddItemWindow").ToString()));
            }
            catch { }
            Closed += AddItemWindow_Closed;
        }

        private void AddItemWindow_Closed(object sender, EventArgs e)
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            try
            {
                string top = Encode.EncodeDecrypt(Convert.ToString(this.Top));
                key.SetValue("topAddItemWindow", top);
                //Password_vvod.Password = Encode.EncodeDecrypt(Password_vvod.Password);

                string left = Encode.EncodeDecrypt(Convert.ToString(this.Left));
                key.SetValue("topAddItemWindow", left);
                //Login_vvod.Text = Encode.EncodeDecrypt(Login_vvod.Text);
            }
            catch
            {

            }
        }

        public string jsonConverter(object obj)
        {
            try
            {
                int i = 0;
                foreach (var propertyInfo in obj.GetType().GetProperties())
                {
                    if (i == 0)
                    {
                        i++;
                        continue;
                    }
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        if (textBoxs[i - 1].Text != "")
                        {
                            propertyInfo.SetValue(obj, textBoxs[i - 1].Text);
                        }
                    }
                    else if (propertyInfo.PropertyType == typeof(int))
                    {
                        if (textBoxs[i - 1].Text == "") propertyInfo.SetValue(obj, 0);
                        else propertyInfo.SetValue(obj, int.Parse(textBoxs[i - 1].Text));
                    }
                    else if (propertyInfo.PropertyType == typeof(decimal))
                    {
                        if (textBoxs[i - 1].Text == "") propertyInfo.SetValue(obj, 0);
                        else propertyInfo.SetValue(obj, decimal.Parse(textBoxs[i - 1].Text));
                    }

                    i++;
                }

                string jsonString = JsonConvert.SerializeObject(obj);

                return jsonString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
                return "ошибка";
            }
        }
    }
}
