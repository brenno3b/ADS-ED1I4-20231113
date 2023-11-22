using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED1I4_20231113.models
{
    internal class Van
    {
        public int Id { get; }
        public int Lotacao { get; }

        public Van(int id, int lotacao)
        {
            Id = id;
            Lotacao = lotacao;
        }
    }
}
