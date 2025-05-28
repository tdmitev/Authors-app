using System;
using System.ComponentModel.DataAnnotations;
namespace SportScore2.Api.DTO;

public record CreateArticleDto(
    [Required, StringLength(200, MinimumLength = 5)]
    string Title,

    [Required, MinLength(10)]
    string Content,

    DateTime? Published
);