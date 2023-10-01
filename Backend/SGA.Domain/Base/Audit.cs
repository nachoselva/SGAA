namespace SGAA.Domain.Base
{
    using SGAA.Domain.Auth;

    public class Audit
    {
#pragma warning disable CS0414 // The field 'Audit._isDeleted' is assigned but its value is never used
        private bool _isDeleted;
#pragma warning restore CS0414 // The field 'Audit._isDeleted' is assigned but its value is never used

        public DateTimeOffset CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }

        public void Delete(Usuario usuario, DateTimeOffset deletedOn)
        {
            _isDeleted = true;
        }
    }
}
