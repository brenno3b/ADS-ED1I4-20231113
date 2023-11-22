using ADS_ED1I4_20231113.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED1I4_20231113.controllers
{
    internal class TransporteController
    {
        public List<Garagem> Garagens { get; }

        public List<Viagem> Viagens { get; }

        public List<Van> Vans { get; }

        private bool isJornadaIniciada;
        private int VanIdCount = 8;
        private int GaragemIdCount = 2;
        private int ViagemIdCount = 0;

        public TransporteController()
        {
            List<Garagem> garagens = new()
            {
                new Garagem(0, "Congonhas"),
                new Garagem(1, "Guarulhos")
            };

            Garagens = garagens;

            List<Van> vans = new();

            for (int i = 0; i < 8; i++)
            {
                vans.Add(new Van(i, 15));
            }

            Vans = vans;

            Viagens = new List<Viagem>();
            isJornadaIniciada = false;
        }

        public bool CadastrarVan(int lotacao)
        {
            if (isJornadaIniciada) return false;

            Van van = new(VanIdCount++, lotacao);

            Vans.Add(van);

            return true;
        }

        public bool CadastrarGaragem(string nome)
        {
            if (isJornadaIniciada) return false;

            Garagem garagem = new(GaragemIdCount++, nome);

            Garagens.Add(garagem);

            return true;
        }

        public bool IniciarJornada()
        {
            if (isJornadaIniciada) return false;

            Viagens.Clear();

            ReorganizarVans();

            isJornadaIniciada = true;

            return true;
        }

        public bool EncerrarJornada()
        {
            if (!isJornadaIniciada) return false;

            isJornadaIniciada = false;

            return true;
        }

        public bool LiberarViagem(Garagem origem, Garagem destino)
        {
            if (!isJornadaIniciada) return false;

            if (origem.Vans.Count == 0) return false;

            Van van = origem.Vans.Pop();

            destino.AddVan(van);

            Viagem viagem = new(ViagemIdCount++, origem, destino, van);

            Viagens.Add(viagem);

            return true;
        }

        public List<Viagem> GetViagensByOrigemDestino(Garagem origem, Garagem destino)
        {
            return Viagens.Where((viagem) => viagem.Origem.Id.Equals(origem.Id) && viagem.Destino.Id.Equals(destino.Id)).ToList();
        }

        private void ReorganizarVans()
        {
            foreach (var Garagem in Garagens)
            {
                Garagem.ClearVans();
            }

            int currentGaragemId = 0;

            for (int i = 0; i < Vans.Count; i++)
            {
                Van van = Vans[i];

                Garagem garagem = Garagens[currentGaragemId];

                garagem.AddVan(van);

                currentGaragemId++;

                if (currentGaragemId == Garagens.Count)
                {
                    currentGaragemId = 0;
                }
            }
        }
    }
}
