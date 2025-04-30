using System.IO;
using System.Security.Cryptography;

namespace Installer.ViewModel.Installation.ChecksumAlgorithms;

public class Sha1AlgorithmWrapper : IHashAlgorithmWrapper
{
    public byte[] HashData(byte[] data) => SHA1.HashData(data);
    public async Task<byte[]> HashDataAsync(Stream dataStream) => await SHA1.HashDataAsync(dataStream);
}