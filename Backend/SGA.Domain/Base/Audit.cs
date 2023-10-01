namespace SGAA.Domain.Base
{
    public class Audit
    {
#pragma warning disable CS0414 // The field 'Audit._isDeleted' is assigned but its value is never used
        private bool _isDeleted;
#pragma warning restore CS0414 // The field 'Audit._isDeleted' is assigned but its value is never used

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }

        public void Delete()
        {
            _isDeleted = true;
        }
    }
}
