using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entity
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
