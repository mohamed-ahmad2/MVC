namespace ProjectMVC1.Models
{
    public class ProductsBL
    {
        private static List<Product> products = new ()
        {
            new Product { Id = 1, Name = "Milk", Price = 20, Description = "drink", ImageUrl = "milk.jpeg"},
            new Product { Id = 2, Name = "Laptop", Price = 1000, Description = "Gaming laptop", ImageUrl = "laptob.jpeg" },
            new Product { Id = 3, Name = "Phone", Price = 500, Description = "Smartphone", ImageUrl = "phone.jpeg" }
        };

        public List<Product> GetAll()
        {
            return products;
        }

        public Product? GetById(int id)
        {
            return products.FirstOrDefault(e => e.Id == id);
        }
    }
}
