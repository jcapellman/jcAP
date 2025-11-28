namespace jcAP.API.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public DateTime Created { get; set; }
        
        public DateTime Modified { get; set; }
    }
}