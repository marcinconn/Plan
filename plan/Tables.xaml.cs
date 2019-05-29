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
    /// Logika interakcji dla klasy Tables.xaml
    /// </summary>
    public partial class Tables : Window
    {
        public Tables()
        {
            InitializeComponent();
        }

        public void SetGrid(string t, string p)
        {
            try
            {
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                string q = "SELECT Identyfikator, prz_nazwa AS Nazwa FROM Przedmiot;";
                cmd.CommandText = q.Replace("prz_nazwa", p).Replace("Przedmiot", t);
                Console.WriteLine(cmd.CommandText);
                OleDbDataReader dr = cmd.ExecuteReader();
                if(dr!=null) {tabela.ItemsSource = dr; tabela.DataContext = dr;}
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

    }
}
