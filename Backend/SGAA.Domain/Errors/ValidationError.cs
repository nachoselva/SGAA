namespace SGAA.Domain.Errors
{
    public class ValidationError
    {
        public ValidationError(string fieldName, string error)
        {
            FieldName = fieldName;
            ValidationMessage = error;
        }

        public string FieldName { get; set; }
        public string ValidationMessage { get; set; }
    }
}
