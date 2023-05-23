using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace hotel_pousadas
{
    internal class Conection
    {
        public string strConect = "SERVER=127.0.0.1; DATABASE=hotel; UID=root; PWD=root; PORT=3306";
        public MySqlConnection conn;
        public void AbrirConn()
        {
            try
            {
                conn = new MySqlConnection(strConect);
                conn.Open();
            }
            catch (Exception e)
            {
                Toast.MakeText(Application.Context, e.Message, ToastLength.Long).Show();
            }
        }
        public void FecharConn()
        {
            try
            {
                conn = new MySqlConnection(strConect);
                conn.Close();
            }
            catch (Exception e)
            {
                Toast.MakeText(Application.Context, "Erro ao fechar. " + e.ToString(), ToastLength.Long).Show();
            }
        }
    }
}