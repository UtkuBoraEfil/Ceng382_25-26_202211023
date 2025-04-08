using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace RazorProject.Pages
{
    public class IndexModel : PageModel
    {
        private static List<ClassInformationTable> AllClasses = Enumerable.Range(1, 100).Select(i => new ClassInformationTable
        {
            Id = i,
            ClassName = $"Class {i}",
            StudentCount = i * 10,
            Description = $"Description for Class {i}"
        }).ToList();

        public List<ClassInformationTable> ClassList { get; set; } = new List<ClassInformationTable>();
        public string FilterClassName { get; set; } = string.Empty;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public int? EditingId { get; set; } // Nullable to indicate no item is being edited

        public void OnGet(string filterClassName, int currentPage = 1)
        {
            // Apply filtering
            var filteredClasses = AllClasses;
            if (!string.IsNullOrEmpty(filterClassName))
            {
                filteredClasses = filteredClasses
                    .Where(c => c.ClassName.Contains(filterClassName, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Calculate pagination
            TotalPages = (int)System.Math.Ceiling(filteredClasses.Count / (double)PageSize);
            CurrentPage = currentPage < 1 ? 1 : currentPage > TotalPages ? TotalPages : currentPage;

            ClassList = filteredClasses
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            FilterClassName = filterClassName;
        }

        public IActionResult OnPostDelete(int id)
        {
            Console.WriteLine($"Deleting item with ID: {id}");
            var itemToDelete = AllClasses.FirstOrDefault(c => c.Id == id);
            if (itemToDelete != null)
            {
                AllClasses.Remove(itemToDelete); // Remove the item from the static list
            }

            return RedirectToPage();
        }

        public IActionResult OnPostEdit(int id)
        {
            EditingId = id; // Set the item being edited
            return RedirectToPage();
        }

        public IActionResult OnPostSave(int id, string className, int studentCount, string description)
        {
            var itemToEdit = AllClasses.FirstOrDefault(c => c.Id == id);
            if (itemToEdit != null)
            {
                itemToEdit.ClassName = className;
                itemToEdit.StudentCount = studentCount;
                itemToEdit.Description = description;
            }

            EditingId = null; // Clear the editing state
            return RedirectToPage();
        }
    }
}