using System.ComponentModel.DataAnnotations.Schema;

namespace Installer.Model;

public class InstallationData
{
    [Column("download_url")]
    public required string DownloadUrl { get; set; }
    
    [Column("checksum_hash")]
    public required string ChecksumHash { get; set; }
    
    [Column("checksum_algorithm")]
    public required string ChecksumAlgorithm { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(DownloadUrl)}: {DownloadUrl}, {nameof(ChecksumHash)}: {ChecksumHash}, {nameof(ChecksumAlgorithm)}: {ChecksumAlgorithm}";
    }
}