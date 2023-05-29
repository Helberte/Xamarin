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

namespace TelasColetor.Fonte
{
    [Activity(Label = "SepararPalete")]
    public class SepararPalete : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Faz a ligação deste fonte com a tela de design
            SetContentView(Resource.Layout.Separar_palete);
        }
    }
}
