namespace Veros.Paperless.Model.Servicos.Separacao
{
    public interface IAtualizaStatusSeparacaoServico
    {
        void RegistrarLogSubfaseSeparacao(int loteId, int processoId, string subfase);
    }
}
