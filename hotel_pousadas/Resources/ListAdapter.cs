using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using hotel_pousadas.Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hotel_pousadas.Resources
{
    public class ListAdapter : BaseAdapter
    {
        private readonly Activity context;
        private readonly List<Aluno> alunos;

        public ListAdapter(Activity _context, List<Aluno> _alunos)
        { 
            this.context = _context;
            this.alunos = _alunos;
        }

        public override int Count
        {
            get
            {
                return alunos.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return alunos[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.lista_pessoas, parent, false);

            var lvtxtNome = view.FindViewById<TextView>(Resource.Id.textView1);
            var lvtxtIdade = view.FindViewById<TextView>(Resource.Id.textView2);
            var lvtxtEmail = view.FindViewById<TextView>(Resource.Id.textView3);

            lvtxtNome.Text = alunos[position].Nome;
            lvtxtIdade.Text = "" + alunos[position].Idade;
            lvtxtEmail.Text = alunos[position].Email;

            return view;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
    }
}