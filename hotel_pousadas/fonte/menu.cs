using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using hotel_pousadas.Resources.DataBaseHelper;
using hotel_pousadas.Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hotel_pousadas.fonte
{
    [Activity(Label = "menu")]
    public class menu : Activity
    {
        TextView txtNomeUsuario;
        TextView edNomePessoa;
        TextView edIdadePessoa;
        TextView edEmailPessoa;
        TextView txtCaminhoBanco;
        Button btCadastrar;
        Button btListarPessoas;

        Aluno aluno;
        DataBase dataBase;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // linca o fonte com a tela que este fonte irá fazer funcionar
            SetContentView(Resource.Layout.menu);


            txtNomeUsuario = FindViewById<TextView>(Resource.Id.txtNomeUsuario);
            txtNomeUsuario.Text = Intent.GetStringExtra("nome");

            edNomePessoa  = FindViewById<TextView>(Resource.Id.edNomePessoa);
            edIdadePessoa = FindViewById<TextView>(Resource.Id.edIdadePessoa);
            edEmailPessoa = FindViewById<TextView>(Resource.Id.edEmailPessoa);
            txtCaminhoBanco = FindViewById<TextView>(Resource.Id.txtCaminhoBanco);


            btCadastrar = FindViewById<Button>(Resource.Id.btCadastrar);
            btListarPessoas = FindViewById<Button>(Resource.Id.btListar);

            btCadastrar.Click += BtCadastrar_Click;
            btListarPessoas.Click += BtListarPessoas_Click;

            // define a cor de um texto
            txtNomeUsuario.SetTextColor(Android.Graphics.Color.Black);

            dataBase = new DataBase();

            if (!dataBase.CriarBancoDeDados())            
                Toast.MakeText(Application.Context, "Falha ao criar banco.", ToastLength.Long).Show();

            txtCaminhoBanco.Text = Variaveis.caminho_banco;

            aluno = new Aluno();
        }

        private void BtListarPessoas_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(listaPessoas));
        }

        private void BtCadastrar_Click(object sender, EventArgs e)
        {
            aluno.Nome = edNomePessoa.Text;
            aluno.Email = edEmailPessoa.Text;
            aluno.Idade = Convert.ToInt32(edIdadePessoa.Text);

            if (!dataBase.InserirAluno(aluno))
                Toast.MakeText(Application.Context, "Impossível inserir aluno.", ToastLength.Long).Show();
            else
                Toast.MakeText(Application.Context, "Pessoa Cadastrada.", ToastLength.Long).Show();
        }
    }
}