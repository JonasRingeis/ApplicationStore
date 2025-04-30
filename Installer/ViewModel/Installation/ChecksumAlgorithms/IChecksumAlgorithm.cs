using System.IO;

namespace Installer.ViewModel.Installation.ChecksumAlgorithms;

public interface IChecksumAlgorithm
{
    public bool VerifyChecksum(string checksum, byte[] data);
    public Task<bool> VerifyChecksumAsync(string checksum, Stream dataStream);
}