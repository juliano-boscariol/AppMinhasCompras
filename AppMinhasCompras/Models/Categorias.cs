using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMinhasCompras.Models
{
    public static class Categorias
    {
        public static List<string> Lista_categorias { get; } = new()
        {
            "Açougue",
            "Higiene",
            "Hortifruti",
            "Limpeza",
            "Mercearia"
        };
    }
}
