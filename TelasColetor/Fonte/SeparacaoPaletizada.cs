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
using static Android.Graphics.ColorSpace;

namespace TelasColetor.Fonte
{
    [Activity(Label = "SeparacaoPaletizada")]
    public class SeparacaoPaletizada : Activity
    {
        RecyclerView recyclerView;
        Button separacao_paletizada_voltar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // vincula o layout com este codigo fonte
            SetContentView(Resource.Layout.Separacao_Paletizada);

            // pega o recycleView do front
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);
            separacao_paletizada_voltar = FindViewById<Button>(Resource.Id.separacao_paletizada_voltar);

            // define quantas colunas o recycleView vai ter
            var gridLayoutManager = new GridLayoutManager(this, 2);

            // pega os dados do back e adapta eles para colocar no recycleView
            var adapter = new RecyclerAdapter(GetMenusSeparacaoPaletizadaUsuario());
            //                                      +---- Retorna um objeto contendo um atributo lista com vários outros objetos

            // Registre o manipulador de cliques de item (abaixo) com o adaptador:
            adapter.ItemClick += Adapter_ItemClick;
            separacao_paletizada_voltar.Click += Separacao_paletizada_voltar_Click;

            recyclerView.SetLayoutManager(gridLayoutManager);
            recyclerView.HasFixedSize = true;

            // Conecte o adaptador no RecyclerView:
            recyclerView.SetAdapter(adapter);
        }

        private void Separacao_paletizada_voltar_Click(object sender, EventArgs e)
        {       
            Finish();
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            int posicao = e;

            RecyclerAdapter recyclerAdapter = recyclerView.GetAdapter() as RecyclerAdapter;
            string rota = recyclerAdapter.items.MenuSeparacaoPaletizadaUsuario[posicao].Form.Trim();

            Type type = System.Type.GetType(rota);

            Intent intent = new Intent(this, type);
            
            StartActivity(intent);
        }
               
        // ADAPTER
        // Adaptador para conectar o conjunto de dados(vindos do banco, retorno da API) ao RecyclerView:
        public class RecyclerAdapter : RecyclerView.Adapter
        {
            // referencia para o objeto contendo a lista de dados
            public MenusSeparacaoPaletizada items;

            // Manipulador de eventos para cliques de itens:
            public event EventHandler<int> ItemClick;

            // recebe no construtor, o conjunto de dados vindo do retorno da API
            public RecyclerAdapter(MenusSeparacaoPaletizada data)
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
                var item = items.MenuSeparacaoPaletizadaUsuario[position];

                // define a descrição e o icone
                var holder = viewHolder as RecyclerHolder;
                holder.Descricao.Text = item.Descricao;

                // holder.icone.SetImageResource(ImagemMenu.ObtemImagemMenu(item.icone));
                holder.Icone.SetImageResource(item.Icone);
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
                    return items.MenuSeparacaoPaletizadaUsuario.Count;
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

        // simula dados vindo do banco de dados
        public MenusSeparacaoPaletizada GetMenusSeparacaoPaletizadaUsuario()
        { 
            MenusSeparacaoPaletizada menus = new MenusSeparacaoPaletizada();
            menus.MenuSeparacaoPaletizadaUsuario = new List<MenuSeparacaoPaletizadaUsuario>();
                                         
            menus.MenuSeparacaoPaletizadaUsuario.Add(new MenuSeparacaoPaletizadaUsuario
            {
                Descricao = "Separar Palete",
                Icone = Resource.Drawable.icons8_fork_lift_96,
                Form = "TelasColetor.Fonte.SepararPalete"
            });
            menus.MenuSeparacaoPaletizadaUsuario.Add(new MenuSeparacaoPaletizadaUsuario
            {
                Descricao = "Descer Palete",
                Icone = Resource.Drawable.icons8_pallet_96_7,
                Form = ""
            });
            menus.MenuSeparacaoPaletizadaUsuario.Add(new MenuSeparacaoPaletizadaUsuario
            {
                Descricao = "Transportar Palete",
                Icone = Resource.Drawable.icons8_use_forklift_96,
                Form = ""
            });
            menus.MenuSeparacaoPaletizadaUsuario.Add(new MenuSeparacaoPaletizadaUsuario
            {
                Descricao = "Transportar e Expedir Palete",
                Icone = Resource.Drawable.icons8_end_96,
                Form = ""
            });

            return menus;
        }

        public class MenusSeparacaoPaletizada
        {  
            public List<MenuSeparacaoPaletizadaUsuario> MenuSeparacaoPaletizadaUsuario { get; set; }
        }
         
        public class MenuSeparacaoPaletizadaUsuario 
        { 
            public string Descricao { get; set; }
            public int Icone { get; set; }
            public string Form { get; set; }
        }
    }
} 