﻿using Identity.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Identity.Models.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser , AppRole , string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
    }
}
