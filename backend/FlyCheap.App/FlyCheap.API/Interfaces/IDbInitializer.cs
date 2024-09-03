namespace FlyCheap.API.Interfaces
{
    public interface IDbInitializer
    {
        Task SeedDatabaseAsync(string filePath);
    }
}
