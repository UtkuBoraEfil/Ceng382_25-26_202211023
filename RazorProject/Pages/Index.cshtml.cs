using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace RazorProject.Pages
{
    public class IndexModel : PageModel
    {
        private static List<ClassInformationModel> _classList = new List<ClassInformationModel>
        {
            new ClassInformationModel { Id = 1, ClassName = "Math", StudentCount = 30, Description = "Mathematics Class" },
            new ClassInformationModel { Id = 2, ClassName = "Science", StudentCount = 25, Description = "Science Class" }
        };

        private static int _idCounter = _classList.Max(c => c.Id) + 1;

        [BindProperty]
        public ClassInformationModel NewClass { get; set; }
        public List<ClassInformationModel> ClassList => _classList;

        public void OnGet()
        {
        }

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewClass.Id = _idCounter++;
            _classList.Add(NewClass);
            NewClass = new ClassInformationModel(); // Reset the form

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var classToDelete = _classList.FirstOrDefault(c => c.Id == id);
            if (classToDelete != null)
            {
                _classList.Remove(classToDelete);
            }

            return RedirectToPage();
        }
    }
}