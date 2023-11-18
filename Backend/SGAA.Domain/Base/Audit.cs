namespace SGAA.Domain.Base
{
    public class Audit
    {
        public Audit()
        {

        }

        public bool IsDeleted { get; }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
