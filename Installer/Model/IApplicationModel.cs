namespace Installer.Model;

public interface IApplicationModel
{
    public Task<Application[]> GetAllApplications();
    public Task<ApplicationVersion[]> GetApplicationVersions(int appId);
}