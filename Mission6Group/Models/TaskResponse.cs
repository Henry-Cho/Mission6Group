using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission6Group.Models
{
    public class TaskResponse
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string TaskDescription { get; set; }
        public DateTime DueDate { get; set; }
        [Range(1, 4)]
        public int Quadrant { get; set; }
        public int Categoryid { get; set; }
        public Category Category { get; set; }
        public bool Completed { get; set; }



    }
}
