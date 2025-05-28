using System.ComponentModel.DataAnnotations;
namespace SportScore2.Api.DTO;

public record UpdateAuthorDto(
    [Required, StringLength(100, MinimumLength = 2)]
    string Name
);