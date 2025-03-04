﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ZeroCodeFramework
{
    public class LicenceCheck
    {
        private static string getChipperText(string chipperText)
        {
            int n = (int)chipperText[1];
            return chipperText.Substring((n - 64));
        }
        private static string getAppenderKey(string appender)
        {
            int n = (int)appender[0];
            return "122238690951" + appender.Substring(0, (n - 96));
        }

        internal static bool Decrypt(string cipherText)
        {
            var hostName = System.Environment.MachineName;
            string EncryptionKey = hostName + getAppenderKey(cipherText);
            cipherText = cipherText.Replace(" ", "+");
            cipherText = getChipperText(cipherText);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            var data = cipherText;

            var time = data.Substring(hostName.Length + 2, 8).ToDateTime("yyyymmdd", DateTimeKind.Utc);
            if (time > DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }




        }
    }

    public static class DateTimeExt
    {

        public static DateTime ToDateTime(this string dateTimeString, string dateTimeFormat, DateTimeKind dateTimeKind)
        {
            if (string.IsNullOrEmpty(dateTimeString))
            {
                return DateTime.MinValue;
            }

            DateTime dateTime;
            try
            {
                dateTime = DateTime.SpecifyKind(DateTime.ParseExact(dateTimeString, dateTimeFormat, CultureInfo.InvariantCulture), dateTimeKind);
            }
            catch (FormatException)
            {
                dateTime = DateTime.MinValue;
            }

            return dateTime;
        }
    }
}
