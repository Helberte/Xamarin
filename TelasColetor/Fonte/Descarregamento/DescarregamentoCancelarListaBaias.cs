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
    [Activity(Label = "DescarregamentoCancelarListaBaias")]
    public class DescarregamentoCancelarListaBaias : Activity
    {

        ListView descarregamento_cancelar_lista_de_baias_listview_baias;

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

            Random random = new Random();
            List<ModelListaBaias> baias = new List<ModelListaBaias>();
            for (int i = 0; i < 20; i++)
            {
                baias.Add(new ModelListaBaias() { NumeroBaia = Convert.ToInt32(random.Next(1, 50).ToString().PadLeft(2, '0')),
                                                  ImagemBaia = Resource.Drawable.icons8_down_96 } );
            }

            baias = baias.OrderBy(a => a.NumeroBaia).ToList();
            baias = baias.Distinct().ToList();

            descarregamento_cancelar_lista_de_baias_listview_baias.Adapter = new AdapterBaiaCustomizado(baias);
            descarregamento_cancelar_lista_de_baias_listview_baias.Click += Descarregamento_cancelar_lista_de_baias_listview_baias_Click;

        }

        private void Descarregamento_cancelar_lista_de_baias_listview_baias_Click(object sender, EventArgs e)
        {

        }
    }
}