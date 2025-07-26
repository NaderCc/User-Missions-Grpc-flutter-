using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewMission.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }        //(PK)
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description {  get; set; }
                       //(FK → Users)
        public DateTime CreatedAt {  get; set; } = DateTime.Now;
        [ForeignKey("user")]
        public int UserId { get; set; }
        public User user{get; set; }

        public ICollection<Mission> Tasks { get; set; }
        
    }
}
