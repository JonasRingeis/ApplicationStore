using System.ComponentModel.DataAnnotations.Schema;

namespace Installer.Model;

public class ApplicationVersion
{
    [Column("application_version_id")]
    public required int ApplicationVersionId { get; set; }
    
    [Column("version_name")]
    public required string VersionName { get; set; }
    
    [Column("uploaded_at")]
    public required DateTime UploadedAt { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(ApplicationVersionId)}: {ApplicationVersionId}, {nameof(VersionName)}: {VersionName}, {nameof(UploadedAt)}: {UploadedAt}";
    }
}