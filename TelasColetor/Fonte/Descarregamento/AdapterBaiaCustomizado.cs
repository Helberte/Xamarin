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
        public List<ModelListaBaias> modelListaBaias;

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
            var item = modelListaBaias[position];
            var view = convertView;

            view ??= LayoutInflater.From(parent.Context).Inflate(Resource.Layout.DescarregamentoCancelarListaBaiasItem, parent, false);

            view.FindViewById<TextView>(Resource.Id.textview_baia).Text = item.NumeroBaia.ToString();
            view.FindViewById<ImageView>(Resource.Id.imageView_baias).SetImageResource(item.ImagemBaia);

            return view;
        }
    }
}