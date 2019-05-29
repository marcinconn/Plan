using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
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
    /// Logika interakcji dla klasy Lesson.xaml
    /// </summary>
    public partial class Lesson : Window
    {
        public Lesson()
        {
            InitializeComponent();
            LoadCombos2();
        }      

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (roomToAdd.SelectedItem != null && teacherToAdd.SelectedItem != null && hoursToAdd.SelectedItem != null && dayToAdd.SelectedItem != null && classToAdd.SelectedItem != null && subjectToAdd.SelectedItem != null)
            {
                MyDB m = new MyDB();

                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                
                int roomID = m.FindID("Sala", roomToAdd.SelectedItem.ToString());
                int classID = m.FindID("Klasa", classToAdd.SelectedItem.ToString());
                int hoursID = m.FindID("Termin", hoursToAdd.SelectedItem.ToString());
                int teacherID = m.FindID("Nauczyciel", teacherToAdd.SelectedItem.ToString());
                int subjID = m.FindID("Przedmiot", subjectToAdd.SelectedItem.ToString());
                int dayID = m.FindID("Dzien", dayToAdd.SelectedItem.ToString());

                bool t1 = m.CheckTeacher(teacherID, dayID, hoursID);
                bool t2 = m.CheckClass(classID, dayID, hoursID);
                bool t3 = m.CheckRoom(roomID,dayID,hoursID);

                if (t1==true||t2==true||t3==true) { MessageBox.Show("Kolizja zajęć"); }
                else
                {
                    cmd.CommandText = "insert into [Zajęcia](id_klasy,id_sali,id_nauczyciela,id_dnia,id_terminu,id_przedmiotu)Values(@idK,@idS,@idN,@idD,@idT,@idP);";
                    cmd.Parameters.AddWithValue("@idK", classID.ToString());
                    cmd.Parameters.AddWithValue("@idS", roomID.ToString());
                    cmd.Parameters.AddWithValue("@idN", teacherID.ToString());
                    cmd.Parameters.AddWithValue("@idD", dayID.ToString());
                    cmd.Parameters.AddWithValue("@idT", hoursID.ToString());
                    cmd.Parameters.AddWithValue("@idP", subjID.ToString());

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                con.Close();

                this.DialogResult = true;
            }
        }

        private void LoadCombos2()
        {
            List<String> sData = new List<String>();
            List<String> kData = new List<String>();
            List<String> nData = new List<String>();
            List<String> dData = new List<String>();
            List<String> tData = new List<String>();
            List<String> uData = new List<String>();
            
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
                        sData.Add(reader.GetString(0));
                    }
                }
                cmd.CommandText = "SELECT nau_dane FROM Nauczyciel";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nData.Add(reader.GetString(0));
                    }
                }
                cmd.CommandText = "SELECT kla_nazwa FROM Klasa";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        kData.Add(reader.GetString(0));
                    }
                }
                cmd.CommandText = "SELECT dzi_nazwa FROM Dzien";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dData.Add(reader.GetString(0));
                    }
                }
                cmd.CommandText = "SELECT ter_nazwa FROM Termin";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tData.Add(reader.GetString(0));
                    }
                }
                cmd.CommandText = "SELECT prz_nazwa FROM Przedmiot";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        uData.Add(reader.GetString(0));
                    }
                }
                hoursToAdd.ItemsSource = tData;
                subjectToAdd.ItemsSource = uData;
                dayToAdd.ItemsSource = dData;
                classToAdd.ItemsSource = kData;
                roomToAdd.ItemsSource = sData;
                teacherToAdd.ItemsSource = nData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }
    }
}
