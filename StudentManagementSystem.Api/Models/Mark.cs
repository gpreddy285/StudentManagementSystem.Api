using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Api.Models
{
    public class Mark
    {
        [Key]
        public int MarksId {  get; set; }

        public int StudentId {  get; set; }

        public string SubjectName {  get; set; }

        public string ExamType {  get; set; }

        public int Marks {  get; set; }
        public Student? Student { get; set; }
    }
}
