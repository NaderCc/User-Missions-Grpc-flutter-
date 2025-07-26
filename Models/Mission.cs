using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace NewMission.Models
{
    public class Mission
    {
        [Key]
        public int Id {  get; set; }
        public string Title {  get; set; }
        [Required]
        public string Description {  get; set; }
        public DateTime DueDate {  get; set; }
        //Status(Pending / InProgress / Done)
        public string Status {  get; set; }
        //Priority(Low / Medium / High)
        public enum Priority { Low,Medium , High }
        //ProjectId(FK → Projects)
        [ForeignKey("project")]
        public int ProjectId { get; set; }
        public Project project { get; set; }

        
        public DateTime CreatedAt {  get; set; }
    }
}
