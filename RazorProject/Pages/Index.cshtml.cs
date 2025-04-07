using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace RazorProject.Pages
{
    public class IndexModel : PageModel
    {
        public List<ClassInformationTable> ClassList { get; set; } = new List<ClassInformationTable>();
        public string FilterClassName { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;

        public void OnGet(string filterClassName, int currentPage = 1)
        {
            // Generate synthetic data for testing
            var allClasses = Enumerable.Range(1, 100).Select(i => new ClassInformationTable
            {
                Id = i,
                ClassName = $"Class {i}",
                StudentCount = i * 10,
                Description = $"Description for Class {i}"
            }).ToList();

            // Apply filtering
            if (!string.IsNullOrEmpty(filterClassName))
            {
                allClasses = allClasses
                    .Where(c => c.ClassName.Contains(filterClassName, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Calculate pagination
            TotalPages = (int)System.Math.Ceiling(allClasses.Count / (double)PageSize);
            CurrentPage = currentPage < 1 ? 1 : currentPage > TotalPages ? TotalPages : currentPage;

            ClassList = allClasses
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            FilterClassName = filterClassName;
        }
    }
}