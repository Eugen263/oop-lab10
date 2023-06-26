using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Method 1: RC2 encryption
            await Task.Run(() =>
            {
                using (var rc2 = new RC2CryptoServiceProvider())
                {
                    var data = Encoding.UTF8.GetBytes("Hello, world!");
                    var key = rc2.Key;
                    var iv = rc2.IV;

                    var encryptedData = rc2.CreateEncryptor(key, iv).TransformFinalBlock(data, 0, data.Length);
                    var decryptedData = rc2.CreateDecryptor(key, iv).TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                    var decryptedString = Encoding.UTF8.GetString(decryptedData);
                    Console.WriteLine("RC2: {0}", decryptedString);
                }
            });
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // Method 2: MDC-2 hashing
            await Task.Run(() =>
            {
                using (var mdc2 = new MDC2CryptoServiceProvider())
                {
                    var data = Encoding.UTF8.GetBytes("Hello, world!");
                    var hash = mdc2.ComputeHash(data);

                    var hashString = Convert.ToBase64String(hash);
                    Console.WriteLine("MDC-2: {0}", hashString);
                }
            });
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // Method 3: ESIGN signature
            await Task.Run(() =>
            {
                using (var eSign = new ECDsaCng())
                {
                    var data = Encoding.UTF8.GetBytes("Hello, world!");
                    var signature = eSign.SignData(data);

                    var verified = eSign.VerifyData(data, signature);
                    Console.WriteLine("ESIGN verified: {0}", verified);
                }
            });
        }
    }
}
