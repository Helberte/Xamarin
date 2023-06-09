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
    [Activity(Label = "DescarregamentoConsultarProdutoListaBaias")]
    public class DescarregamentoConsultarProdutoListaBaias : Activity
    {
        ListView descarregamento_consultar_produto_lista_baias_listview;
        Button descarregamento_consultar_produto_lista_baias_botao_voltar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DescarregamentoConsultarProdutoListaBaias);

            descarregamento_consultar_produto_lista_baias_listview     = FindViewById<ListView>(Resource.Id.descarregamento_consultar_produto_lista_baias_listview);
            descarregamento_consultar_produto_lista_baias_botao_voltar = FindViewById<Button>(Resource.Id.descarregamento_consultar_produto_lista_baias_botao_voltar);

            descarregamento_consultar_produto_lista_baias_listview.Adapter    = new ListViewAdapter(GetCargasBaias());
            descarregamento_consultar_produto_lista_baias_listview.ItemClick += Descarregamento_consultar_produto_lista_baias_listview_ItemClick;
            descarregamento_consultar_produto_lista_baias_botao_voltar.Click += (sender, e) => this.Finish();
        }

        private void Descarregamento_consultar_produto_lista_baias_listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int posicao = e.Position;

            ListViewAdapter adapter = descarregamento_consultar_produto_lista_baias_listview.Adapter as ListViewAdapter;

            string baia = adapter.items[posicao].Baia;

            Intent intent = new Intent(this, typeof(DescarregamentoConsultarProdutoLeiaProduto));
            intent.PutExtra("baia", baia);

            StartActivity(intent);
        }

        public class ListViewAdapter : BaseAdapter<ModelListViewBaias>
        {
            public List<ModelListViewBaias> items;

            public ListViewAdapter(List<ModelListViewBaias> items)
            {
                this.items = items;
            }

            public override ModelListViewBaias this[int position]
            {
                get
                {
                    return items[position];
                }
            }

            public override int Count
            {
                get
                {
                    return items.Count;
                }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = convertView;

                if (view == null)
                {
                    view = Android.Views.LayoutInflater.From(parent.Context).Inflate(Resource.Layout.DescarregamentoConsultarProdutoListaBaiasItem, parent, false);

                    TextView textView_baia = view.FindViewById<TextView>(Resource.Id.textView_baia);
                    TextView textView_carga = view.FindViewById<TextView>(Resource.Id.textView_carga);

                    view.Tag = new ViewHolder() { TextView_baia = textView_baia, TextView_carga = textView_carga };
                }
                ViewHolder holder = (ViewHolder)view.Tag;

                holder.TextView_baia.Text = items[position].Baia;
                holder.TextView_carga.Text = items[position].Carga;

                return view;
            }
        }

        public class ViewHolder : Java.Lang.Object
        {
            public TextView TextView_baia { get; set; }
            public TextView TextView_carga { get; set; }
        }

        public class ModelListViewBaias
        {
            public string Baia { get; set; }
            public string Carga { get; set; }
        } 

        public List<ModelListViewBaias> GetCargasBaias()
        {
            Random random = new Random();
            List<ModelListViewBaias> list = new List<ModelListViewBaias>();

            for (int i = 0; i < random.Next(1,20); i++)
            {
                list.Add(new ModelListViewBaias() { Baia  = random.Next(0,40).ToString().PadLeft(2,'0'), 
                                                    Carga = random.Next(10000, 77777).ToString().PadLeft(8, '0') });
            }
            list = list.OrderBy(x => x.Baia).ToList();
            list = list.Distinct().ToList();

            return list;
        }
    }
}