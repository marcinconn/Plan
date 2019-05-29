using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Configuration;

namespace plan
{
    class MyDB
    {
        public MyDB() { }
        public int FindID(string t, string v)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;

            switch (t)
            {
                case "Sala":
                    string pc1 = "Select Identyfikator from Sala where sal_nazwa='?'";
                    cmd.CommandText = pc1.Replace("?", v.Trim());
                    Int32 ID = (Int32)cmd.ExecuteScalar();
                    return (int)ID;
                case "Nauczyciel":
                    string pc2 = "Select Identyfikator from Nauczyciel where nau_dane='?'";
                    cmd.CommandText = pc2.Replace("?", v.Trim());
                    Int32 ID2 = (Int32)cmd.ExecuteScalar();
                    return (int)ID2;
                case "Klasa":
                    string pc3 = "Select Identyfikator from Klasa where kla_nazwa='?'";
                    cmd.CommandText = pc3.Replace("?", v.Trim());
                    Int32 ID3 = (Int32)cmd.ExecuteScalar();
                    return (int)ID3;
                case "Termin":
                    string pc4 = "Select Identyfikator from Termin where ter_nazwa='?'";
                    cmd.CommandText = pc4.Replace("?", v);
                    Int32 ID4 = (Int32)cmd.ExecuteScalar();
                    return (int)ID4;
                case "Przedmiot":
                    string pc5 = "Select Identyfikator from Przedmiot where prz_nazwa='?'";
                    cmd.CommandText = pc5.Replace("?", v);
                    Int32 ID5 = (Int32)cmd.ExecuteScalar();
                    return (int)ID5;
                case "Dzien":
                    string pc6 = "Select Identyfikator from Dzien where dzi_nazwa='?'";
                    cmd.CommandText = pc6.Replace("?", v);
                    Int32 ID6 = (Int32)cmd.ExecuteScalar();
                    return (int)ID6;
            }

            return 0;
        }

        public int CountEntries(string t, string v)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;

            switch (t)
            {
                case "Sala":
                    string pc1 = "Select Count(Identyfikator) from Sala where sal_nazwa='?'";
                    cmd.CommandText = pc1.Replace("?", v.Trim());
                    Int32 ID = (Int32)cmd.ExecuteScalar();
                    return (int)ID;
                case "Nauczyciel":
                    string pc2 = "Select Count(Identyfikator) from Nauczyciel where nau_dane='?'";
                    cmd.CommandText = pc2.Replace("?", v.Trim());
                    Int32 ID2 = (Int32)cmd.ExecuteScalar();
                    return (int)ID2;
                case "Klasa":
                    string pc3 = "Select Count(Identyfikator) from Klasa where kla_nazwa='?'";
                    cmd.CommandText = pc3.Replace("?", v.Trim());
                    Int32 ID3 = (Int32)cmd.ExecuteScalar();
                    return (int)ID3;
                case "Przedmiot":
                    string pc5 = "Select Count(Identyfikator) from Przedmiot where prz_nazwa='?'";
                    cmd.CommandText = pc5.Replace("?", v);
                    Int32 ID4 = (Int32)cmd.ExecuteScalar();
                    return (int)ID4;
            }

            return 0;
        }

        public void CreateMyOleDbCommand(OleDbConnection connection, string queryString, OleDbParameter[] parameters)
        {
            OleDbCommand command = new OleDbCommand(queryString, connection);
            command.CommandText =
                "SELECT Identyfikator FROM Sala WHERE sal_nazwa = ? ";
            command.Parameters.Add(parameters);

            for (int j = 0; j < parameters.Length; j++)
            {
                command.Parameters.Add(parameters[j]);
            }
        }

        public bool CheckTeacher(int n, int d, int t)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            string q = "SELECT Count(Identyfikator) AS Total FROM Zajęcia WHERE id_nauczyciela = @idN AND id_dnia = @idD AND id_terminu = @idT";
            string fin = q.Replace("@idN", n.ToString()).Replace("@idD", d.ToString()).Replace("@idT", t.ToString());
            cmd.CommandText = fin;
            Int32 ID = (Int32)cmd.ExecuteScalar();
            con.Close();
            if (ID > 0) return true; else return false;
        }

        public bool CheckRoom(int r, int d, int t)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            string q = "SELECT Count(Identyfikator) AS Total FROM Zajęcia WHERE id_sali=@idS AND id_dnia=@idD AND id_terminu=@idT";
            string fin = q.Replace("@idS", r.ToString()).Replace("@idD", d.ToString()).Replace("@idT", t.ToString());
            cmd.CommandText = fin;
            Int32 ID = (Int32)cmd.ExecuteScalar();
            con.Close();
            if (ID > 0) return true; else return false;
        }

        public bool CheckClass(int k, int d, int t)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            string q = "SELECT Count(Identyfikator) AS Total FROM Zajęcia WHERE id_klasy=@idK AND id_dnia=@idD AND id_terminu=@idT";
            string fin = q.Replace("@idK", k.ToString()).Replace("@idD", d.ToString()).Replace("@idT", t.ToString());
            cmd.CommandText = fin;
            Int32 ID = (Int32)cmd.ExecuteScalar();
            con.Close();
            if (ID > 0) return true; else return false;
        }

        public bool CheckIfExist(int id)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            string q = "SELECT Count(Identyfikator) FROM Zajęcia WHERE Identyfikator=?";
            string fin = q.Replace("?", id.ToString());
            cmd.CommandText = fin;
            Int32 ID = (Int32)cmd.ExecuteScalar();
            con.Close();
            if (ID > 0) return false; else return true;
        }
    }
}
