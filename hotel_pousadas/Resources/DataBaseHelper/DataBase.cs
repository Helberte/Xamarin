using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using hotel_pousadas.Resources.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hotel_pousadas.Resources.DataBaseHelper
{
    public class DataBase
    {
        string pasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool CriarBancoDeDados()
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Alunos.db")))
                {
                    conexao.CreateTable<Aluno>();
                    Variaveis.caminho_banco = conexao.DatabasePath;
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool InserirAluno(Aluno aluno)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Alunos.db")))
                {
                    conexao.Insert(aluno);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<Aluno> GetAlunos()
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Alunos.db")))
                {
                    return conexao.Table<Aluno>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool AtualizarAluno(Aluno aluno)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Alunos.db")))
                {
                    conexao.Query<Aluno>("UPDATE Aluno set Nome=?,Idade=?, Email=? Where Id=?", aluno.Nome, aluno.Idade, aluno.Email, aluno.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool DeletarAluno(Aluno aluno)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Alunos.db")))
                {
                    conexao.Delete(aluno);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool GetAluno(int Id)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Alunos.db")))
                {
                    conexao.Query<Aluno>("SELECT * FROM Aluno Where Id=?", Id);
                    //conexao.Update(aluno);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

    }
}