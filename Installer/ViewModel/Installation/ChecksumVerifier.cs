using System.IO;
using Installer.ViewModel.Installation.ChecksumAlgorithms;

namespace Installer.ViewModel.Installation;

public class ChecksumVerifier
{
    private Dictionary<string, IHashAlgorithmWrapper> _algorithmWrappers = new();
    
    public ChecksumVerifier()
    {
        _algorithmWrappers.Add("MD5", new Md5AlgorithmWrapper());
        _algorithmWrappers.Add("SHA1", new Sha1AlgorithmWrapper());
        _algorithmWrappers.Add("SHA256", new Sha256AlgorithmWrapper());
        _algorithmWrappers.Add("SHA512", new Sha512AlgorithmWrapper());
    }

    public bool Verify(string filePath, string algorithmName, string checksum)
    {
        var foundAlgorithm = _algorithmWrappers.TryGetValue(algorithmName, out var algorithm);
        if (!foundAlgorithm || algorithm is null)
        {
            Console.WriteLine($"WARN: Could not find algorithm with name: '{algorithmName}'");
            return false;
        }
        
        var bytes = File.ReadAllBytes(filePath);
        
        var hash = algorithm.HashData(bytes);
        var newChecksum = Convert.ToHexString(hash);
        return checksum.Equals(newChecksum, StringComparison.OrdinalIgnoreCase);
    }
    public async Task<bool> VerifyAsync(string filePath, string algorithmName, string checksum)
    {
        var foundAlgorithm = _algorithmWrappers.TryGetValue(algorithmName, out var algorithm);
        if (!foundAlgorithm || algorithm is null)
        {
            Console.WriteLine($"WARN: Could not find algorithm with name: '{algorithmName}'");
            return false;
        }
        
        const int readChunkSize = 16_384;
        
        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, readChunkSize, true))
        {
            var hash = await algorithm.HashDataAsync(fs);
            var newChecksum = Convert.ToHexString(hash);
            return checksum.Equals(newChecksum, StringComparison.OrdinalIgnoreCase);
        }
    }
}