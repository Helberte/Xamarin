using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using hotel_pousadas.Resources;
using hotel_pousadas.Resources.DataBaseHelper;
using hotel_pousadas.Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;

namespace hotel_pousadas.fonte
{
    [Activity(Label = "listaPessoas")]
    public class listaPessoas : Activity
    {
        DataBase dataBase;

        TextView txtNomeUsuario;
        ListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.lista_pessoas);

            txtNomeUsuario      = FindViewById<TextView>(Resource.Id.txtNomeUsuario);
            txtNomeUsuario.Text = Intent.GetStringExtra("nome");
    
            listView = FindViewById<ListView>(Resource.Id.listView1);

            dataBase = new DataBase();
            PopulaListView(listView);
        }

        private void PopulaListView(ListView listView)
        {
            List<Aluno> alunos = new List<Aluno>();
            alunos = dataBase.GetAlunos();

            var adapter = new ListAdapter(this, alunos);
            listView.Adapter = adapter;           
        }
    }
}