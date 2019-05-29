using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
using System.Data.Common;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace plan
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;
        public static StringBuilder w = new StringBuilder();
        public static Window1 w1 = new Window1();

        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;
            loadTreeView();
        }

        public void TreeViewItem_OnItemSelected_classes(object sender, RoutedEventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                connection.Open();
                OleDbCommand cmd = new OleDbCommand();
                string q = "TRANSFORM Max(kwerenda_klas.[Lekcja]) AS MaksimumOfLekcja " +
                     "SELECT kwerenda_klas.[id_ter],kwerenda_klas.[ter_nazwa] " +
                     "FROM kwerenda_klas WHERE(((kwerenda_klas.kla_nazwa)= '2B')) " +
                     "GROUP BY kwerenda_klas.[id_ter], kwerenda_klas.[ter_nazwa], kwerenda_klas.kla_nazwa " +
                     "PIVOT kwerenda_klas.[id_dnia];";
                cmd.CommandText = q.Replace("2B",tree.SelectedItem.ToString().Replace("Nagłówek System.Windows.Controls.TreeViewItem:", String.Empty).Replace("Items.Count:0", String.Empty));
                    
                cmd.Connection = connection;
                OleDbDataReader dr = cmd.ExecuteReader();
                plan.ItemsSource = dr;
                plan.DataContext = dr;
                AppWindow.planTitle.Content = tree.SelectedItem.ToString().Replace("Nagłówek System.Windows.Controls.TreeViewItem:", String.Empty).Replace("Items.Count:0", String.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void TreeViewItem_OnItemSelected_room(object sender, RoutedEventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                connection.Open();
                OleDbCommand cmd = new OleDbCommand();
                string q = "TRANSFORM Max(kwerenda_sal.Lekcja) AS MaksimumOfLekcja " +
                    "SELECT kwerenda_sal.id_ter, kwerenda_sal.ter_nazwa " +
                    "FROM kwerenda_sal WHERE(((kwerenda_sal.sal_nazwa) = '14')) " +
                    "GROUP BY kwerenda_sal.id_ter, kwerenda_sal.ter_nazwa, kwerenda_sal.sal_nazwa " +
                    "PIVOT kwerenda_sal.id_dnia;";
                cmd.CommandText = q.Replace("14", tree.SelectedItem.ToString().Replace("Nagłówek System.Windows.Controls.TreeViewItem:", String.Empty).Replace("Items.Count:0", String.Empty));
                cmd.Connection = connection;
                OleDbDataReader dr = cmd.ExecuteReader();
                plan.ItemsSource = dr;
                plan.DataContext = dr;
                AppWindow.planTitle.Content = tree.SelectedItem.ToString().Replace("Nagłówek System.Windows.Controls.TreeViewItem:", String.Empty).Replace("Items.Count:0", String.Empty);
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.Message);
            }
        }

        public void TreeViewItem_OnItemSelected_teacher(object sender, RoutedEventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                connection.Open();
                OleDbCommand cmd = new OleDbCommand();
                string q = "TRANSFORM Max(kwerenda_nauczycieli.Lekcja) AS MaksimumOfLekcja " +
                    "SELECT kwerenda_nauczycieli.id_ter, kwerenda_nauczycieli.ter_nazwa " +
                    "FROM kwerenda_nauczycieli WHERE(((kwerenda_nauczycieli.nau_dane) = 'Jan Kowalski'))" +
                    "GROUP BY kwerenda_nauczycieli.id_ter, kwerenda_nauczycieli.ter_nazwa, kwerenda_nauczycieli.nau_dane" +
                    " PIVOT kwerenda_nauczycieli.id_dnia;";
                cmd.CommandText = q.Replace("Jan Kowalski", tree.SelectedItem.ToString().Replace("Nagłówek System.Windows.Controls.TreeViewItem:", String.Empty).Replace("Items.Count:0", String.Empty));

                cmd.Connection = connection;
                OleDbDataReader dr = cmd.ExecuteReader();
                plan.ItemsSource = dr;
                plan.DataContext = dr;
                AppWindow.planTitle.Content = tree.SelectedItem.ToString().Replace("Nagłówek System.Windows.Controls.TreeViewItem:", String.Empty).Replace("Items.Count:0", String.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as ItemsControl;
        }
        
        public void What(object sender, RoutedEventArgs e)
        {
            AddToTreeView a = new AddToTreeView();
            DeleteFromTreeView d = new DeleteFromTreeView();
            EditTreeView t = new EditTreeView();
            Lesson lesson = new Lesson();
            Tables tab = new Tables();
            Classes c = new Classes();

            MenuItem mi = e.Source as MenuItem;
            
            switch (mi.Name)
            {
                case "add_s":
                    w.Append("s");
                    a.ShowDialog();
                    break;
                case "add_n":
                    w.Append("n");
                    a.ShowDialog();
                    break;
                case "add_k":
                    w.Append("k");
                    a.ShowDialog();
                    break;
                case "add_p":
                    w.Append("p");
                    a.ShowDialog();
                    break;
                case "edit_s":
                    w.Append("s");
                    t.ShowDialog();
                    break;
                case "edit_n":
                    w.Append("n");
                    t.ShowDialog();
                    break;
                case "edit_k":
                    w.Append("k");
                    t.ShowDialog();
                    break;
                case "edit_p":
                    w.Append("p");
                    t.ShowDialog();
                    break;
                case "del_s":
                    w.Append("s");
                    d.ShowDialog();
                    break;
                case "del_n": 
                    w.Append("n");
                    d.ShowDialog();
                    break;
                case "del_k":
                    w.Append("k");
                    d.ShowDialog();
                    break;
                case "del_p":
                    w.Append("p");
                    d.ShowDialog();
                    break;
                case "add_z":
                    lesson.ShowDialog();
                    break;
                case "del_spec_z":
                    w.Append("z");
                    d.ShowDialog();
                    break;
                case "show_n":
                    tab.SetGrid("Nauczyciel", "nau_dane");
                    tab.ShowDialog();
                    break;
                case "show_s":
                    tab.SetGrid("Sala", "sal_nazwa");
                    tab.ShowDialog();
                    break;
                case "show_k":
                    tab.SetGrid("Klasa", "kla_nazwa");
                    tab.ShowDialog();
                    break;
                case "show_p":
                    tab.SetGrid("Przedmiot", "prz_nazwa");
                    tab.ShowDialog();
                    break;
                case "show_z":
                    c.ShowDialog();
                    break;
            }
        }

        private void sFromFile()
        {
            try
            {
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                connection.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT sal_nazwa FROM Sala";
                cmd.Connection = connection;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TreeViewItem tvi = new TreeViewItem() { Header = reader.GetString(0) };
                        tvi.Selected += TreeViewItem_OnItemSelected_room;
                        AppWindow.sale.Items.Add(tvi);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
    }

        private void kFromFile()
        {
            try
            {
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                connection.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT kla_nazwa FROM Klasa";
                cmd.Connection = connection;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TreeViewItem tvi = new TreeViewItem() { Header = reader.GetString(0) };
                        tvi.Selected += TreeViewItem_OnItemSelected_classes;
                        AppWindow.klasy.Items.Add(tvi);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        private void nFromFile()
        {
            try
            {
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                connection.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT nau_dane FROM Nauczyciel";
                cmd.Connection = connection;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TreeViewItem tvi = new TreeViewItem() { Header = reader.GetString(0) };
                        tvi.Selected += TreeViewItem_OnItemSelected_teacher;
                        AppWindow.nauczyciel.Items.Add(tvi);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        public void loadTreeView()
        {
            sFromFile();
            kFromFile();
            nFromFile();
        }

        public void Delete_All(object sender, RoutedEventArgs e)
        {
            int result = (int)MessageBox.Show("Usunąć wszystkie zajęcia?","Potwierdzenie",MessageBoxButton.YesNo,MessageBoxImage.Stop,MessageBoxResult.No);
            switch(result)
            {
                case (int)MessageBoxResult.Yes:
                    OleDbConnection con = new OleDbConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE * FROM Zajęcia";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    break;
                case (int)MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }
    }
}
