namespace SGAA.Domain.Errors
{
    using System;

    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message) : base(message)
        {
        }
    }
}
