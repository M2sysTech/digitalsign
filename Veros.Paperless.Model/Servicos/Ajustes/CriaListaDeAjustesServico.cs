namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using Entidades;
    using Framework;

    public class CriaListaDeAjustesServico : ICriaListaDeAjustesServico
    {
        public IEnumerable<AjusteDeDocumento> Executar(string acoes)
        {
            var ajustes = new List<AjusteDeDocumento>();

            if (string.IsNullOrEmpty(acoes))
            {
                return ajustes;
            }

            foreach (var linha in acoes.Split('|'))
            {
                if (string.IsNullOrEmpty(linha))
                {
                    continue;
                }

                var colunas = linha.Split(';');

                var ajuste = new AjusteDeDocumento
                {
                    Acao = AcaoAjusteDeDocumento.FromString(colunas[0]),
                    Pagina = colunas[1].ToInt(),
                    Documento = new Documento { Id = colunas[2].ToInt() }
                };

                if (ajuste.Acao == AcaoAjusteDeDocumento.Reclassificar)
                {
                    ajuste.TipoDocumentoNovo = new TipoDocumento { Id = colunas[3].ToInt() };
                }

                ajustes.Add(ajuste);
            }

            return ajustes;
        }
    }
}
