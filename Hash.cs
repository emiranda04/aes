using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

namespace Cipher {

    class Hash {

        public static Dictionary<int, string> HashTypes() {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "SHA1");
            dictionary.Add(2, "SHA256");
            dictionary.Add(3, "SHA512");
            return dictionary;
        }

        public static byte[] GetFileSHA512(string file) {
            using (var sha = SHA512.Create()) {
                using (var fileStream = File.OpenRead(file)) {
                    return sha.ComputeHash(fileStream);
                }
            }
        }

        public static byte[] GetFileSHA256(string file) {
            using (var sha = SHA256.Create()) {
                using (var fileStream = File.OpenRead(file)) {
                    return sha.ComputeHash(fileStream);
                }
            }
        }

        public static byte[] GetFileSHA1(string file) {
            using (var sha = SHA1.Create()) {
                using (var fileStream = File.OpenRead(file)) {
                    return sha.ComputeHash(fileStream);
                }
            }
        }
    }
}
