using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logika interakcji dla klasy AddToTreeView.xaml
    /// </summary>
    public partial class AddToTreeView : Window
    {
        public AddToTreeView()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            String s = toAdd.Text;
            String s2 = MainWindow.w.ToString();
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            MyDB mdb = new MyDB();

            if (!string.IsNullOrEmpty(toAdd.Text))
            {
                switch (s2)
                {
                    case "s":
                        if (!Regex.IsMatch(toAdd.Text, "^[0-9]+$") || mdb.CountEntries("Sala", toAdd.Text) > 0) { MessageBox.Show("Sala istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
                            TreeViewItem tvi = new TreeViewItem() { Header = s };
                            tvi.Selected += MainWindow.AppWindow.TreeViewItem_OnItemSelected_room;
                            MainWindow.AppWindow.sale.Items.Add(tvi);
                            MainWindow.w.Clear();
                            toAdd.Clear();
                            cmd.CommandText = "insert into [Sala](sal_nazwa)Values(@nm)";
                            cmd.Parameters.AddWithValue("@nm", s);
                            cmd.Connection = con;
                            int a = cmd.ExecuteNonQuery();
                            if (a > 0) { MessageBox.Show("Dodane"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                    case "n":
                        if (!Regex.IsMatch(toAdd.Text, "^[a-zA-Z]+\\s?[a-zA-Z]+$") || mdb.CountEntries("Nauczyciel", toAdd.Text) > 0) { MessageBox.Show("Nauczyciel istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
                            TreeViewItem tvi2 = new TreeViewItem() { Header = s };
                            tvi2.Selected += MainWindow.AppWindow.TreeViewItem_OnItemSelected_teacher;
                            MainWindow.AppWindow.nauczyciel.Items.Add(tvi2);
                            MainWindow.w.Clear();
                            toAdd.Clear();
                            cmd.CommandText = "insert into [Nauczyciel](nau_dane)Values(@nm)";
                            cmd.Parameters.AddWithValue("@nm", s);
                            cmd.Connection = con;
                            int b = cmd.ExecuteNonQuery();
                            if (b > 0) { MessageBox.Show("Dodane"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                    case "k":
                        if (!Regex.IsMatch(toAdd.Text, "^[0-9]{1}[a-zA-Z]{1}$") || mdb.CountEntries("Klasa", toAdd.Text) > 0) { MessageBox.Show("Klasa istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
                            TreeViewItem tvi3 = new TreeViewItem() { Header = s };
                            tvi3.Selected += MainWindow.AppWindow.TreeViewItem_OnItemSelected_classes;
                            MainWindow.AppWindow.klasy.Items.Add(tvi3);
                            MainWindow.w.Clear();
                            toAdd.Clear();
                            cmd.CommandText = "insert into [Klasa](kla_nazwa)Values(@nm)";
                            cmd.Parameters.AddWithValue("@nm", s);
                            cmd.Connection = con;
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0) { MessageBox.Show("Dodane"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                    case "p":
                        if (!Regex.IsMatch(toAdd.Text, "^[a-zA-Z]+$") || mdb.CountEntries("Przedmiot", toAdd.Text) > 0) { MessageBox.Show("Przedmiot istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
                            MainWindow.w.Clear();
                            toAdd.Clear();
                            cmd.CommandText = "insert into [Przedmiot](prz_nazwa)Values(@nm)";
                            cmd.Parameters.AddWithValue("@nm", s);
                            cmd.Connection = con;
                            int d = cmd.ExecuteNonQuery();
                            if (d > 0) { MessageBox.Show("Dodane"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                }
            }
        }
    }
}
