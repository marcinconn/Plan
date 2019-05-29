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
    /// Logika interakcji dla klasy EditTreeView.xaml
    /// </summary>
    public partial class EditTreeView : Window
    {
        public EditTreeView()
        {
            InitializeComponent();
        }

        public String newVal(TextBox tb)
        {
            String val = tb.Text;
            return val;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            String se1 = oldValueEdit.Text;
            String sNew = newValueEdit.Text;
            String s3 = MainWindow.w.ToString();
            MyDB mdb = new MyDB();

            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();

            if (!string.IsNullOrEmpty(oldValueEdit.Text) && !string.IsNullOrEmpty(newValueEdit.Text))
            {
                switch (s3)
                {
                    case "s":
                        if (!Regex.IsMatch(newValueEdit.Text, "[0-9]+") || mdb.FindID("Sala", oldValueEdit.Text) <= 0) { MessageBox.Show("Sala nie istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
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
                            cmd.CommandText = "update Sala set sal_nazwa = @newval where sal_nazwa = @oldval;";
                            cmd.Parameters.AddWithValue("@newval", sNew);
                            cmd.Parameters.AddWithValue("@oldval", se1);
                            cmd.Connection = con;
                            int a = cmd.ExecuteNonQuery();
                            if (a > 0) { MessageBox.Show("Zmienione"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                    case "n":
                        if (!Regex.IsMatch(newValueEdit.Text, "[^0-9]*") || mdb.FindID("Nauczyciel", oldValueEdit.Text) <= 0) { MessageBox.Show("Nauczyciel nie istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
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
                            cmd.CommandText = "update Nauczyciel set sal_nazwa = @newval where sal_nazwa = @oldval;";
                            cmd.Parameters.AddWithValue("@newval", sNew);
                            cmd.Parameters.AddWithValue("@oldval", se1);
                            cmd.Connection = con;
                            int b = cmd.ExecuteNonQuery();
                            if (b > 0) { MessageBox.Show("Zmienione"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                    case "k":
                        if (!Regex.IsMatch(newValueEdit.Text, "[0-9]{1}[a-zA-Z]{1}") || mdb.FindID("Klasa", oldValueEdit.Text) <= 0) { MessageBox.Show("Klasa nie istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
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
                            cmd.CommandText = "update Klasa set sal_nazwa = @newval where sal_nazwa = @oldval;";
                            cmd.Parameters.AddWithValue("@newval", sNew);
                            cmd.Parameters.AddWithValue("@oldval", se1);
                            cmd.Connection = con;
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0) { MessageBox.Show("Zmienione"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                    case "p":
                        if (!Regex.IsMatch(newValueEdit.Text, "[^0-9]*") || mdb.FindID("Przedmiot", oldValueEdit.Text) <= 0) { MessageBox.Show("Przedmiot nie istnieje lub niepoprawny format danych wejściowych"); break; }
                        else
                        {
                            newValueEdit.Clear();
                            oldValueEdit.Clear();
                            MainWindow.w.Clear();
                            cmd.CommandText = "update Przedmiot set prz_nazwa = @newval where prz_nazwa = @oldval;";
                            cmd.Parameters.AddWithValue("@newval", sNew);
                            cmd.Parameters.AddWithValue("@oldval", se1);
                            cmd.Connection = con;
                            int d = cmd.ExecuteNonQuery();
                            if (d > 0) { MessageBox.Show("Zmienione"); }
                            con.Close();
                            this.DialogResult = true;
                            break;
                        }
                }
            } else { MessageBox.Show("Pola muszą być wypełnione"); }
        }
    }
}
