﻿namespace Veros.Paperless.Model.Servicos.Separacao
{
    using ViewModel;

    public interface IReclassificaDocumentoNaSeparacaoServico
    {
        void Executar(AcaoDeSeparacao acao, LoteParaSeparacaoViewModel loteParaSeparacao);
    }
}
