using System.Text.Json.Serialization;

namespace StudentManagementSystem.Api.Models
{
    public class Teacher
    {
        public int TeacherId {  get; set; }

        public string TeacherName { get; set; }

        public string TeacherEmail {  get; set; }

        public int CourseId {  get; set; }
        [JsonIgnore]
        public ICollection<Student>? Students { get; set; }
    }
}
