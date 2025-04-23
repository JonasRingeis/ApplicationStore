namespace Installer.Model;

public class Application
{
    public required int ApplicationId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int PublisherId { get; set; }
    public required string PublisherName { get; set; }

    
    public override string ToString()
    {
        return
            $"{nameof(ApplicationId)}: {ApplicationId}, {nameof(Title)}: {Title}, {nameof(Description)}: {Description}, {nameof(PublisherId)}: {PublisherId}, {nameof(PublisherName)}: {PublisherName}";
    }
}