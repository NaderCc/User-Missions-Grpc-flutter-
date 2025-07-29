using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace NewMission.Models
{
    public class Mission
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public string Title {  get; set; }
        [Required]
        public string Description {  get; set; }
        public string DueDate {  get; set; }
        //Status(Pending / InProgress / Done)
        public string Status {  get; set; }
        //Priority(Low / Medium / High)
        public string Priority { get; set; }  
        //ProjectId(FK → Projects)
        [ForeignKey("project")]
        public int ProjectId { get; set; }
        public Project project { get; set; }

        
        public DateTime CreatedAt = DateTime.Now;
    }
}
