namespace Catalog.API.Entities
{
    /// <summary>
    /// Product of shop
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        /// <summary>
        /// Name of product
        /// </summary>
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Navigation Properties 
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
