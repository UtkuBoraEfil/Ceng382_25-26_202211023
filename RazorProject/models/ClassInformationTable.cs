using System.ComponentModel.DataAnnotations;

namespace RazorProject.Models
{
    public class ClassInformationTable
    {
        public int Id { get; set; } // Used for backend actions like Edit/Delete
        [Display(Name = "Class Name")]
        public string ClassName { get; set; } = string.Empty; // Default value added
        [Display(Name = "Student Count")]
        public int StudentCount { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty; // Default value added
    }
}