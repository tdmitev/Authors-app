using System.ComponentModel.DataAnnotations;
namespace SportScore2.Api.DTO;

public record CreateAuthorDto(
    [Required, StringLength(100, MinimumLength = 2)]
    string Name
);