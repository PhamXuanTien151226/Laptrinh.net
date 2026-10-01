namespace TechMartManager
{
    public class Category
    {
        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

        public Category(string id, string name)
        {
            CategoryId = id;
            CategoryName = name;
        }
    }
}