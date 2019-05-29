using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
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
    /// Logika interakcji dla klasy DeleteFromTreeView.xaml
    /// </summary>
    public partial class DeleteFromTreeView : Window
    {
        public DeleteFromTreeView()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            String s = toDelete.Text;
            String s2 = MainWindow.w.ToString();
            OleDbConnection con = new OleDbConnection();
            MyDB mdb = new MyDB();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();

            if (!string.IsNullOrEmpty(toDelete.Text))
            {
                switch (s2)
                {
                    case "s":
                        if (mdb.FindID("Sala", toDelete.Text) <= 0) { MessageBox.Show("Sala nie istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
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
                            toDelete.Clear();
                            cmd.CommandText = "delete from [Sala] where sal_nazwa = @nm";
                            cmd.Parameters.AddWithValue("@nm", s);
                            cmd.Connection = con;
                            int a = cmd.ExecuteNonQuery();
                            if (a > 0) { MessageBox.Show("Usunięte"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                    case "n":
                        if (mdb.FindID("Nauczyciel", toDelete.Text) <= 0) { MessageBox.Show("Nauczyciel nie istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
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
                            toDelete.Clear();
                            cmd.CommandText = "delete from [Nauczyciel] where nau_dane = @nm";
                            cmd.Parameters.AddWithValue("@nm", s);
                            cmd.Connection = con;
                            int b = cmd.ExecuteNonQuery();
                            if (b > 0) { MessageBox.Show("Usunięte"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                    case "k":
                        if (mdb.FindID("Klasa", toDelete.Text) <= 0) { MessageBox.Show("Klasa nie istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
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
                            toDelete.Clear();
                            cmd.CommandText = "delete from [Klasa] where kla_nazwa = @nm";
                            cmd.Parameters.AddWithValue("@nm", s);
                            cmd.Connection = con;
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0) { MessageBox.Show("Usunięte"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                    case "p":
                        if (mdb.FindID("Przedmiot", toDelete.Text) <= 0) { MessageBox.Show("Przedmiot nie istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
                            MainWindow.w.Clear();
                            toDelete.Clear();
                            cmd.CommandText = "delete from [Przedmiot] where prz_nazwa = @nm";
                            cmd.Parameters.AddWithValue("@nm", s);
                            cmd.Connection = con;
                            int d = cmd.ExecuteNonQuery();
                            if (d > 0) { MessageBox.Show("Usunięte"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                    case "z":
                        if (mdb.CheckIfExist(Int32.Parse(s))) { MessageBox.Show("Nie istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
                            MainWindow.w.Clear();
                            toDelete.Clear();
                            cmd.CommandText = "delete from [Zajęcia] where Identyfikator = @nm";
                            cmd.Parameters.AddWithValue("@nm", s);
                            cmd.Connection = con;
                            int f = cmd.ExecuteNonQuery();
                            if (f > 0) { MessageBox.Show("Usunięte"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                }
            } else { MessageBox.Show("Pole musi być uzupełnione"); }
        }
    }
}
