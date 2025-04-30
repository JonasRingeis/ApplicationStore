using System.IO;
using System.Security.Cryptography;

namespace Installer.ViewModel.Installation.ChecksumAlgorithms;

public class Md5AlgorithmWrapper : IHashAlgorithmWrapper
{
    public byte[] HashData(byte[] data) => MD5.HashData(data);
    public async Task<byte[]> HashDataAsync(Stream dataStream) => await MD5.HashDataAsync(dataStream);
}