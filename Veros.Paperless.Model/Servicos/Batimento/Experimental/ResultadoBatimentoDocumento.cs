namespace Veros.Paperless.Model.Servicos.Batimento.Experimental
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework;
    using Veros.Paperless.Model.Entidades;

    public class ResultadoBatimentoDocumento
    {
        public ResultadoBatimentoDocumento()
        {
            this.Campos = new List<CampoBatido>();
        }

        public IList<CampoBatido> Campos
        {
            get;
            set;
        }
        
        public void AdicionarOuEditar(CampoBatido campoBatido)
        {
            if (this.JaExisteResultadoBatido(campoBatido))
            {
                return;
            }

            if (this.JaExisteResultadoNaoBatido(campoBatido))
            {
                this.AtribuirNovoValor(campoBatido);
                return;
            }
            
            this.Campos.Add(campoBatido);
        }

        public bool CampoEstaBatido(Indexacao indexacao)
        {
            CampoBatido campoBatido = null;
            foreach (CampoBatido campo in this.Campos)
            {
                if (campo.Indexacao == indexacao)
                {
                    campoBatido = campo;
                    break;
                }
            }

            return campoBatido != null && campoBatido.Batido;
        }

        private bool JaExisteResultadoNaoBatido(CampoBatido campoBatido)
        {
            return this.Campos.Any(
                index => index.Indexacao == campoBatido.Indexacao &&
                index.Batido == false);
        }

        private bool JaExisteResultadoBatido(CampoBatido campoBatido)
        {
            return this.Campos.Any(
                index => index.Indexacao == campoBatido.Indexacao &&
                index.Batido);
        }

        private void AtribuirNovoValor(CampoBatido campoBatido)
        {
            var camp = this.Campos.FirstOrDefault(campo => campo.Indexacao == campoBatido.Indexacao);

            if (camp == null)
            {
                return;
            }

            Log.Application.DebugFormat(
                "AtribuirNovoValor::Documento {0} Campo {1} resultado {2}",
                camp.Indexacao.Documento.Id,
                camp.Indexacao.Campo.Description,
                campoBatido.Batido);

            camp.Batido = campoBatido.Batido;
            camp.TipoBatimento = campoBatido.TipoBatimento;
        }
    }
}