﻿using Android.App;
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
    public class Produtos
    {
        public string NumeroDocumento { get; set; }
        public string Descricao { get; set; }
        public string Etiqueta { get; set; }
        public int Gramas { get; set; }
        public int QuantidadeEmbalagem { get; set; }
        public string Localizacao { get; set; }
        public string Codigo { get; set; }
        public string Referencia { get; set; }
        public string Lote { get; set; }
        public string Validade { get; set; }
    }
}