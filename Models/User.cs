using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Design;

namespace NewMission.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; } 
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        

        public ICollection<Project> projects { get; set; }= new List<Project>();
        
        public User() { }
        public User (string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }


    }
}
