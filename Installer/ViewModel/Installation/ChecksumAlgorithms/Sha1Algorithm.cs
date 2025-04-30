using System.IO;
using System.Security.Cryptography;

namespace Installer.ViewModel.Installation.ChecksumAlgorithms;

public class Sha1Algorithm : IChecksumAlgorithm
{
    public bool VerifyChecksum(string checksum, byte[] data)
    {
        var hashBytes = SHA1.HashData(data);
        var newChecksum = Convert.ToHexString(hashBytes);
        
        return newChecksum == checksum;
    }
    
    public async Task<bool> VerifyChecksumAsync(string checksum, Stream dataStream)
    {
        var hashBytes = await SHA1.HashDataAsync(dataStream);
        var newChecksum = Convert.ToHexString(hashBytes);
        
        return newChecksum == checksum;
    }
}