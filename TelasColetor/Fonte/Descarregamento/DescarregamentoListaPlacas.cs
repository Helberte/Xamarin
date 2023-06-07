using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static TelasColetor.Fonte.SeparacaoFracionada.SeparacaoFracionadaListaBaias;

namespace TelasColetor.Fonte.Descarregamento
{
    [Activity(Label = "DescarregamentoListaPlacas")]
    public class DescarregamentoListaPlacas : Activity
    {
        RecyclerView descarregamento_leia_doca_recycler_placas;
        TextView descarregamento_lista_placas_doca;
    
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DescarregamentoListaPlacas);

            descarregamento_leia_doca_recycler_placas    = FindViewById<RecyclerView>(Resource.Id.descarregamento_leia_doca_recycler_placas);
            descarregamento_lista_placas_doca            = FindViewById<TextView>(Resource.Id.descarregamento_lista_placas_doca);
         
            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 1);

            RecyclerAdapter adapter = new RecyclerAdapter(GetPlacas());
            adapter.ItemClick += Adapter_ItemClick;

            descarregamento_leia_doca_recycler_placas.SetLayoutManager(gridLayoutManager);
            descarregamento_leia_doca_recycler_placas.HasFixedSize = true;
            descarregamento_leia_doca_recycler_placas.SetAdapter(adapter);
        }


        private void Adapter_ItemClick(object sender, int e)
        {
            int position = e;

            RecyclerAdapter adapter = descarregamento_leia_doca_recycler_placas.GetAdapter() as RecyclerAdapter;
            Placas placa = adapter.items[position];

            string strPlaca = JsonConvert.SerializeObject(placa);

            Intent intent = new Intent(this, typeof(DescarregamentoListaDeBaias));
            intent.PutExtra("placa", strPlaca);
            intent.PutExtra("doca", descarregamento_lista_placas_doca.Text);

            StartActivity(intent);
        }

        public class RecyclerAdapter : RecyclerView.Adapter
        {
            public List<Placas> items;

            public RecyclerAdapter(List<Placas> items)
            {
                this.items = items;
            }

            public override int ItemCount
            {
                get
                {
                    return items.Count;
                }
            }

            public event EventHandler<int> ItemClick;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                Placas placa = items[position];

                RecyclerHolder recycler = holder as RecyclerHolder;
                recycler.Descarregamento_lista_placas_item_placa.Text = placa.Placa;
                recycler.Descarregamento_lista_placas_item_data.Text = placa.Data;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.DescarregamentoListaPlacasItem, parent, false);

                RecyclerHolder vh = new RecyclerHolder(itemView, OnClick);
                return vh; 
            }

            public void OnClick(int position)
            {
                ItemClick?.Invoke(this, position);
            }
        }

        public class RecyclerHolder : RecyclerView.ViewHolder
        {
            public TextView Descarregamento_lista_placas_item_placa { get; private set; }
            public TextView Descarregamento_lista_placas_item_data { get; private set; }
             
            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Descarregamento_lista_placas_item_placa = itemView.FindViewById<TextView>(Resource.Id.descarregamento_lista_placas_item_placa);
                Descarregamento_lista_placas_item_data = itemView.FindViewById<TextView>(Resource.Id.descarregamento_lista_placas_item_data);

                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }

        public List<Placas> GetPlacas()
        {
            Random random = new Random();
            List<Placas> placas = new List<Placas>();
            string[] letras = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };

            for (int i = 0; i < random.Next(1,5); i++)
            {
                string mes = random.Next(1,12).ToString().PadLeft(2,'0');
                int diasNoMes = DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(mes));
                string dia = random.Next(1, diasNoMes).ToString().PadLeft(2,'0');

                placas.Add(new Placas() { Data = $"{dia}/{mes}/{DateTime.Now.Year}", 
                                         Placa = letras[random.Next(0, letras.Length - 1)] +
                                                 letras[random.Next(0, letras.Length - 1)] +
                                                 letras[random.Next(0, letras.Length - 1)] + 
                                                 random.Next(0, 9999)
                });
            }

            return placas;
        }

        public class Placas
        { 
            public string Placa { get; set; }
            public string Data { get; set; }
        }
    } 
}