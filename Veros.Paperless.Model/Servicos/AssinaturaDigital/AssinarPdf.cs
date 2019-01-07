namespace Veros.Paperless.Model.Servicos.AssinaturaDigital
{
    using System;
    using System.IO;
    using Framework;

    public class AssinarPdf : IAssinarPdf
    {
        private readonly ITimeStampSigner timeStampSigner;
        
        public AssinarPdf(ITimeStampSigner timeStampSigner)
        {
            this.timeStampSigner = timeStampSigner;
        }

        public string Execute(string pdfMontado)
        {
            var singInfo = new SingInfo();

            var diretorio = Path.GetDirectoryName(pdfMontado);
            var fileName = Path.GetFileNameWithoutExtension(pdfMontado);

            singInfo.SourcePdf = pdfMontado;
            singInfo.TargetPdf = Path.Combine(diretorio, fileName + "-ass.pdf");

            singInfo.Author = Contexto.AssinaturaDigitalAuthor;
            singInfo.Creator = Contexto.AssinaturaDigitalCreator;
            singInfo.Keywords = Contexto.AssinaturaDigitalKeywords;
            singInfo.Producer = Contexto.AssinaturaDigitalProducer;
            singInfo.SignatureContact = Contexto.AssinaturaDigitalContact;
            singInfo.SignatureLocation = Contexto.AssinaturaDigitalLocation;
            singInfo.SignatureReason = Contexto.AssinaturaDigitalReason;
            singInfo.Subject = Contexto.AssinaturaDigitalSubject;
            singInfo.Title = Contexto.AssinaturaDigitalTitle;

            try
            {
                var pdfAssinado = this.timeStampSigner.Execute(singInfo);

                return pdfAssinado;
            }
            catch (RepositorioCertificadosException ex)
            {
                throw;
            }
            catch (Exception exception)
            {
                Log.Application.Error(exception.GetAllExceptionsMessages());
                throw;
            }
        }
    }
}