using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED1I4_20231113.models
{
    internal class Garagem
    {
        public int Id { get; }
        public string Nome { get; }
        public Stack<Van> Vans { get; }

        public int PotencialDeTransporte
        {
            get { return Vans.Aggregate(0, (acc, value) => acc + value.Lotacao); }
        }

        public Garagem(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Vans = new Stack<Van>();
        }

        public void AddVan(Van van)
        {
            Vans.Push(van);
        }

        public void ClearVans()
        {
            Vans.Clear();
        }
    }
}
