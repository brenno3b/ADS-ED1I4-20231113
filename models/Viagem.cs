using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED1I4_20231113.models
{
    internal class Viagem
    {
        public int Id { get; }
        public Garagem Origem { get; }
        public Garagem Destino { get; }
        public Van Van { get; }

        public Viagem(int id, Garagem origem, Garagem destino, Van van)
        {
            Id = id;   
            Origem = origem;
            Destino = destino;
            Van = van;
        }
    }
}
