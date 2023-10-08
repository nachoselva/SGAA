namespace SGAA.Domain.Base
{
    public class Audit
    {
        public Audit()
        {
            _isDeleted = false;
        }

#pragma warning disable CS0414 // The field 'Audit._isDeleted' is assigned but its value is never used
        private bool _isDeleted;
#pragma warning restore CS0414 // The field 'Audit._isDeleted' is assigned but its value is never used

        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
