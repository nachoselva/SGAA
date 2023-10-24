namespace SGAA.Api.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class TransactionalAttribute : Attribute
    {
       
    }
}
