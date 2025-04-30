using System.IO;

namespace Installer.ViewModel.Installation.ChecksumAlgorithms;

public interface IHashAlgorithmWrapper
{
    public byte[] HashData(byte[] data);
    public Task<byte[]> HashDataAsync(Stream dataStream);
}