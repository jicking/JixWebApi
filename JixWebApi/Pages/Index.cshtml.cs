using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JixWebApi.Pages {
	public class IndexModel : PageModel {
		private readonly ILogger<IndexModel> _logger;

		public readonly IWebHostEnvironment WebHostEnv;

		public IndexModel(
			ILogger<IndexModel> logger,
			IWebHostEnvironment webHostEnv
			) {
			_logger = logger;
			WebHostEnv = webHostEnv;
		}

		public void OnGet() {

		}
	}
}