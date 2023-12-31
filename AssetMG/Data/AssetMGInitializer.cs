using AssetMG.Models;

namespace AssetMG.Data
{
    public class AssetMGInitializer
    {
        public static void Initialize1(AssetMGDbContext Asset_Typecontext)
        {
               Asset_Typecontext.Database.EnsureCreated();
            if (Asset_Typecontext.Asset_Type.Any())
                return;
            Asset_Type[] asset_type = new[]
            {
                new Asset_Type { ATId = 1, ATName = "Computer" },
                new Asset_Type { ATId = 2, ATName = "Furniture" }
            };
            foreach (var type in asset_type)
            {
                Asset_Typecontext.Asset_Type.Add(type);
            }
            Asset_Typecontext.SaveChanges();
        }
        public static void Initialize2(AssetMGDbContext Departmentcontext)
        {
            Departmentcontext.Database.EnsureCreated();
            if (Departmentcontext.Department.Any())
                return;
            Department[] department = new[]
            {
              new Department { DId = 1, DName = "IT" },
              new Department { DId = 2, DName = "Finance" }
            };
            foreach (var name in department)
            {
                Departmentcontext.Department.Add(name);
            }
            Departmentcontext.SaveChanges();
        }
        public static void Initialize3(AssetMGDbContext Userscontext)
        {
            Userscontext.Database.EnsureCreated();
            if (Userscontext.Users.Any())
                return;
            Users[] users = new[]
            {
              new Users
    {
        Uid = 1,
        FirstName = "John",
        LastName = "Doe",
        Position = "Manager",
        Username = "johndoe",
        Password = "password123",
        DId = 1, // Assuming the Department ID for this user
        Department = new Department { DId = 1, DName = "HR" } // Sample department linked to the user
    },
        new Users
    {
        Uid = 2,
        FirstName = "kokou",
        LastName = "pomenou",
        Position = "IT spec",
        Username = "kpomenou",
        Password = "pass456",
        DId = 2, // Assuming the Department ID for this user
        Department = new Department { DId = 2, DName = "Finance" } // Sample department linked to the user
    }
            };
            foreach (var user in users)
            {
                Userscontext.Users.Add(user);
            }
            Userscontext.SaveChanges();
        }
        public static void Initialize4(AssetMGDbContext Asset_Mvmt_Typecontext)
        {
            Asset_Mvmt_Typecontext.Database.EnsureCreated();
            if (Asset_Mvmt_Typecontext.MvmtTypes.Any())
                return;
            Asset_Mvmt_Type[] mvmt_type = new[]
            {
               new Asset_Mvmt_Type { AMId = 1, Asset_Mvmt_Type_Name = "Transfer", MDescription = "Transfer of assets" },
               new Asset_Mvmt_Type { AMId = 2, Asset_Mvmt_Type_Name = "Maintenance", MDescription = "Asset maintenance record" }

        };
            foreach (var mvmt in mvmt_type )
            {
                Asset_Mvmt_Typecontext.MvmtTypes.Add(mvmt);
            }
            Asset_Mvmt_Typecontext.SaveChanges();
        }
        public static void Initialize5(AssetMGDbContext Asset_Locationcontext)
        {
            Asset_Locationcontext.Database.EnsureCreated();
            if (Asset_Locationcontext.Locations.Any())
                return;
            Asset_Location[] locations = new[]
            {
              new Asset_Location { ALId = 1, ALName = "Document room" },
              new Asset_Location { ALId = 2, ALName = "Warehouse B" }
            };
            foreach (var location in locations)
            {
                Asset_Locationcontext.Locations.Add(location);
            }
            Asset_Locationcontext.SaveChanges();
        }
        public static void Initialize6(AssetMGDbContext Assetscontext)
        {
            Assetscontext.Database.EnsureCreated();
            if (Assetscontext.Locations.Any())
                return;
            Assets[] assets = new[]
            {
              new Assets
    {
        Id = 1,
        Aname = "Laptop",
        Quantity = 5,
        Cost = 1000,
        Shelve = 3,
        ImagePath = "/images/laptop.jpg",
        Locker = 102,
        Date = DateTime.Now,
        AssetTypeId = 1, // Assuming the Asset Type ID for this asset
        AssetType = new Asset_Type { ATId = 1, ATName = "Computer" }, // Sample Asset Type linked to the asset
        CreateByUserId = 1, // Assuming the User ID who created this asset
        CreatedByUser = new Users { Uid = 1, FirstName = "John", LastName = "Doe" }, // Sample User who created the asset
        DId = 1, // Assuming the Department ID for this asset
        Department = new Department { DId = 1, DName = "HR" }, // Sample Department linked to the asset
        LocationId = 1, // Assuming the Location ID for this asset
        Location = new Asset_Location { ALId = 1, ALName = "Building A" } // Sample Location linked to the asset
    },
    new Assets
    {
        Id = 2,
        Aname = "Chair",
        Quantity = 10,
        Cost = 50,
        Shelve = 1,
        ImagePath = "/images/chair.jpg",
        Locker = 105,
        Date = DateTime.Now,
        AssetTypeId = 2, // Assuming the Asset Type ID for this asset
        AssetType = new Asset_Type { ATId = 2, ATName = "Furniture" }, // Sample Asset Type linked to the asset
        CreateByUserId = 2, // Assuming the User ID who created this asset
        CreatedByUser = new Users { Uid = 2, FirstName = "Jane", LastName = "Smith" }, // Sample User who created the asset
        DId = 2, // Assuming the Department ID for this asset
        Department = new Department { DId = 2, DName = "Finance" }, // Sample Department linked to the asset
        LocationId = 2, // Assuming the Location ID for this asset
        Location = new Asset_Location { ALId = 2, ALName = "Warehouse B" } // Sample Location linked to the asset
    }
            };
            foreach (var asset in assets)
            {
                Assetscontext.Assets.Add(asset);
            }
            Assetscontext.SaveChanges();
        }
    }
}
