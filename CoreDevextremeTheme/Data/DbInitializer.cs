namespace CoreDevextremeTheme.Data
{
    public static class DbInitializer
    {
        //private readonly ModelBuilder modelBuilder;

        //public DbInitializer(ModelBuilder modelBuilder)
        //{
        //    this.modelBuilder = modelBuilder;
        //}
        public static void Seed(ApplicationDbContext context)
        {

            #region Identity
            var roleAdmin = new IdentityRole
            {
                Id = Guid.NewGuid().ToString("D"),
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var roleUser = new IdentityRole
            {
                Id = Guid.NewGuid().ToString("D"),
                Name = "User",
                NormalizedName = "USER"
            };

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(roleAdmin, roleUser);
            }

            var userAdmin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString("D"),
                RegistrationNumber= 5311,
                Email = "mansur.dincer@filidea.com.tr",
                UserName = "mansur.dincer@filidea.com.tr",
                NormalizedUserName = "MANSUR.DINCER@FILIDEA.COM.TR",
                EmailConfirmed=false,
                PasswordHash = "AQAAAAIAAYagAAAAEKRqktMpiSxEzzjTshLnmIP4dPYdVFQyc87whRmF61I1lEX1baTNNtQB3TiTbglttg==", //12345Aa.
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
            };

            if (!context.ApplicationUsers.Any())
            {
                context.ApplicationUsers.Add(userAdmin);
            }


            var userAdminRole = new IdentityUserRole<string>
            {
                RoleId = roleAdmin.Id,
                UserId = userAdmin.Id
            };

            if (!context.UserRoles.Any())
            {
                context.UserRoles.Add(userAdminRole);
            }

            #endregion

            #region Organisation
            var departmentBase = new Department
            {
                Id = Guid.NewGuid(),
                ParentId = null,
                Sequence = 0,
                Name = "Genel Müdürlük"
            };

            var departmentIt = new Department
            {
                Id = Guid.NewGuid(),
                ParentId = departmentBase.Id,
                Sequence = 1,
                Name = "Bilgi İşlem"
            };

            var departmentHr = new Department
            {
                Id = Guid.NewGuid(),
                ParentId = departmentBase.Id,
                Sequence = 2,
                Name = "İnsan Kaynakları"
            };

            var departmentElectric = new Department
            {
                Id = Guid.NewGuid(),
                ParentId = departmentBase.Id,
                Sequence = 3,
                Name = "Elektrik Bakım "
            };

            var departmentMechanic = new Department
            {
                Id = Guid.NewGuid(),
                ParentId = departmentBase.Id,
                Sequence = 4,
                Name = "Mekanik Bakım"
            };

            var departmentProduction = new Department
            {
                Id = Guid.NewGuid(),
                ParentId = departmentBase.Id,
                Sequence = 5,
                Name = "Üretim"
            };

            if (!context.Departments.Any())
                context.Departments.AddRange(departmentBase, departmentIt, departmentHr, departmentElectric, departmentMechanic, departmentProduction);


            var jobTitleWhiteCollar = new JobTitle
            {
                Id = Guid.NewGuid(),
                Name = "Beyaz Yaka"
            };

            var jobTitleBlueCollar = new JobTitle
            {
                Id = Guid.NewGuid(),
                Name = "Mavi Yaka"
            };
            if(!context.JobTitles.Any())
            context.JobTitles.AddRange(jobTitleWhiteCollar, jobTitleBlueCollar);

            //var ApplicationUserAbdullahKasim = new ApplicationUser
            //{
            //    Id = Guid.NewGuid(),
            //    RegistrationNumber = "0274",
            //    Name = "Abdullah",
            //    Surname = "Kasım",
            //    DepartmentId = departmentIt.Id,
            //    JobTitleId = jobTitleWhiteCollar.Id,
            //};

            //var ApplicationUserMansurDincer = new ApplicationUser
            //{
            //    Id = Guid.NewGuid(),
            //    RegistrationNumber = "5311",
            //    Name = "Mansur",
            //    Surname = "Dinçer",
            //    DepartmentId = departmentIt.Id,
            //    JobTitleId = jobTitleBlueCollar.Id,
            //};

            //var ApplicationUserMuhammerSayin = new ApplicationUser
            //{
            //    Id = Guid.NewGuid(),
            //    RegistrationNumber = "4545",
            //    Name = "Muhammer",
            //    Surname = "Sayın",
            //    DepartmentId = departmentIt.Id,
            //    JobTitleId = jobTitleBlueCollar.Id,
            //};

            //if(!context.JobTitles.Any())
            //context.Employees.AddRange(ApplicationUserAbdullahKasim, ApplicationUserMansurDincer, ApplicationUserMuhammerSayin);

            #endregion                      

            context.SaveChanges();
        }

    }
}
