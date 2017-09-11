using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace XamarinBot
{
    [Serializable]
    public class plan
    {
        
        [Optional]
        public string Descripcion { get; set; }

        //[Prompt("Near which Airport")]
        [Optional]
        public string Precio { get; set; }

        [Optional]
        public string Duracion { get; set; }

        public string Image { get; set; }

    }
}