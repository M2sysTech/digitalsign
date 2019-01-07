namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System.Collections.Generic;
    using Entidades;
    using Image.Reconhecimento;
    using Repositorios;

    public class ObtemValoresReconhecidosPaginaServico : IObtemValoresReconhecidosPaginaServico
    {
        private readonly IMapeamentoCampoRepositorio mapeamentoCampos;

        public ObtemValoresReconhecidosPaginaServico(IMapeamentoCampoRepositorio mapeamentoCampos)
        {
            this.mapeamentoCampos = mapeamentoCampos;
        }

        public List<ValorReconhecido> Obter(Pagina pagina, ResultadoReconhecimento resultadoReconhecimento)
        {
            //// TODO: fortalecer teste. está fraco
            var mapeamentos = this.mapeamentoCampos.ObterTodosExcetoPac();
            var valoresReconhecidos = new List<ValorReconhecido>();

            foreach (var campoReconhecidoEngine in resultadoReconhecimento.Campos)
            {
                var valorReconhecido = new ValorReconhecido();
                
                if (campoReconhecidoEngine.Value.EhReconhecivel)
                {
                    valorReconhecido.Value = campoReconhecidoEngine.Value.Texto;
                }

                valorReconhecido.SetarCampo(mapeamentos, campoReconhecidoEngine.Value);
                valorReconhecido.SetarCampoReconhecidoDoEngine(campoReconhecidoEngine.Value);
                valorReconhecido.Pagina = pagina;

                valoresReconhecidos.Add(valorReconhecido);
            }

            return valoresReconhecidos;
        }
    }
}
