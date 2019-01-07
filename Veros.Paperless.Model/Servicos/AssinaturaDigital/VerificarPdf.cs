namespace Veros.Paperless.Model.Servicos.AssinaturaDigital
{
    using System;
    using Framework;
    using iText.Kernel.Pdf;
    using iText.Forms;

    public class VerificarPdf : IVerificarPdf
    {
        public bool EstaAssinado(string pdfMontado)
        {
            try
            {
                PdfDocument pdfDoc = new PdfDocument(new PdfReader(pdfMontado));

                var form = PdfAcroForm.GetAcroForm(pdfDoc, false);
                var names = form.GetFormFields();

                if (names.Count == 0)
                {
                    return false;
                }
               
                ////acroFields.VerifySignature(name);

                ////name = names[1];
                ////acroFields.VerifySignature(name);

            }
            catch (Exception exception)
            {
                Log.Application.Error(exception);
                return false;
            }

            return true;
        }
    }
}