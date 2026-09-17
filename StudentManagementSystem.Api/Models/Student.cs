using System.Text.Json.Serialization;

namespace StudentManagementSystem.Api.Models
{
    public class Student
    {
        public int StudentId {  get; set; }

        public string? StudentName {  get; set; }

        public int StudentAge { get; set; }

        public string? StudentEmail {  get; set; }

        public int CourseId { get; set; }
        [JsonIgnore]
        public Course? Course { get; set; }

        public int TeacherId {  get; set; }
        public Teacher? Teacher { get; set; }
        [JsonIgnore]
        public ICollection<Mark>? Marks { get; set; }
    }
}
