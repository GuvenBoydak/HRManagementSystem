using Microsoft.AspNetCore.Identity;

namespace HrManagement.Domain.Entities.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string NameSurname { get; set; }
}