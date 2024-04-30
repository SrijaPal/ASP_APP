using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyStore.Pages
{
    public class DocumentationModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        public DocumentationModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
