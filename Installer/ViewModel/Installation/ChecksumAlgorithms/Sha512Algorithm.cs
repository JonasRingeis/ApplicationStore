using System.IO;
using System.Security.Cryptography;

namespace Installer.ViewModel.Installation.ChecksumAlgorithms;

public class Sha512Algorithm : IChecksumAlgorithm
{
    public bool VerifyChecksum(string checksum, byte[] data)
    {
        var hashBytes = SHA512.HashData(data);
        var newChecksum = Convert.ToHexString(hashBytes);
        
        return newChecksum == checksum;
    }
    
    public async Task<bool> VerifyChecksumAsync(string checksum, Stream dataStream)
    {
        var hashBytes = await SHA512.HashDataAsync(dataStream);
        Console.WriteLine("Hashed input");
        var newChecksum = Convert.ToHexString(hashBytes);
        
        return newChecksum == checksum;
    }
}