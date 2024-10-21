namespace ApiProject.Core.Interfaces.IServices
{
    public interface IReportsService
    {
        Task<string> GetPerformanceReport(int userId);
    }
}
