namespace SGAA.Documents.Contracts
{
    using SGAA.Documents.DocumentModels;

    public interface IContratoDocumentHandler
    {
        string GetDocumentBody(ContratoDocumentModel documentModel);
    }
}
