namespace Installer.Model;

public interface IApplicationModel
{
    public Task<Application[]> GetAllApplications();
}