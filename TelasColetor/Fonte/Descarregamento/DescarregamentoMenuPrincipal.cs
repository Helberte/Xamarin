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

namespace TelasColetor.Fonte.Descarregamento
{
    [Activity(Label = "DescarregamentoMenuPrincipal")]
    public class DescarregamentoMenuPrincipal : Activity
    {
        RecyclerView recyclerView_descarregamento_menu_principal;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DescarregamentoMenuPrincipal);

            recyclerView_descarregamento_menu_principal = FindViewById<RecyclerView>(Resource.Id.recyclerView_descarregamento_menu_principal);

            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 2);
            RecyclerAdapter adapter = new RecyclerAdapter(GetMenus());
            adapter.ItemClick += Adapter_ItemClick;
            recyclerView_descarregamento_menu_principal.SetLayoutManager(gridLayoutManager);
            recyclerView_descarregamento_menu_principal.HasFixedSize = true;
            recyclerView_descarregamento_menu_principal.SetAdapter(adapter);
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            int position = e;

            RecyclerAdapter adapter = recyclerView_descarregamento_menu_principal.GetAdapter() as RecyclerAdapter;
            string rota = adapter.items[position].Rota;

            Type type = System.Type.GetType(rota);

            StartActivity(type);
        }

        public class RecyclerAdapter : RecyclerView.Adapter
        {
            public List<Menu> items;

            public RecyclerAdapter(List<Menu> items)
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
                Menu menu = items[position];

                RecyclerHolder recycler = holder as RecyclerHolder;
                recycler.Menu_Coletor_Item_Descricao.Text = menu.Nome;
                recycler.Menu_Coletor_Item_Icone.SetImageResource(menu.Icone);
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Menu_Coletor_Item, parent, false);

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
            public ImageView Menu_Coletor_Item_Icone { get; private set; }
            public TextView Menu_Coletor_Item_Descricao { get; private set; }

            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Menu_Coletor_Item_Icone     = itemView.FindViewById<ImageView>(Resource.Id.Menu_Coletor_Item_Icone);
                Menu_Coletor_Item_Descricao = itemView.FindViewById<TextView>(Resource.Id.Menu_Coletor_Item_Descricao);

                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }

        public List<Menu> GetMenus()
        {
            List<Menu> list = new List<Menu>
            {
                new Menu() { Icone = Resource.Drawable.icons8_in_transit_96_3,
                             Nome = "Iniciar Descarregamento",
                             Rota = "TelasColetor.Fonte.Descarregamento.DescarregamentoLeiaDoca"},

                new Menu() { Icone = Resource.Drawable.icons8_in_transit_96_2,
                             Nome = "Finalizar Descarregamento",
                             Rota = "TelasColetor.Fonte.Descarregamento.DescarregamentoFinalizarLeiaBaia"},

                new Menu() { Icone = Resource.Drawable.icons8_cancel_96,
                             Nome = "Cancelar Descarregamento",
                             Rota = "TelasColetor.Fonte.Descarregamento.DescarregamentoCancelarListaBaias"}
            };

            return list;
        }

        public class Menu
        { 
            public string Nome { get; set; }
            public int Icone { get; set; }
            public string Rota { get; set; }
        }
    }
}