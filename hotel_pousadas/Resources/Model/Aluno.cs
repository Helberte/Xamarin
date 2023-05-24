using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hotel_pousadas.Resources.Model
{
    public class Aluno
    {
        [PrimaryKey, AutoIncrement] // faz com que o campo id, seja chave primaria auto-increment
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }
    }
}