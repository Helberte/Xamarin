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

namespace TelasColetor.Fonte.SeparacaoFracionada
{
    internal class Dialog : DialogFragment
    {
        Button separacao_fracionada_menu_mais_opcoes_botao_fechar;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.SeparacaoFracionadaMenuMaisOpcoes, container, false);

            separacao_fracionada_menu_mais_opcoes_botao_fechar        = view.FindViewById<Button>(Resource.Id.separacao_fracionada_menu_mais_opcoes_botao_fechar);

            separacao_fracionada_menu_mais_opcoes_botao_fechar.Click += Separacao_fracionada_menu_mais_opcoes_botao_fechar_Click;

            return view;
        }

        private void Separacao_fracionada_menu_mais_opcoes_botao_fechar_Click(object sender, EventArgs e)
        {
            this.Dismiss();
        }
    }
} 