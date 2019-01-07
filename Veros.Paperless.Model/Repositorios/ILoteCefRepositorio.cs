namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface ILoteCefRepositorio : IRepositorio<LoteCef>
    {
        IList<LoteCef> ObterAbertos(ConfiguracaoDeLoteCef configuracao);

        IList<LoteCef> ObterAbertos();

        IList<LoteCef> ObterAguardandoNovaAmostra();

        IList<LoteCef> ObterTodosMenosAbertos();

        void Finalizar(LoteCef loteCef);

        void Finalizar(int loteCefId, int quantidade);

        void RedisponibilizarParaQualicef(int loteCefId);

        void AdicionarLote(LoteCef loteCef);

        void AlterarQuantidade(int loteCefId, int quantidade);

        void AlterarStatus(int loteCefId, LoteCefStatus status);

        void Aprovar(int loteCefId, int usuarioId);

        void Recusar(int loteCefId, int usuarioId);

        void AtualizarGeracaoPdf(int lotecefId, int usuarioId);
        
        IList<LoteCef> ObterReprovados();
    }
}