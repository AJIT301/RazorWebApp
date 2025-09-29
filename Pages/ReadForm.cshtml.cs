namespace FirstRazorApp.Pages
{
    public class ReadFormModel : PageModel
    {
        public List<Submission> Submissions { get; set; } = new();

        public void OnGet()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "text-files", "submissions.txt");
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        // Parse line: "Title: {Title}, Question: {Question}, Submitted at: {DateTime}"
                        var parts = line.Split(", Submitted at: ");
                        if (parts.Length == 2)
                        {
                            var dateStr = parts[1];
                            var data = parts[0].Split(", Question: ");
                            if (data.Length == 2)
                            {
                                var titleStr = data[0].Substring("Title: ".Length);
                                var questionStr = data[1];
                                if (DateTime.TryParse(dateStr, out var submittedAt))
                                {
                                    Submissions.Add(new Submission { Title = titleStr, Question = questionStr, SubmittedAt = submittedAt });
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public class Submission
    {
        public string Title { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
    }
}
