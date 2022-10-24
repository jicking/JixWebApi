using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JixWebApi.Core;

public class MaxFileSizeAttribute : ValidationAttribute {
	private readonly int _maxFileSize;
	public MaxFileSizeAttribute(int maxFileSize) {
		_maxFileSize = maxFileSize;
	}

	protected override ValidationResult IsValid(
	object value, ValidationContext validationContext) {
		var file = value as IFormFile;
		if (file != null) {
			if (file.Length > _maxFileSize) {
				return new ValidationResult(GetErrorMessage());
			}
		}

		return ValidationResult.Success;
	}

	public string GetErrorMessage() {
		return $"Maximum allowed file size is {_maxFileSize} bytes.";
	}
}

// https://stackoverflow.com/questions/56588900/how-to-validate-uploaded-file-in-asp-net-core?msclkid=0743ca07d02f11eca099f39229ecbf02

public class AllowedExtensionsAttribute : ValidationAttribute {
	private readonly string[] _extensions;
	public AllowedExtensionsAttribute(string[] extensions) {
		_extensions = extensions;
	}

	protected override ValidationResult IsValid(
	object value, ValidationContext validationContext) {
		var file = value as IFormFile;
		if (file != null) {
			var extension = Path.GetExtension(file.FileName);
			if (!_extensions.Contains(extension.ToLower())) {
				return new ValidationResult(GetErrorMessage());
			}
		}

		return ValidationResult.Success;
	}

	public string GetErrorMessage() {
		return $"This photo extension is not allowed!";
	}
}