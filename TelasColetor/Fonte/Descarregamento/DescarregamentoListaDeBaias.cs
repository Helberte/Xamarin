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
using static Android.Icu.Text.CaseMap;
using static TelasColetor.Fonte.SepararPaleteListaDeBaias;

namespace TelasColetor.Fonte.Descarregamento
{
    [Activity(Label = "DescarregamentoListaDeBaias")]
    public class DescarregamentoListaDeBaias : Activity
    {
        TextView     descarregamento_lista_de_baias_doca;
        TextView     descarregamento_lista_de_baias_placa;
        RecyclerView descarregamento_lista_de_baias_placa_recycler;
        EditText     descarregamento_lista_de_baias_placa_leia_endereco;

        DescarregamentoListaPlacas.Placas placa;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DescarregamentoListaDeBaias);

            descarregamento_lista_de_baias_doca                = FindViewById<TextView>(Resource.Id.descarregamento_lista_de_baias_doca);
            descarregamento_lista_de_baias_placa               = FindViewById<TextView>(Resource.Id.descarregamento_lista_de_baias_placa);
            descarregamento_lista_de_baias_placa_recycler      = FindViewById<RecyclerView>(Resource.Id.descarregamento_lista_de_baias_placa_recycler);
            descarregamento_lista_de_baias_placa_leia_endereco = FindViewById<EditText>(Resource.Id.descarregamento_lista_de_baias_placa_leia_endereco);

            placa = JsonConvert.DeserializeObject<DescarregamentoListaPlacas.Placas>(Intent.GetStringExtra("placa"));

            //descarregamento_lista_de_baias_placa_leia_endereco.RequestFocus();

            descarregamento_lista_de_baias_doca.Text  = Intent.GetStringExtra("doca");
            descarregamento_lista_de_baias_placa.Text = placa.Placa;

            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 1);
            RecyclerAdapter adapter = new RecyclerAdapter(GetBaias());
            adapter.ItemClick += Adapter_ItemClick;
            descarregamento_lista_de_baias_placa_recycler.SetLayoutManager(gridLayoutManager);
            descarregamento_lista_de_baias_placa_recycler.HasFixedSize = true;
            descarregamento_lista_de_baias_placa_recycler.SetAdapter(adapter);
        }


        private void Adapter_ItemClick(object sender, int e)
        {
            int position = e;

            RecyclerAdapter adapter = descarregamento_lista_de_baias_placa_recycler.GetAdapter() as RecyclerAdapter;
            int baia = adapter.items[position];
            
            Mensagens mensagens = new Mensagens();

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog alerta = builder.Create();
            alerta.SetTitle("Sucesso");
            alerta.SetIcon(Resource.Drawable.icons8_done_96);
            alerta.SetMessage("Descarregamento iniciado da doca " + descarregamento_lista_de_baias_doca.Text +
                                                                        " para a baia " + baia.ToString().PadLeft(2, '0') + ".");
            alerta.SetButton("OK", (s, ev) =>
            {
                StartActivity(typeof(DescarregamentoMenuPrincipal));
            });
            alerta.Show();
        }

        private class RecyclerAdapter : RecyclerView.Adapter
        {
            public List<int> items;

            public RecyclerAdapter(List<int> items)
            {
                this.items = items;
            }

            public event EventHandler<int> ItemClick;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                int item = items[position];

                RecyclerHolder recycler = holder as RecyclerHolder;
                recycler.textview_baia.Text = item.ToString().PadLeft(2,'0');
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

        public List<int> GetBaias()
        {
            List<int> list = new List<int>();
            Random random = new Random();

            for (int i = 0; i < random.Next(1,15); i++)
            {
                list.Add( random.Next(1,40) );
            }
            list = list.OrderBy(x => x).ToList();
            list = list.Distinct().ToList();

            return list;
        }
    }
}