using System.ComponentModel.DataAnnotations;

namespace DemoProject.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}