using SistemaArmazem.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SistemaArmazem.Models.Negocio
{
    public static class Negocio
    {
        public static bool armazemTemEspacoPraIsso(int qtd, int IDdoArmazem)
        {
            try
            {
                ArmazemRepository armazemRepository = new ArmazemRepository();
                TamanhoArmazemRepository tamanhoArmazemRepository = new TamanhoArmazemRepository();

                int tamanhoTotalOcupado = 0;
                foreach (var partesDoArmazem in armazemRepository.Listar().Where(x=>x.tamanhoArmazemId == IDdoArmazem))
                {
                    tamanhoTotalOcupado += partesDoArmazem.usadoArmazem;
                }
                if (tamanhoTotalOcupado + qtd <= tamanhoArmazemRepository.Buscar(IDdoArmazem).tamanhoArmazem)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public static int espacoLivre()
        {
            try
            {
                ArmazemRepository armazemRepository = new ArmazemRepository();
                TamanhoArmazemRepository tamanhoArmazemRepository = new TamanhoArmazemRepository();

                int tamanhoTotalOcupado = 0;
                int tamanhoTotalDoArmazem = tamanhoArmazemRepository.Buscar(0).tamanhoArmazem;
                foreach (var partesDoArmazem in armazemRepository.Listar().Where(x=>x.tamanhoArmazemId == 0))
                {
                    tamanhoTotalOcupado += partesDoArmazem.usadoArmazem;
                }
                return tamanhoTotalDoArmazem - tamanhoTotalOcupado;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public static int espacoLivre(int IDdoArmazem)
        {
            try
            {
                ArmazemRepository armazemRepository = new ArmazemRepository();
                TamanhoArmazemRepository tamanhoArmazemRepository = new TamanhoArmazemRepository();

                int tamanhoTotalOcupado = 0;
                int tamanhoTotalDoArmazem = tamanhoArmazemRepository.Buscar(IDdoArmazem).tamanhoArmazem;
                foreach (var partesDoArmazem in armazemRepository.Listar().Where(x => x.tamanhoArmazemId == IDdoArmazem))
                {
                    tamanhoTotalOcupado += partesDoArmazem.usadoArmazem;
                }
                return tamanhoTotalDoArmazem - tamanhoTotalOcupado;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}