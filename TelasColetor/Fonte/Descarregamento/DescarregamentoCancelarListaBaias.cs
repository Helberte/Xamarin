using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelasColetor.Fonte.Descarregamento
{
    [Activity(Label = "DescarregamentoCancelarListaBaias")]
    public class DescarregamentoCancelarListaBaias : Activity
    {
        ListView descarregamento_cancelar_lista_de_baias_listview_baias;
        Button   descarregamento_cancelar_lista_de_baias_botao_voltar;

        /// <summary>
        /// Ponto de entrada da activity, aqui, é criado um adapter personalizado para preencher a lista, 
        /// aqui é preenchida a lista com um layout do xamarin
        /// </summary>
        /// <param name="savedInstanceState"></param>
        /// <remarks>Helberte Costa, 07/06/2023</remarks>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DescarregamentoCancelarListaBaias);

            descarregamento_cancelar_lista_de_baias_listview_baias = FindViewById<ListView>(Resource.Id.descarregamento_cancelar_lista_de_baias_listview_baias);
            descarregamento_cancelar_lista_de_baias_botao_voltar   = FindViewById<Button>(Resource.Id.descarregamento_cancelar_lista_de_baias_botao_voltar);

            Random random = new Random();
            List<ModelListaBaias> baias = new List<ModelListaBaias>();
            for (int i = 0; i < 20; i++)
            {
                baias.Add(new ModelListaBaias() { NumeroBaia = Convert.ToInt32(random.Next(1, 50).ToString().PadLeft(2, '0')),
                                                  ImagemBaia = Resource.Drawable.icons8_down_96 } );
            }

            baias = baias.OrderBy(a => a.NumeroBaia).ToList();
            baias = baias.Distinct().ToList();

            descarregamento_cancelar_lista_de_baias_listview_baias.ItemClick += Descarregamento_cancelar_lista_de_baias_listview_baias_ItemClick;
            descarregamento_cancelar_lista_de_baias_botao_voltar.Click += (sender, e) => this.Finish();

            descarregamento_cancelar_lista_de_baias_listview_baias.Adapter = new AdapterBaiaCustomizado(baias);            
        }

        private void Descarregamento_cancelar_lista_de_baias_listview_baias_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int posicao = e.Position;

            AdapterBaiaCustomizado baias = descarregamento_cancelar_lista_de_baias_listview_baias.Adapter as AdapterBaiaCustomizado;
                       
            Intent intent = new Intent(this, typeof(DescarregamentoCancelarConfirmaInformacoes));
            intent.PutExtra("baia", baias.modelListaBaias[posicao].NumeroBaia.ToString().PadLeft(2,'0'));

            StartActivity(intent);
        }
    }
}