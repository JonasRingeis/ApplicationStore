using System.IO;
using System.Security.Cryptography;

namespace Installer.ViewModel.Installation.ChecksumAlgorithms;

public class Sha512AlgorithmWrapper : IHashAlgorithmWrapper
{
    public byte[] HashData(byte[] data) => SHA512.HashData(data);
    public async Task<byte[]> HashDataAsync(Stream dataStream) => await SHA512.HashDataAsync(dataStream);
}