namespace SGAA.Domain.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BadRequestException : Exception
    {
        private readonly Dictionary<string, IList<string>> _validationErrors;

        public BadRequestException()
        {
            _validationErrors = new Dictionary<string, IList<string>>();
        }

        public BadRequestException(Dictionary<string, IList<string>> keyValuePairs)
        {
            _validationErrors = keyValuePairs;
        }
        public BadRequestException(string fieldName, string validationMessage) : this()
        {
            AddMessage(fieldName, validationMessage);
        }
        public BadRequestException(ValidationError validationError) : this()
        {
            AddMessage(validationError.FieldName, validationError.ValidationMessage);
        }
        public BadRequestException(IReadOnlyCollection<ValidationError> validationErrors) : this()
        {
            foreach (var validationError in validationErrors)
            {
                AddMessage(validationError.FieldName, validationError.ValidationMessage);
            }
        }

        public BadRequestException AddMessage(string fieldName, string validationMessage)
        {
            if (!_validationErrors.ContainsKey(fieldName))
            {
                _validationErrors.Add(fieldName, new List<string>());
            }
            IList<string> errorList = _validationErrors[fieldName];
            errorList.Add(validationMessage);
            return this;
        }
        public override string Message => string.Join(Environment.NewLine, _validationErrors.Select(err => $"{err.Key}: {string.Join("; ", err.Value)}"));
        public IDictionary<string, IList<string>> GetValidationErrors()
        {
            return _validationErrors;
        }
    }
}
