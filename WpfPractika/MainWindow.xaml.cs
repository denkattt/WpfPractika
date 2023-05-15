using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
using WpfPractika.Models;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace WpfPractika
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        string table;
        int ID;
        API api = new API();
        public MainWindow(string table, bool canDeleted = false)
        {
            InitializeComponent();
            this.table = table;
            TableName.Text = this.table;
            FillDatagrid();
            if (!canDeleted)
            {
                btDelFasleItemOpt.Visibility = Visibility.Hidden;
                btVostFalseItemOpt.Visibility = Visibility.Hidden;
                lbCountLine.Visibility = Visibility.Hidden;
                lbNumberPage.Visibility = Visibility.Hidden;
                tbCountLine.Visibility = Visibility.Hidden;
                tbNumberPage.Visibility = Visibility.Hidden;
                btGetPage.Visibility = Visibility.Hidden;
            }
            Closed += MainWindow_Closed;

            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            try
            {
                this.Top = double.Parse(Encode.EncodeDecrypt(key.GetValue("topMainWindow").ToString()));
                this.Left = double.Parse(Encode.EncodeDecrypt(key.GetValue("leftMainWindow").ToString()));
            }
            catch { }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("PraktikaMay");
            try
            {
                string top = Encode.EncodeDecrypt(Convert.ToString(this.Top));
                key.SetValue("topMainWindow", top);
                //Password_vvod.Password = Encode.EncodeDecrypt(Password_vvod.Password);

                string left = Encode.EncodeDecrypt(Convert.ToString(this.Left));
                key.SetValue("leftMainWindow", left);
                //Login_vvod.Text = Encode.EncodeDecrypt(Login_vvod.Text);
            }
            catch
            {

            }
        }

        public async void FillDatagrid()
        {
            string json = await api.Get(table);
            Type itemType = Type.GetType("WpfPractika.Models." + table);
            Type listType = typeof(List<>).MakeGenericType(itemType);
            object items = JsonConvert.DeserializeObject(json, listType);
            dgItems.ItemsSource = (System.Collections.IEnumerable)items;
        }

        private void btAddItem_Click(object sender, RoutedEventArgs e)
        {
            var myobj = Activator.CreateInstance(Type.GetType("WpfPractika.Models." + table));
            AddItemWindow addItemWindow = new AddItemWindow(dgItems, table, myobj, this);
            addItemWindow.Show();
        }

        private async void btDelItem_Click(object sender, RoutedEventArgs e)
        {            
            string response = await api.Delete(table, ID);

            if (response == "")
            {
                MessageBox.Show("Успешно удалено!", "Успех!");
            }
            else
            {
                MessageBox.Show(response, "Ошибка!");
            }
            FillDatagrid();
        }

        private void dgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedCell = dgItems.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                if (cellContent is TextBlock)
                {
                    ID = int.Parse((cellContent as TextBlock).Text);
                }
                btDel.IsEnabled = true;
                btDelFasleItemOpt.IsEnabled = true;
                btVostFalseItemOpt.IsEnabled = true;
            }
            catch
            {
                btDel.IsEnabled = false;
                btDelFasleItemOpt.IsEnabled = false;
                btVostFalseItemOpt.IsEnabled = false;
            }
        }

        private async void dgItems_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                // Получение нового значения ячейки в столбце MyColumn
                var newValue = ((System.Windows.Controls.TextBox)e.EditingElement).Text;

                var newObject = e.Row.DataContext;

                var columnName = e.Column.Header.ToString();

                // Используем динамическое свойство объекта
                var property = newObject.GetType().GetProperty(columnName);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(newObject, newValue);
                }

                var jsonString = JsonConvert.SerializeObject(newObject);

                string response = await api.Put(table, ID, jsonString);

                if (response == "")
                {
                    //MessageBox.Show("Успешно изменено!", "Успех!");
                }
                else
                {
                    MessageBox.Show(response, "Ошибка!");
                }
            }
        }

        private async void btExcel_Click(object sender, RoutedEventArgs e)
        {
            var excelApp = await System.Threading.Tasks.Task.Run(() => new Excel.Application());

            Workbook workbook = excelApp.Workbooks.Add();
            _Worksheet worksheet = workbook.ActiveSheet;

            for (int i = 1; i <= dgItems.Columns.Count; i++)
            {
                var header = dgItems.Columns[i - 1].Header;
                worksheet.Cells[1, i] = header;
            }

            for (int i = 0; i < dgItems.Items.Count; i++)
            {
                var row = dgItems.Items[i];
                for (int j = 0; j < dgItems.Columns.Count; j++)
                {
                    var cellValue = row.GetType().GetProperty(dgItems.Columns[j].SortMemberPath).GetValue(row, null);
                    worksheet.Cells[i + 2, j + 1] = cellValue != null ? cellValue.ToString() : string.Empty;
                }
            }

            excelApp.Visible = true;
        }

        private async void btWord_Click(object sender, RoutedEventArgs e)
        {
            var wordApp = await System.Threading.Tasks.Task.Run(() => new Word.Application());
            var document = wordApp.Documents.Add();

            document.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
            document.Content.Font.Size = 12;

            int rowsCount = dgItems.Items.Count + 1;
            int columnsCount = dgItems.Columns.Count;
            var table = document.Tables.Add(document.Range(), rowsCount, columnsCount);

            table.Borders.Enable = 1; // Включаем границы таблицы
            table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle; // Стиль линии для внутренних границ
            table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle; // Стиль линии для внешних границ
            for (int i = 1; i <= columnsCount; i++)
            {
                table.Cell(1, i).Range.Text = dgItems.Columns[i - 1].Header.ToString();
            }
            for (int i = 0; i < dgItems.Items.Count; i++)
            {
                var item = dgItems.Items[i];
                for (int j = 0; j < dgItems.Columns.Count; j++)
                {
                    var cellValue = item.GetType().GetProperty(dgItems.Columns[j].SortMemberPath).GetValue(item, null);
                    table.Cell(i + 2, j + 1).Range.Text = cellValue != null ? cellValue.ToString() : "";
                }
            }
            wordApp.Visible = true;
        }

        private async void btDelFasleItemOpt_Click(object sender, RoutedEventArgs e)
        {
            string str = "";
            var selectedItems = dgItems.SelectedItems;
            foreach (var selectedItem in selectedItems)
            {
                string cellValue = Convert.ToString(selectedItem.GetType().GetProperty(dgItems.Columns[0].SortMemberPath).GetValue(selectedItem, null));
                if (str == "")
                {
                    str += cellValue;
                }
                else
                {
                    str += $",{cellValue}";
                }
            }
            var response = await api.PutChangeStatus(table, 1, str);
            if (response != "")
            {
                MessageBox.Show(response, "Ошибка!");
            }
            FillDatagrid();
        }

        private async void btVostFalseItemOpt_Click(object sender, RoutedEventArgs e)
        {
            string str = "";
            var selectedItems = dgItems.SelectedItems;
            foreach (var selectedItem in selectedItems)
            {
                string cellValue = Convert.ToString(selectedItem.GetType().GetProperty(dgItems.Columns[0].SortMemberPath).GetValue(selectedItem, null));
                if (str == "")
                {
                    str += cellValue;
                }
                else
                {
                    str += $",{cellValue}";
                }
            }
            var response = await api.PutChangeStatus(table, 0, str);
            if (response != "")
            {
                MessageBox.Show(response, "Ошибка!");
            }
            FillDatagrid();
        }

        private async void btGetPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                uint page = uint.Parse(tbNumberPage.Text);
                if (page == 0)
                {
                    MessageBox.Show("Введите ненулевое число номера страницы!", "Ошибка!");
                }

                uint count = uint.Parse(tbCountLine.Text);
                if (count == 0)
                {
                    MessageBox.Show("Введите ненулевое число записей на странице!", "Ошибка!");
                }

                string json = await api.GetPage(table, page, count);
                Type itemType = Type.GetType("WpfPractika.Models." + table);
                Type listType = typeof(List<>).MakeGenericType(itemType);
                object items = JsonConvert.DeserializeObject(json, listType);
                dgItems.ItemsSource = (System.Collections.IEnumerable)items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            
        }
    }
}
