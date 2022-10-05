using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JixWebApi.Pages;

public class InfoModel : PageModel {
	public readonly IWebHostEnvironment WebHostEnv;

	public InfoModel(
		IWebHostEnvironment webHostEnv
		) {
		WebHostEnv = webHostEnv;
	}

	public void OnGet() {
	}
}
