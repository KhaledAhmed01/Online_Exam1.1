using Online_Exam.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Exam.Models
{
    public class Choices
    {
        [Key, Column(Order = 0)]
        [Required]
        // [ForeignKey("Questions")]
        public int Exam_id { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        // [ForeignKey("Questions")]
        public int Question_id { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        public string Choice_text { get; set; }

        public bool Is_correct { get; set; }


        [ForeignKey("Exam_id,Question_id")]
        public virtual Questions Questions { get; set; } = null!;
    }
}
