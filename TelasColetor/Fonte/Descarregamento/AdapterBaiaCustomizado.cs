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

namespace TelasColetor.Fonte.Descarregamento
{
    public class AdapterBaiaCustomizado : BaseAdapter<ModelListaBaias>
    {
        List<ModelListaBaias> modelListaBaias;

        public AdapterBaiaCustomizado(List<ModelListaBaias> modelListaBaias)
        {
            this.modelListaBaias = modelListaBaias;
        }

        public override ModelListaBaias this[int position]
        {
            get 
            { 
                return modelListaBaias[position];
            }
        }

        public override int Count
        {
            get
            {
                return modelListaBaias.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.DescarregamentoCancelarListaBaiasItem, parent, false);
                TextView textview_baia   = view.FindViewById<TextView>(Resource.Id.textview_baia);
                ImageView imageView_baia = view.FindViewById<ImageView>(Resource.Id.imageView_baias);

                view.Tag = new ViewHolder() { Textview_baia = textview_baia, ImagemBaia = imageView_baia };
            }

            var holder = (ViewHolder)view.Tag;

            holder.Textview_baia.Text = modelListaBaias[position].NumeroBaia.ToString();
            holder.ImagemBaia.SetImageResource(modelListaBaias[position].ImagemBaia);

            return view;
        }
    }
}