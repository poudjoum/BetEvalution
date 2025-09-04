using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Models
{
    public class RawMarket
    {
        public int Id { get; set; }
        public string E { get; set; } = default!; // Libellé anglais
        public string F { get; set; } = default!; // Libellé français
        public string C { get; set; } = default!; // Code marché
        public int N { get; set; }               // Nombre d’options
        public int O { get; set; }               // Ordre
        public int G { get; set; }               // Groupe
    }
}
