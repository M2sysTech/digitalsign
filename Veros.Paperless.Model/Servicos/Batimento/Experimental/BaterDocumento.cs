namespace Veros.Paperless.Model.Servicos.Batimento.Experimental
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Framework;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorDocumento;

    public class BaterDocumento : IBaterDocumento
    {
        private readonly BatimentoCampoFactory batimentoCampoFactory;
        private readonly IValidarResultadoBatimento validarResultadoBatimento;

        public BaterDocumento(
            BatimentoCampoFactory batimentoCampoFactory, 
            IValidarResultadoBatimento validarResultadoBatimento)
        {
            this.batimentoCampoFactory = batimentoCampoFactory;
            this.validarResultadoBatimento = validarResultadoBatimento;
        }

        public static BatimentoDo BatimentoCom
        {
            get;
            set;
        }

        public static void Considerar(BatimentoDo batimentoDo)
        {
            BatimentoCom = batimentoDo;
        }

        public ResultadoBatimentoDocumento Iniciar(
            Documento documento, 
            ImagemReconhecida imagemReconhecida)
        {
            IBatimentoDocumento batimentoDocumento = IoC
                .Current
                .Resolve<BatimentoDocumento<QualquerDocumentoBatimento>>();
            
            //// batimento por campos (RECVALUE)
            var resultadoBatimento = batimentoDocumento.Entre(documento, imagemReconhecida);
            
            var camposNaoBatidos = resultadoBatimento
                .Campos
                .Where(x => x.Batido == false)
                .ToList();

            //// batimento por fulltext (PALAVRARECONHECIDA ou REDIS)
            resultadoBatimento = this.RealizarBatimentoPorCampoNaoBatido(
                imagemReconhecida, 
                resultadoBatimento, 
                camposNaoBatidos);

            var resultadoBatimentoValidade = this.validarResultadoBatimento
                .ValidarNumeroDocumentoIdentificacao(resultadoBatimento, documento);

            return resultadoBatimentoValidade;
        }

        private ResultadoBatimentoDocumento RealizarBatimentoPorCampoNaoBatido(
            ImagemReconhecida imagemReconhecida, 
            ResultadoBatimentoDocumento resultadoBatimento, 
            List<CampoBatido> camposNaoBatidos)
        {
            foreach (var campoNaoBatido in camposNaoBatidos)
            {
                var batimentoCampo = this.batimentoCampoFactory.Criar(campoNaoBatido.Indexacao);

                var resultadoDoCampo = batimentoCampo.Entre(campoNaoBatido.Indexacao, imagemReconhecida.Palavras);

                resultadoBatimento.AdicionarOuEditar(resultadoDoCampo);
            }

            return resultadoBatimento;
        }
    }
}