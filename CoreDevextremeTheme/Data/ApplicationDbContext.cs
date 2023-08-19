namespace CoreDevextremeTheme.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    //private readonly INotyfService _toastNotification;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) //, INotyfService toastNotification
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        //_toastNotification = toastNotification;
    }
    public string GetUserId()
    {
        return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    #region DbSets 

    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<DeletedEntityLog> DeletedEntityLogs { get; set; }
    public DbSet<ControllerRole> ControllerRoles { get; set; }

    public DbSet<Department> Departments { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<JobTitle> JobTitles { get; set; }

    public DbSet<Employee> Employees { get; set; }   

    #endregion

    #region Unmapped
    //public DbSet<NextMaintenance_ViewModel> NextMaintenanceTasks { get; set; } 
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<NextMaintenance_ViewModel>().HasNoKey().ToView(null);

        #region Global Filters

        modelBuilder.Entity<Employee>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Department>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<JobTitle>().HasQueryFilter(p => !p.IsDeleted);       

        #endregion

        #region Set DeleteBehavior

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        #endregion

        #region Set Computed Columns

        //modelBuilder.Entity<MaintenancePersonnel>().Property(p => p.DurationInMinutes).HasComputedColumnSql("DATEDIFF(MINUTE, BeginDate, EndDate)");

        #endregion

        #region Customization Of Department and JobTitle Models

        modelBuilder.Entity<Employee>().HasIndex(u => u.RegistrationNumber).IsUnique();
        modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.RegistrationNumber).IsUnique();

        /*Customized connection configuration for models that use the ApplicationUser model as a secondary.*/

        //modelBuilder.Entity<ApplicationUser>()
        //     .HasOne(u => u.Department)
        //     .WithMany()
        //     .HasForeignKey(u => u.DepartmentId)
        //     .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Department>()
        //    .HasOne(d => d.CreatedBy)
        //    .WithMany()
        //    .HasForeignKey(d => d.CreatedById)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Department>()
        //    .HasOne(d => d.UpdatedBy)
        //    .WithMany()
        //    .HasForeignKey(d => d.UpdatedById)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Department>()
        //    .HasOne(d => d.DeletedById)
        //    .WithMany()
        //    .HasForeignKey(d => d.DeletedById)
        //    .OnDelete(DeleteBehavior.Restrict);


        //modelBuilder.Entity<ApplicationUser>()
        //    .HasOne(u => u.JobTitle)
        //    .WithMany()
        //    .HasForeignKey(u => u.JobTitleId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<JobTitle>()
        //    .HasOne(jt => jt.CreatedBy)
        //    .WithMany()
        //    .HasForeignKey(jt => jt.CreatedById)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<JobTitle>()
        //    .HasOne(jt => jt.UpdatedBy)
        //    .WithMany()
        //    .HasForeignKey(jt => jt.UpdatedById)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<JobTitle>()
        //    .HasOne(jt => jt.DeletedById)
        //    .WithMany()
        //    .HasForeignKey(jt => jt.DeletedById)
        //    .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region DeleteBehavior old code
        //modelBuilder.Entity<ApplicationUser>().HasOne(
        //        p => p.Department).WithMany(
        //        p => p.Employees).HasForeignKey(
        //        p => p.DepartmentId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<ApplicationUser>().HasOne(
        //    p => p.JobTitle).WithMany(
        //    p => p.Employees).HasForeignKey(
        //    p => p.JobTitleId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Equipment>().HasOne(
        //    p => p.EquipmentModel).WithMany(
        //    p => p.Equipments).HasForeignKey(
        //    p => p.EquipmentModelId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<EquipmentModel>().HasOne(
        //    p => p.EquipmentType).WithMany(
        //    p => p.EquipmentModels).HasForeignKey(
        //    p => p.EquipmentTypeId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<EquipmentModel>().HasOne(
        //    p => p.EquipmentBrand).WithMany(
        //    p => p.EquipmentModels).HasForeignKey(
        //    p => p.EquipmentBrandId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<EquipmentType>().HasOne(
        //    p => p.EquipmentCategory).WithMany(
        //    p => p.EquipmentTypes).HasForeignKey(
        //    p => p.EquipmentCategoryId).OnDelete(DeleteBehavior.Restrict);


        //modelBuilder.Entity<InventoryItem>().HasOne(
        //    p => p.InventoryCategory).WithMany(
        //    p => p.InventoryItems).HasForeignKey(
        //    p => p.InventoryCategoryId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<InventoryTransaction>().HasOne(
        //    p => p.InventoryItem).WithMany(
        //    p => p.InventoryTransactions).HasForeignKey(
        //    p => p.InventoryItemId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<TicketCategory>().HasOne(
        //    p => p.Parent).WithMany(
        //    p => p.Children).HasForeignKey(
        //    p => p.ParentId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<MaintenanceFormTask>().HasOne(
        //    p => p.MaintenanceForm).WithMany(
        //    p => p.MaintenanceFormTasks).HasForeignKey(
        //    p => p.MaintenanceFormId).OnDelete(DeleteBehavior.Restrict);


        //modelBuilder.Entity<MaintenanceInventoryItem>().HasOne(
        //    p => p.InventoryItem).WithMany(
        //    p => p.MaintenanceInventoryItems).HasForeignKey(
        //    p => p.InventoryItemId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<MaintenanceTask>().HasOne(
        //    p => p.Maintenance).WithMany(
        //    p => p.MaintenanceTasks).HasForeignKey(
        //    p => p.MaintenanceId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Ticket>().HasOne(
        //    p => p.ApplicationUser).WithMany(
        //    p => p.Tickets).HasForeignKey(
        //    p => p.ApplicationUserId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Ticket>().HasOne(
        //    p => p.TicketCategory).WithMany(
        //    p => p.Tickets).HasForeignKey(
        //    p => p.TicketCategoryId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<TicketDiscussion>().HasOne(
        //    p => p.ApplicationUser).WithMany(
        //    p => p.TicketDiscussions).HasForeignKey(
        //    p => p.ApplicationUserId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<MaintenanceForm>().HasOne(
        //    p => p.EquipmentModel).WithMany(
        //    p => p.MaintenanceForms).HasForeignKey(
        //    p => p.EquipmentModelId).OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<FaultReport>().HasMany(
        //    p => p.Maintenances).WithOne(
        //    p => p.FaultReport).HasForeignKey(
        //    p => p.FaultReportId).OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region Change Collation, SetMaxLength and SetIsUnicode

        // Change the collation of the database to Turkish_CI_AS
        modelBuilder.UseCollation("Turkish_CI_AS");

        // Other modelBuilder stuff here
        // This line only required if using ASP.Net Identity:
        base.OnModelCreating(modelBuilder);
        // Force all string-based columns to non-unicode equivalent 
        // when no column type is explicitly set.


        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(
                   p => p.ClrType == typeof(string)    // Entity is a string
                && p.GetColumnType() == null           // No column type is set
                                                       // Next line only required if using ASP.Net Identity:
                && !p.DeclaringEntityType.GetTableName().StartsWith("AspNet")
            ))
        {
            if (property.Name != "EntityJson")
            {
                property.SetIsUnicode(false);
                property.SetMaxLength(1000);
            }
        }

        #endregion       

    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        #region return for IdentityUser models
        if (ChangeTracker.Entries().Any(x => x.Entity is ApplicationUser))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        #endregion

        var userId = Guid.Parse(GetUserId());

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity entity)
            {
                #region Add
                if (entry.State == EntityState.Added)
                {
                    entity.Id = Guid.NewGuid();
                    entity.CreatedById = userId;
                    entity.CreatedAt = DateTime.Now;
                }
                #endregion

                #region Modify
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedById = userId;
                    entity.UpdatedAt = DateTime.Now;
                }
                #endregion

                #region Soft Delete
                else if (entry.State == EntityState.Deleted && entity is ISoftDeletable)
                {
                    var message = "";

                    if (entity.CanDelete(this, out message))
                    {
                        var deletedAt = DateTime.Now;

                        entry.State = EntityState.Modified;
                        entity.DeletedById = userId;
                        entity.DeletedAt = deletedAt;
                        entity.IsDeleted = true;

                        DeletedEntityLogs.Add(
                            new DeletedEntityLog
                            {
                                Id = Guid.NewGuid(),
                                EntityId = entity.Id,
                                EntityName = entity.GetType().Name,
                                EntityType = entity.GetType().ToString(),
                                EntityJson  = JsonConvert.SerializeObject(entity),
                                DeletedById = userId,
                                DeletedAt = deletedAt
                            }
                        );
                        //_toastNotification.Success("Kayıt geri dönüşüm kutusuna gönderildi.");
                    }

                    else
                    {
                        //show message to user
                        Console.WriteLine(message);
                        entry.State = EntityState.Unchanged;
                    }
                }
                #endregion

                #region Set string properties named "Description" to sentence case

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    foreach (var property in entry.Properties)
                    {
                        if (property.Metadata.ClrType == typeof(string))
                        {
                            if (property.Metadata.Name == "Description")
                            {
                                var value = property.CurrentValue as string;
                                if (!string.IsNullOrEmpty(value))
                                {
                                    property.CurrentValue = value.ToSentenceCase();
                                }
                            }
                        }
                    }
                }

                #endregion
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

  

}