namespace Web.API.Models.FamilyMembers;

using System.ComponentModel.DataAnnotations;

public record FamilyMemberCreateOrUpdateModel
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; }
}