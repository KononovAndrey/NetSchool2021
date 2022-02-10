﻿namespace DSRNetSchool.Db.Domain;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public UserStatus Status { get; set; }
}
