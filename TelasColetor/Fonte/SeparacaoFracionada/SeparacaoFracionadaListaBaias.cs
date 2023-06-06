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
using static TelasColetor.Fonte.SepararPaleteListaDeBaias;

namespace TelasColetor.Fonte.SeparacaoFracionada
{
    [Activity(Label = "SeparacaoFracionadaListaBaias")]
    public class SeparacaoFracionadaListaBaias : Activity
    {
        TextView separacao_fracionada_lista_baias_filial;
        TextView separacao_fracionada_lista_baias_etiqueta;
        RecyclerView separacao_fracionada_lista_baias_recycler;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionadaListaBaias);

            separacao_fracionada_lista_baias_filial   = FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_baias_filial);
            separacao_fracionada_lista_baias_etiqueta = FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_baias_etiqueta);
            separacao_fracionada_lista_baias_recycler = FindViewById<RecyclerView>(Resource.Id.separacao_fracionada_lista_baias_recycler);

            separacao_fracionada_lista_baias_filial.Text   = Intent.GetStringExtra("filial");
            separacao_fracionada_lista_baias_etiqueta.Text = Intent.GetStringExtra("etiqueta");

            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 1);

            List<Baia> baias = new List<Baia>();
            List<Baia> baiasOrdenada = new List<Baia>();
            List<Baia> baiasFinal = new List<Baia>();

            int limite = (new Random()).Next(2, 20);

            for (int i = 0; i < limite; i++)                           
                baias.Add(new Baia() { Numero = (new Random()).Next(2, 20) });
            
            // ordena a lista de baias pelo numero
            baiasOrdenada = baias.OrderBy(a => a.Numero).ToList();

            // retira os iguais e deixa somente os que não se repetem
            // baiasFinal = baiasOrdenada.Distinct().ToList();
            try
            {
                int valor = 0;
                for (int i = 0; i < baiasOrdenada.Count; i++)
                {
                    List<Baia> baias1 = baiasOrdenada.FindAll(a => a.Numero == baiasOrdenada[i].Numero).ToList();

                    if (baias1.Count > 1)
                    {
                        valor = baias1[0].Numero;

                        foreach (Baia item in baias1)
                        {
                            baiasOrdenada.RemoveAll(a => a.Numero == baiasOrdenada[i].Numero);
                        }
                        baiasFinal.Add(new Baia { Numero = valor });
                    }
                    else
                    {
                        baiasFinal.Add(baiasOrdenada[i]);
                    }
                }
            }
            catch (Exception) { }            
            
            RecyclerAdapter adapter = new RecyclerAdapter(baiasFinal);
            adapter.ItemClick += Adapter_ItemClick;

            separacao_fracionada_lista_baias_recycler.SetLayoutManager(gridLayoutManager);
            separacao_fracionada_lista_baias_recycler.HasFixedSize = true;
            separacao_fracionada_lista_baias_recycler.SetAdapter(adapter);
        }

        private void Adapter_ItemClick(object sender, int e) {  }

        public class RecyclerAdapter : RecyclerView.Adapter
        {
            List<Baia> baias;

            public RecyclerAdapter(List<Baia> baias)
            {
                this.baias = baias;
            }

            public override int ItemCount
            {
                get
                {
                    return baias.Count;
                }
            }

            public event EventHandler<int> ItemClick;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                Baia item = baias[position];

                RecyclerHolder recycler = holder as RecyclerHolder;
                recycler.textViewBaia.Text = item.Numero.ToString();
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SepararPaleteListaDeBaiasItem, parent, false);

                RecyclerHolder vh = new RecyclerHolder(itemView, OnClick);
                return vh;
            }

            public void OnClick(int posicion)
            {
                ItemClick?.Invoke(this, posicion);
            }
        }

        public class RecyclerHolder : RecyclerView.ViewHolder
        {
            public TextView textViewBaia { get; private set; }

            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                textViewBaia = itemView.FindViewById<TextView>(Resource.Id.textview_baia);

                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }

        public class Baia
        {
            public int Numero { get; set; } 
        }
    }
}