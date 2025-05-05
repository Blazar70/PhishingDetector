using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class SiteDb : DbContext
{
    public SiteDb(DbContextOptions<SiteDb> opt) : base(opt) { }

    public DbSet<Contact> Contacts => Set<Contact>();

    public DbSet<Scan> Scans => Set<Scan>();

}
