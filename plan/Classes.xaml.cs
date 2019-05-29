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
    /// Logika interakcji dla klasy Classes.xaml
    /// </summary>
    public partial class Classes : Window
    {
        public Classes()
        {
            InitializeComponent();
            SetClasses();
        }

        private void SetClasses()
        {
            try
            {
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT Zajęcia.Identyfikator, Klasa.kla_nazwa, " +
                    "Sala.sal_nazwa, Nauczyciel.nau_dane, Dzien.dzi_nazwa, Termin.ter_nazwa, " +
                    "Przedmiot.prz_nazwa FROM Dzien INNER JOIN(Nauczyciel " +
                    "INNER JOIN (Sala INNER JOIN (Klasa INNER JOIN (Przedmiot INNER JOIN (Termin INNER JOIN Zajęcia ON Termin.Identyfikator = Zajęcia.id_terminu) ON Przedmiot.Identyfikator = Zajęcia.id_przedmiotu) ON Klasa.Identyfikator = Zajęcia.id_klasy) ON Sala.Identyfikator = Zajęcia.id_sali) ON Nauczyciel.Identyfikator = Zajęcia.id_nauczyciela) ON Dzien.Identyfikator = Zajęcia.id_dnia;";
                Console.WriteLine(cmd.CommandText);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr != null) { zajecia.ItemsSource = dr; zajecia.DataContext = dr; }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
