using System.Security.Cryptography;
using System.Text;

namespace Company.Core.Helpers;

public static class HashHelper
{
    public static string GetHash(string pass) =>
        Encoding.Unicode.GetString(SHA512.HashData(Encoding.Unicode.GetBytes(pass)));
}
