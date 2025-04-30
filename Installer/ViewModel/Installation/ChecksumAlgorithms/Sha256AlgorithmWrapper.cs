using System.IO;
using System.Security.Cryptography;

namespace Installer.ViewModel.Installation.ChecksumAlgorithms;

public class Sha256AlgorithmWrapper : IHashAlgorithmWrapper
{
    public byte[] HashData(byte[] data) => SHA256.HashData(data);
    public async Task<byte[]> HashDataAsync(Stream dataStream) => await SHA256.HashDataAsync(dataStream);
}