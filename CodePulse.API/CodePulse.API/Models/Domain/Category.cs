namespace CodePulse.API.Models.Domain
{
    public class Category : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
