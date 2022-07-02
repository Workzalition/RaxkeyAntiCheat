using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;
using System.Threading.Tasks;
using PInvoke;

namespace RaxkeyAntiCheat.HWID
{
    public class RAC
    {
        public static string CheckMD5Hash(string filename)
        {
            using(MD5 md5 = MD5.Create())
            {
                    FileStream file = File.OpenRead(filename);
                    file.Position = 0;
                    byte[] hash = md5.ComputeHash(file);
                    StringBuilder string44 = new StringBuilder();
                    for(int o = 0; o < hash.Length; o++)
                    {
                        string44.Append(hash[o].ToString());
                    }
                    file.Close();

                    return string44.ToString().ToLower();
            }
        }
        private static void CreateRaxKeyDirectory()
        {
            Directory.CreateDirectory(@"C:\Program Files\RaxKey Anti Cheat");
        }
        public static void GetCurrentPCHWID(string reghwid, string current_registry)
        {
            CreateRaxKeyDirectory();
            if(Directory.Exists(@"C:\Program Files\RaxKey Anti Cheat"))
            {
                var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                var keyopen = key.OpenSubKey(reghwid, true);
                var x = keyopen.GetValue(current_registry);
                File.WriteAllText(@"C:\Program Files\RaxKey Anti Cheat\HWID.txt", "HWID PC: " + Environment.NewLine + x.ToString());
            }
        }
    }
}
