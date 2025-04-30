using System.IO;
using Installer.ViewModel.Installation.ChecksumAlgorithms;

namespace Installer.ViewModel.Installation;

public class ChecksumVerifier
{
    private Dictionary<string, IChecksumAlgorithm> _algorithms = new();
        
    public ChecksumVerifier()
    {
        _algorithms.Add("MD5", new Md5Algorithm());
        _algorithms.Add("SHA1", new Sha1Algorithm());
        _algorithms.Add("SHA256", new Sha256Algorithm());
        _algorithms.Add("SHA512", new Sha512Algorithm());
    }

    public bool Verify(string filePath, string algorithmName, string checksum)
    {
        var foundAlgorithm = _algorithms.TryGetValue(algorithmName, out var algorithm);
        if (!foundAlgorithm || algorithm is null)
        {
            Console.WriteLine($"WARN: Could not find algorithm with name: '{algorithmName}'");
            return false;
        }
        
        var bytes = File.ReadAllBytes(filePath);
        Console.WriteLine("Read all bytes");
        
        return algorithm.VerifyChecksum(checksum, bytes);
    }
    public async Task<bool> VerifyAsync(string filePath, string algorithmName, string checksum)
    {
        var foundAlgorithm = _algorithms.TryGetValue(algorithmName, out var algorithm);
        if (!foundAlgorithm || algorithm is null)
        {
            Console.WriteLine($"WARN: Could not find algorithm with name: '{algorithmName}'");
            return false;
        }
        
        const int readChunkSize = 16_384;
        
        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, readChunkSize, true))
        {
            var before = DateTime.Now;
            var result = await algorithm.VerifyChecksumAsync(checksum, fs);
            var after = DateTime.Now;
            TimeSpan ts = after - before;
            Console.WriteLine("Time to verify: " + ts);
            return result;
        }
    }
}