namespace SGAA.Documents
{
    using HandlebarsDotNet;
    using NReco.PdfGenerator;
    using SGAA.Documents.Contracts;
    using SGAA.Documents.DocumentModels;
    using System.IO;
    using System.Reflection;

    public class ContratoDocumentHandler : IContratoDocumentHandler
    {
        private static readonly string path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!}\\DocumentTemplates\\";

        private readonly IHandlebars _handlebarEngine = Handlebars.Create();
        private readonly HtmlToPdfConverter _pdfConverter = new();

        public string GetDocumentBody(ContratoDocumentModel documentModel)
        {
            string templateContent = File.ReadAllText($"{path}ContratoDocument.html");
            var template = _handlebarEngine.Compile(templateContent);
            var documentHtml = template(documentModel);
            return Convert.ToBase64String(_pdfConverter.GeneratePdf(documentHtml));
        }
    }
}
