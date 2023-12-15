using Online_Exam.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Online_Exam.Models
{
    public class PrintViewModel
    {
        public IEnumerable<Exam> ExamViewModel { get; set; }
        public IEnumerable<Questions> QuestionsViewModel { get; set; }
        public IEnumerable<Choices> ChoicesViewModel { get; set; }
    }
}
