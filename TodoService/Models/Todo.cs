using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoService.Models
{
    public class Todo
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public int Priority { get; set; }
        [StringLength(50)]
        public string Responsible { get; set; }
        public DateTime Deadline { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ParentId { get; set; }
        [NotMapped]
        public int RemainingWorkHours {
            get
            {
                var hours = 0;
                for (var i = DateTime.Now; i < this.Deadline; i = i.AddHours(1))
                {
                    if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
                    {
                        if (i.TimeOfDay.Hours >= 9 && i.TimeOfDay.Hours < 17)
                        {
                            hours++;
                        }
                    }
                }

                return hours;
            }
        }
        public bool IsDeleted { get; set; }
        public Category Category { get; set; }
    }
}