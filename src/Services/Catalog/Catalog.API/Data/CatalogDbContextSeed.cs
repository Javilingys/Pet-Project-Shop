using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data
{
    public class CatalogDbContextSeed
    {
        // Brand constatnts
        private const string ZaraBrandTitle = "Zara";
        private const string HMTBranditle = "H&M";
        private const string BershkaBrandTitle = "Bershka";
        private const string HendersonBrandTitle = "Henderson";
        private const string AppleBrandTitle = "Apple";
        private const string XiaomiBrandTitle = "Xiaomi";
        // Type constatns
        private const string DressTypedTitle = "Dress";
        private const string SuitTypeitle = "Suit";
        private const string ShoesTypeTitle = "Shoes";
        private const string HatsTypeTitle = "Hats";
        private const string PantsTypeTitle = "Pants";
        private const string SmartphonesTypeTitle = "Smartphones";


        public static async Task Seed(IHost app)
        {
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            try
            {
                var logger = loggerFactory.CreateLogger<CatalogDbContextSeed>();
                var context = serviceProvider.GetRequiredService<CatalogDbContext>();

                logger.LogInformation("--> Apllying Migrations started");
                await context.Database.MigrateAsync();
                logger.LogInformation("--> Apllying Migrations successfully ended");

                logger.LogInformation("--> Seeding started");
                await SeedProductData(context);
                logger.LogInformation("--> Seeding successfully ended");
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<CatalogDbContextSeed>();
                logger.LogError(ex, "--> Error during Seeding/Applying migration process");
            }
        }

        private static async Task SeedProductData(CatalogDbContext context)
        {
            bool isProductsExists = await context.Products.AnyAsync();
            List<ProductBrand> brands = await SeedProductBrands(context, isProductsExists);
            List<ProductType> types = await SeedProductTypes(context, isProductsExists);

            if (isProductsExists == false)
            {
                await SeedProducts(context, brands, types);
            }

            await context.SaveChangesAsync();
        }

        private static async Task<List<ProductBrand>> SeedProductBrands(CatalogDbContext context, bool isProductsExists)
        {
            List<ProductBrand> brands = null;

            if (await context.ProductBrands.AnyAsync() == false)
            {
                brands = new()
                {
                    new ProductBrand
                    {
                        Title = ZaraBrandTitle
                    },
                    new ProductBrand
                    {
                        Title = HMTBranditle
                    },
                    new ProductBrand
                    {
                        Title = BershkaBrandTitle
                    },
                    new ProductBrand
                    {
                        Title = HendersonBrandTitle
                    },
                    new ProductBrand
                    {
                        Title = AppleBrandTitle
                    },
                    new ProductBrand
                    {
                        Title = XiaomiBrandTitle
                    }
                };

                context.ProductBrands.AddRange(brands);
            }
            else if (isProductsExists == false)
            {
                brands = await context.ProductBrands.ToListAsync();
            }

            return brands;
        }

        private static async Task<List<ProductType>> SeedProductTypes(CatalogDbContext context, bool isProductsExists)
        {
            List<ProductType> types = null;

            if (await context.ProductTypes.AnyAsync() == false)
            {
                types = new()
                {
                    new ProductType
                    {
                        Title = DressTypedTitle
                    },
                    new ProductType
                    {
                        Title = SuitTypeitle
                    },
                    new ProductType
                    {
                        Title = ShoesTypeTitle
                    },
                    new ProductType
                    {
                        Title = HatsTypeTitle
                    },
                    new ProductType
                    {
                        Title = PantsTypeTitle
                    },
                    new ProductType
                    {
                        Title = SmartphonesTypeTitle
                    }
                };

                context.ProductTypes.AddRange(types);
            }
            else if (isProductsExists == false)
            {
                types = await context.ProductTypes.ToListAsync();
            }

            return types;
        }

        private static async Task SeedProducts(CatalogDbContext context,
                                               List<ProductBrand> brands,
                                               List<ProductType> types)
        {
            if (await context.Products.AnyAsync() == false)
            {
                List<Product> products = new()
                {
                    new Product
                    {
                        Title = "Beaty Zara Dress For Adults",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 79.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For kids",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 60.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For Women",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 80.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Hippy dress from H&M",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 180.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Chip dress for homless",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 10.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == BershkaBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Adult night dress",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 110.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == BershkaBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Super Style suit for buisnessmen",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 1110.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HendersonBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "Suit for date",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 528.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HendersonBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "Suit for Z generation",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 777.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "IPhone 13",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 1300.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == AppleBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SmartphonesTypeTitle)
                    },
                    new Product
                    {
                        Title = "Xiamoi Super mega 228Mp Camera Super shot",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 500.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == XiaomiBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SmartphonesTypeTitle)
                    },
                    new Product
                    {
                        Title = "Panama hat",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 28.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == HatsTypeTitle)
                    },
                    new Product
                    {
                        Title = "Cap",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 8.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == HatsTypeTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For Adults",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 79.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For kids",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 60.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For Women",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 80.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For Adults",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 79.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For kids",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 60.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For Women",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 80.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For Adults",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 79.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For kids",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 60.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For Women",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 80.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For Adults",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 79.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For kids",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 60.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Beaty Zara Dress For Women",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 80.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Hippy dress from H&M",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 180.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Chip dress for homless",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 10.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == BershkaBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Adult night dress",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 110.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == BershkaBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Super Style suit for buisnessmen",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 1110.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HendersonBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "Suit for date",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 528.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HendersonBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "Suit for Z generation",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 777.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "IPhone 13",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 1300.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == AppleBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SmartphonesTypeTitle)
                    },
                    new Product
                    {
                        Title = "Xiamoi Super mega 228Mp Camera Super shot",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 500.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == XiaomiBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SmartphonesTypeTitle)
                    },
                    new Product
                    {
                        Title = "Panama hat",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 28.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == HatsTypeTitle)
                    },
                    new Product
                    {
                        Title = "Cap",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 8.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == HatsTypeTitle)
                    },
                    new Product
                    {
                        Title = "Hippy dress from H&M",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 180.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Chip dress for homless",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 10.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == BershkaBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Adult night dress",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 110.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == BershkaBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Super Style suit for buisnessmen",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 1110.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HendersonBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "Suit for date",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 528.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HendersonBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "Suit for Z generation",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 777.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "IPhone 13",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 1300.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == AppleBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SmartphonesTypeTitle)
                    },
                    new Product
                    {
                        Title = "Xiamoi Super mega 228Mp Camera Super shot",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 500.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == XiaomiBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SmartphonesTypeTitle)
                    },
                    new Product
                    {
                        Title = "Panama hat",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 28.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == HatsTypeTitle)
                    },
                    new Product
                    {
                        Title = "Cap",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 8.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == HatsTypeTitle)
                    },
                    new Product
                    {
                        Title = "Hippy dress from H&M",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 180.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Chip dress for homless",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 10.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == BershkaBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Adult night dress",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 110.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == BershkaBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == DressTypedTitle)
                    },
                    new Product
                    {
                        Title = "Super Style suit for buisnessmen",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 1110.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HendersonBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "Suit for date",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 528.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HendersonBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "Suit for Z generation",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 777.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == ZaraBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SuitTypeitle)
                    },
                    new Product
                    {
                        Title = "IPhone 13",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 1300.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == AppleBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SmartphonesTypeTitle)
                    },
                    new Product
                    {
                        Title = "Xiamoi Super mega 228Mp Camera Super shot",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 500.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == XiaomiBrandTitle),
                        ProductType = types.FirstOrDefault(x => x.Title == SmartphonesTypeTitle)
                    },
                    new Product
                    {
                        Title = "Panama hat",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 28.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == HatsTypeTitle)
                    },
                    new Product
                    {
                        Title = "Cap",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Nullam porttitor maximus lacus vitae gravida. Suspendisse eleifend nisi mollis tincidunt suscipit. ",
                        Price = 8.99m,
                        ProductBrand = brands.FirstOrDefault(x => x.Title == HMTBranditle),
                        ProductType = types.FirstOrDefault(x => x.Title == HatsTypeTitle)
                    },
                };

                context.Products.AddRange(products);
            }
        }
    }
}
