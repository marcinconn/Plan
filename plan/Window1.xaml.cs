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

namespace plan
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            //this.Closing += W1_Closing;
            //if (addTab) tabControl.Items
            /*if (editTab) tabControl.SelectedIndex = 1;
            if (deleteTab) tabControl.SelectedIndex = 2;*/
        }

        private void W1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.w1.Visibility = Visibility.Collapsed;
        }

        public static bool addTab;
        public static bool editTab;
        public static bool deleteTab;

        public void Dodaj(object sender, RoutedEventArgs e)
        {
            String s = newValueAdd.Text;
            String s2 = MainWindow.w.ToString();

            switch (s2)
            {
                case "s":
                    TreeViewItem tvi = new TreeViewItem() { Header = s };
                    MainWindow.AppWindow.sale.Items.Add(tvi);
                    MainWindow.w.Clear();
                    newValueAdd.Clear();
                    this.Visibility = Visibility.Collapsed;
                    break;
                case "n":
                    TreeViewItem tvi2 = new TreeViewItem() { Header = s };
                    MainWindow.AppWindow.nauczyciel.Items.Add(tvi2);
                    MainWindow.w.Clear();
                    newValueAdd.Clear();
                    this.Visibility = Visibility.Collapsed;
                    break;
                case "k":
                    TreeViewItem tvi3 = new TreeViewItem() { Header = s };
                    MainWindow.AppWindow.klasy.Items.Add(tvi3);
                    MainWindow.w.Clear();
                    newValueAdd.Clear();
                    this.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        public void Usun(object sender, RoutedEventArgs e)
        {
            String s = deleteValue.Text;
            String s2 = MainWindow.w.ToString();

            switch (s2)
            {
                case "s":
                    int i = 0;
                    foreach (TreeViewItem item in MainWindow.AppWindow.sale.Items)
                      {
                           if (item.Header.ToString().Contains(s))
                           {
                            MainWindow.AppWindow.sale.Items.RemoveAt(i);
                            break;
                           }
                            i++;
                      }
                    MainWindow.w.Clear();
                    deleteValue.Clear();
                    this.Visibility = Visibility.Collapsed;
                    break;
                case "n":
                    int j = 0;
                    foreach (TreeViewItem item in MainWindow.AppWindow.nauczyciel.Items)
                    {
                        if (item.Header.ToString().Contains(s))
                        {
                            MainWindow.AppWindow.nauczyciel.Items.RemoveAt(j);
                            break;
                        }
                        j++;
                    }
                    MainWindow.w.Clear();
                    deleteValue.Clear();
                    this.Visibility = Visibility.Collapsed;
                    break;
                case "k":
                    int k = 0;
                    foreach (TreeViewItem item in MainWindow.AppWindow.klasy.Items)
                    {
                        if (item.Header.ToString().Contains(s))
                        {
                            MainWindow.AppWindow.klasy.Items.RemoveAt(k);
                            break;
                        }
                        k++;
                    }
                    MainWindow.w.Clear();
                    deleteValue.Clear();
                    this.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        public String newVal(TextBox tb )
        {
            String val = tb.Text;
            return val;
        }

        public void Edit(object sender, RoutedEventArgs e)
        {
            String se1 = oldValueEdit.Text;
            String s3 = MainWindow.w.ToString();

            switch (s3)
            {
                case "s":
                    foreach (TreeViewItem item in MainWindow.AppWindow.sale.Items)
                    {
                        if (item.Header.ToString().Contains(se1))
                        {
                            item.Header = newVal(newValueEdit);
                            break;
                        }
                    }
                    newValueEdit.Clear();
                    oldValueEdit.Clear();
                    MainWindow.w.Clear();
                    Hide();
                    break;
                case "n":
                    foreach (TreeViewItem item in MainWindow.AppWindow.nauczyciel.Items)
                    {
                        if (item.Header.ToString().Contains(se1))
                        {
                            item.Header = newVal(newValueEdit);
                            break;
                        }
                    }
                    newValueEdit.Clear();
                    oldValueEdit.Clear();
                    MainWindow.w.Clear();
                    Hide();
                    break;
                case "k":
                    foreach (TreeViewItem item in MainWindow.AppWindow.klasy.Items)
                    {
                        if (item.Header.ToString().Contains(se1))
                        {
                            item.Header = newVal(newValueEdit);
                            break;
                        }
                    }
                    newValueEdit.Clear();
                    oldValueEdit.Clear();
                    MainWindow.w.Clear();
                    Hide();
                    break;
            }
        }
    }
}
