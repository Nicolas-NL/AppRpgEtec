using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRpgEtec.Models.Enuns;
using AppRpgEtec.Models;

namespace AppRpgEtec.Models
{
    public class Personagem
    {

        public int Id { get; set; }

        public string Nome { get; set; }

        public int PontosVida { get; set; }

        public int Forca { get; set; }

        public int Defesa { get; set; }

        public int Inteligencia { get; set; }

        public ClasseEnum Classe { get; set; }
        public int Derrotas { get; internal set; }
        public int Disputas { get; internal set; }
        public int Vitorias { get; internal set; }
    }
}
