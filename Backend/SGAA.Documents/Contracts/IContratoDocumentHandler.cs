namespace SGAA.Documents.Contracts
{
    using SGAA.Documents.DocumentModels;

    public interface IContratoDocumentHandler
    {
        byte[] GetDocumentBody(ContratoDocumentModel documentModel);
    }
}
