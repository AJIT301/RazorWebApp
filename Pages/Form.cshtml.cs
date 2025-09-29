using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace FirstRazorApp.Pages
{
    public class FormModel : PageModel
    {
        [BindProperty]
        public string? Title { get; set; }

        [BindProperty]
        public string? Question { get; set; }

        public void OnGet()
        {
            // Optional: Initialize fields if needed
        }

        public IActionResult OnPost()
        {
            // Handle form submission
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save to file
            var data = $"Title: {Title}, Question: {Question}, Submitted at: {DateTime.Now}\n";
            var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "text-files");
            Directory.CreateDirectory(dirPath);
            var filePath = Path.Combine(dirPath, "submissions.txt");
            System.IO.File.AppendAllText(filePath, data);

            TempData["Message"] = "Form submitted and saved to file.";
            return RedirectToPage("/Index");
        }
    }
}
