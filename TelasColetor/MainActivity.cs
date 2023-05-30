using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
//using Java.Util;
using System;
using System.Collections.Generic;
using TelasColetor.Fonte;
using static Android.Content.ClipData;
using static TelasColetor.Fonte.SeparacaoPaletizada;

namespace TelasColetor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]

    public class MainActivity : AppCompatActivity
    {
        RecyclerView recyclerView;
        Mensagens mensagens = new Mensagens();
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // começar criar os componentes necessários para o recyclerView

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView_menu_principal);

            // definir o layout do grid que será mostrado, neste caso, duas colunas
            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 2);

            // criar adapter para adaptar os dados ao recyclerView
            RecyclerAdapter adapter = new RecyclerAdapter(GetMenusUsuario());

            // gera o click
            adapter.ItemClick += Adapter_ItemClick;

            // seta o layout no recyclerView
            recyclerView.SetLayoutManager(gridLayoutManager);
            recyclerView.HasFixedSize = true;

            // conecta o adapter ao recyclerView
            recyclerView.SetAdapter(adapter);
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            try
            {
                // O foco aqui é obter o numero da posicao do item clicado
                int posicaoItem = e;

                // obtem o objeto recyclerView que contém o objeto items com os objetos dos menus, precisamos acessar através da posição
                // qual menu a pessoa clicou

                RecyclerAdapter recyclerAdapter = recyclerView.GetAdapter() as RecyclerAdapter;
                string rota = recyclerAdapter.items.MenusUsuario[posicaoItem].Form.Trim();
                //                                                  +------ obtem qual foi o menu que a pessoa clicou, nele tem a propriedade form com o nome da active a ser chamada

                Type type = System.Type.GetType(rota);

                Intent intent = new Intent(this, type);
                intent.PutExtra("parametros", "teste");

                StartActivity(intent);
            }
            catch (Exception)
            {
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);

                mensagens.MostraMensagem(Android.Resource.Drawable.IcDialogAlert, "Em desenvolvimento.", builder);
            }           
        }

        // chamaria a separação paletizada
        private void Layout_bt_separacao_paletizada_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(SeparacaoPaletizada));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        // ADAPTER

        // Adaptador para conectar o conjunto de dados(vindos do banco, retorno da API) ao RecyclerView:
        public class RecyclerAdapter : RecyclerView.Adapter
        {
            // referencia para o objeto contendo a lista de dados
            public Menus items;

            // Manipulador de eventos para cliques de itens:
            public event EventHandler<int> ItemClick;

            // recebe no construtor, o conjunto de dados vindo do retorno da API
            public RecyclerAdapter(Menus data)
            {
                items = data;
            }

            // Crie uma nova CardView (chamada pelo gerenciador de layout). Aqui, o layout de visualização personalizado já deverá estar criado, o FrameLayout
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                // Inflar o CardView para mostrar o card:

                View itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.Menu_Coletor_Item, parent, false);
                // referencia o layout card que foi criado na pasta Resource -> Layout

                // Crie um ViewHolder para localizar e manter essas referências de exibição e
                // registra OnClick com o viewholder:
                RecyclerHolder vh = new RecyclerHolder(itemView, OnClick);
                return vh;
            }

            // Preencha o conteúdo do card com os objetos passados (chamado pelo gerenciador de layout) :
            public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
            {
                var item = items.MenusUsuario[position];

                // define a descrição e o icone
                var holder = viewHolder as RecyclerHolder;
                holder.Descricao.Text = item.Descricao;
                holder.Icone.SetImageResource(item.Icone);      // coloca o icone

                // holder.icone.SetImageResource(ImagemMenu.ObtemImagemMenu(item.icone));
            }

            // Gera um evento quando ocorre o clique do item:
            public void OnClick(int position)
            {
                ItemClick?.Invoke(this, position);
            }

            // retorna a quantidade de itens na lista
            public override int ItemCount
            {
                get
                {
                    return items.MenusUsuario.Count;
                }
            }
        }

        // VIEW HOLDER

        // Implementa o padrão ViewHolder: cada ViewHolder contém referências
        // para os componentes de interface do usuário (ImageView e TextView) dentro do CardView
        // que é exibido em uma linha do RecyclerView:

        // aqui é onde são criados os atributos que serão mostrados
        public class RecyclerHolder : RecyclerView.ViewHolder
        {
            public TextView Descricao { get; private set; }
            public ImageView Icone { get; private set; }

            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Descricao = (TextView)itemView.FindViewById(Resource.Id.Menu_Coletor_Item_Descricao);
                //                                                          +--- pega estes itens exatamente da nova view de card personalizada que foi criada, FrameLayout
                Icone = (ImageView)itemView.FindViewById(Resource.Id.Menu_Coletor_Item_Icone);
                //                                                          +--- pega estes itens exatamente da nova view de card personalizada que foi criada, FrameLayout


                // cria o evento de click para cada item

                // Detecte cliques do usuário na exibição do item e informe qual item
                // foi clicado (por posição de layout) para o ouvinte:
                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }



        // simula os dados vindos do back-end
        // preenche menu

        public Menus GetMenusUsuario()
        {            
            // SepararPalete            

            Menus menus = new Menus();
            menus.MenusUsuario = new List<Menu>();


            menus.MenusUsuario.Add(
                                    new Menu { 
                                        Descricao = "Separação Paletizada", 
                                        Icone     = TelasColetor.Resource.Drawable.icons8_pallet_96, 
                                        Form      = "TelasColetor.Fonte.SeparacaoPaletizada" 
                                    });
            menus.MenusUsuario.Add(
                                   new Menu
                                   {
                                       Descricao = "Separação Fracionada",
                                       Icone = TelasColetor.Resource.Drawable.icons8_breakable_96_1,
                                       Form = ""
                                   });
            menus.MenusUsuario.Add(
                                   new Menu
                                   {
                                       Descricao = "Separação de Unidades",
                                       Icone = TelasColetor.Resource.Drawable.icons8_empty_box_96_3,
                                       Form = ""
                                   });
            return menus;
        }

        public class Menus
        {
            public List<Menu> MenusUsuario { get; set; }
        }
        public class Menu
        {
            public string Descricao { get; set; }
            public int Icone { get; set; }
            public string Form { get; set; }
        }
    }
}