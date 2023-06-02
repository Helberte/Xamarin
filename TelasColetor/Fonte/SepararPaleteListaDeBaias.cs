using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelasColetor.Fonte
{
    [Activity(Label = "SepararPaleteListaDeBaias")]
    public class SepararPaleteListaDeBaias : Activity
    {
        RecyclerView separar_palete_lista_de_baias_recycler;
        TextView textView_filial;
        TextView textView_etiqueta;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SepararPaleteListaDeBaias);

            // obtem o recycler view
            separar_palete_lista_de_baias_recycler = FindViewById<RecyclerView>(Resource.Id.separar_palete_lista_de_baias_recycler);
            textView_filial                        = FindViewById<TextView>(Resource.Id.textView_filial);
            textView_etiqueta                      = FindViewById<TextView>(Resource.Id.textView_etiqueta);

            textView_filial.Text   = Intent.GetStringExtra("filial");
            textView_etiqueta.Text = Intent.GetStringExtra("etiqueta");

            // cria o objeto do layout
            GridLayoutManager gridLayout = new GridLayoutManager(this, 1);

            // gera as baias e passa para o adaptador 
            RecyclerAdapter adapter = new RecyclerAdapter(GetBaias((new Random()).Next(1,20)));

            // registra o evento do click nos itens
            adapter.ItemClick += Adapter_ItemClick;

            // coloca o layout para o recycler view
            separar_palete_lista_de_baias_recycler.SetLayoutManager(gridLayout);
            separar_palete_lista_de_baias_recycler.HasFixedSize = true;
                       
            // coloca o adapter no recycler view
            separar_palete_lista_de_baias_recycler.SetAdapter(adapter);              
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            throw new NotImplementedException();
        }

        private class RecyclerAdapter : RecyclerView.Adapter
        {
            public List<Baias> items;
             
            public RecyclerAdapter(List<Baias> items)
            {
                this.items = items;
            } 

            public event EventHandler<int> ItemClick;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                Baias item = items[position];

                RecyclerHolder recycler     = holder as RecyclerHolder;
                recycler.textview_baia.Text = item.NumeroBaia;               
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SepararPaleteListaDeBaiasItem, parent, false);

                RecyclerHolder vh = new RecyclerHolder(itemView, OnClick);
                return vh;
            }

            public void OnClick(int position)
            {
                ItemClick?.Invoke(this, position);
            }

            public override int ItemCount
            {
                get
                {
                    return this.items.Count;
                }
            }
        }

        public class RecyclerHolder : RecyclerView.ViewHolder
        {
            public TextView textview_baia { get; private set; }

            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                textview_baia = itemView.FindViewById<TextView>(Resource.Id.textview_baia);

                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }

        /// <summary>
        /// Gera uma quantidade randomica de baias, de acordo com o parâmetro passado 
        /// </summary>
        /// <param name="quantidadeBaias"></param>
        /// <returns>Retorna uma List<Baias> </returns>
        /// <remarks>Helberte Costa, 01/06/2023</remarks>   
        public List<Baias> GetBaias(int quantidadeBaias)
        {
            Random random = new Random();
            List<Baias> lista = new List<Baias>();

            for (int i = 0; i < quantidadeBaias; i++)
            {
                lista.Add(new Baias { NumeroBaia = random.Next(1, 40).ToString().PadLeft(2,'0') });
            }
            return lista;
        }

        public class Baias
        { 
            public string NumeroBaia { get; set; }
        }
    }
}