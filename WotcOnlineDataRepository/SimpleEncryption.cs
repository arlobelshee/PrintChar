using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;

namespace WotcOnlineDataRepository
{
	internal class SimpleEncryption
	{
		[NotNull]
		public static byte[] Encrypt([NotNull] string plainText, string key)
		{
			var transform = _GetSimpleAlgorithm(key).CreateEncryptor();
			using (var cipherText = new MemoryStream())
			{
				using (var encryptor = new CryptoStream(cipherText, transform, CryptoStreamMode.Write))
				{
					var bytes = Encoding.UTF8.GetBytes(plainText);
					encryptor.Write(bytes, 0, bytes.Length);
					encryptor.Flush();
					encryptor.FlushFinalBlock();
					cipherText.Position = 0L;
					return cipherText.ToArray();
				}
			}
		}

		private static SymmetricAlgorithm _GetSimpleAlgorithm(string key)
		{
			var aes = new AesManaged();
			var source = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(key));
			return new AesManaged {Key = source, IV = source.Take((aes.BlockSize/8)).ToArray()};
		}
	}
}
