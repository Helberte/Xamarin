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
    [Activity(Label = "DescarregamentoFinalizarLeiaBaia")]
    public class DescarregamentoFinalizarLeiaBaia : MainClassApplication
    {
        LinearLayout descarregamento_finaliza_leia_baia_informacoes_carga;
        EditText     descarregamento_finaliza_leia_baia_leia_uma_baia;
        Button       descarregamento_finaliza_leia_baia_botao_confirmar;
        Button       descarregamento_finaliza_leia_baia_botao_sair;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DescarregamentoFinalizarLeiaBaia);

            descarregamento_finaliza_leia_baia_informacoes_carga = FindViewById<LinearLayout>(Resource.Id.descarregamento_finaliza_leia_baia_informacoes_carga);
            descarregamento_finaliza_leia_baia_leia_uma_baia     = FindViewById<EditText>(Resource.Id.descarregamento_finaliza_leia_baia_leia_uma_baia);
            descarregamento_finaliza_leia_baia_botao_confirmar   = FindViewById<Button>(Resource.Id.descarregamento_finaliza_leia_baia_botao_confirmar);
            descarregamento_finaliza_leia_baia_botao_sair        = FindViewById<Button>(Resource.Id.descarregamento_finaliza_leia_baia_botao_sair);

            descarregamento_finaliza_leia_baia_informacoes_carga.Visibility  = ViewStates.Gone;
            descarregamento_finaliza_leia_baia_botao_confirmar.Click        += Descarregamento_finaliza_leia_baia_botao_confirmar_Click;
            descarregamento_finaliza_leia_baia_leia_uma_baia.TextChanged    += Descarregamento_finaliza_leia_baia_leia_uma_baia_TextChanged;

            descarregamento_finaliza_leia_baia_botao_sair.Click += Descarregamento_finaliza_leia_baia_botao_sair_Click; ;
        }

        private void Descarregamento_finaliza_leia_baia_botao_sair_Click(object sender, EventArgs e)
        {      
            MensagemSairDoAplicativo();
        }

        private void Descarregamento_finaliza_leia_baia_leia_uma_baia_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if ((sender as EditText).Text.Replace(" ", "").Length < 4)
            {
                descarregamento_finaliza_leia_baia_informacoes_carga.Visibility = ViewStates.Gone; 
                return;
            }            
            if ((sender as EditText).Text.Replace(" ", "") == "1234")
            {
                descarregamento_finaliza_leia_baia_informacoes_carga.Visibility = ViewStates.Visible;
                return;
            }

            descarregamento_finaliza_leia_baia_informacoes_carga.Visibility = ViewStates.Gone;
            Toast.MakeText(this, "Nenhuma carga encontrada para esta baia.", ToastLength.Long).Show();
        }

        private void Descarregamento_finaliza_leia_baia_botao_confirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(descarregamento_finaliza_leia_baia_leia_uma_baia.Text))
            {
                Toast.MakeText(this, "Por favor, leia uma baia.", ToastLength.Long).Show();
                return;
            }    
            if (descarregamento_finaliza_leia_baia_leia_uma_baia.Text.Replace(" ", "").Length >= 4 
                && 
                descarregamento_finaliza_leia_baia_leia_uma_baia.Text.Replace(" ", "") != "1234")
            {
                Toast.MakeText(this, "Nenhuma carga encontrada para esta baia.", ToastLength.Long).Show();
                descarregamento_finaliza_leia_baia_informacoes_carga.Visibility = ViewStates.Gone;
                return;
            }

            FragmentTransaction fragment = FragmentManager.BeginTransaction();
            DialogFragmentClass dialog = new DialogFragmentClass();

            dialog.Show(fragment, "Dialog Fragment");
        }
    }
}