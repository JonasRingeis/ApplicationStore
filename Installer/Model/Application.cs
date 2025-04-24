using System.ComponentModel.DataAnnotations.Schema;
using DapperExtensions.Mapper;

namespace Installer.Model;

public class Application
{
    [Column("application_id")]
    public required int ApplicationId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    
    [Column("publisher_id")]
    public required int PublisherId { get; set; }
    
    [Column("publisher_name")]
    public required string PublisherName { get; set; }

    
    public override string ToString()
    {
        return
            $"{nameof(ApplicationId)}: {ApplicationId}, {nameof(Title)}: {Title}, {nameof(Description)}: {Description}, {nameof(PublisherId)}: {PublisherId}, {nameof(PublisherName)}: {PublisherName}";
    }
}