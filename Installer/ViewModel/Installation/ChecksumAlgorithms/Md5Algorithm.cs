using System.IO;
using System.Security.Cryptography;

namespace Installer.ViewModel.Installation.ChecksumAlgorithms;

public class Md5Algorithm : IChecksumAlgorithm
{
    public bool VerifyChecksum(string checksum, byte[] data)
    {
        var hashBytes = MD5.HashData(data);
        var newChecksum = Convert.ToHexString(hashBytes);
        
        return newChecksum == checksum;
    }

    public async Task<bool> VerifyChecksumAsync(string checksum, Stream dataStream)
    {
        var bytes = await MD5.HashDataAsync(dataStream);
        var newChecksum = Convert.ToHexString(bytes);
        
        return newChecksum == checksum;
    }
}