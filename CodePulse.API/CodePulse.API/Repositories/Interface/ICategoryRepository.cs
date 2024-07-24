namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<List<string>> GetAllAsync();
        Task<string> GetByNameAsync(string name);
        Task<string> CreateAsync(string name);
        Task<string> UpdateAsync(string oldName, string newName);
        Task<string> DeleteAsync(string name);
    }
}
