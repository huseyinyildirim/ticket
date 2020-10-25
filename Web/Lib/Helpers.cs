using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.IO;

using Web.Controllers;

namespace Web.Lib
{
    public static class Helpers
    {
        public static string ToSeoUrl(this string url)
        {
            string cleanUrl = (url ?? "").ToLower();
            cleanUrl = Regex.Replace(cleanUrl, @"\&+", "and");
            cleanUrl = cleanUrl.Replace("'", "");
            cleanUrl = Regex.Replace(cleanUrl, @"[^a-z0-9ğüşöçıİĞÜŞÖÇ]", "-");
            cleanUrl = Regex.Replace(cleanUrl, @"-+", "-");
            cleanUrl = cleanUrl.Trim('-');
            cleanUrl = cleanUrl.Replace("[", "");
            cleanUrl = cleanUrl.Replace("]", "");
            cleanUrl = cleanUrl.Replace("(", "");
            cleanUrl = cleanUrl.Replace(")", "");
            cleanUrl = cleanUrl.Replace("ğ", "g");
            cleanUrl = cleanUrl.Replace("ü", "u");
            cleanUrl = cleanUrl.Replace("ö", "o");
            cleanUrl = cleanUrl.Replace("ş", "s");
            cleanUrl = cleanUrl.Replace("ç", "c");
            cleanUrl = cleanUrl.Replace("ı", "i");
            return cleanUrl;
        }

        public static string ToMD5(this string str)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(str);
            byte[] hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string ToCleanString(this string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            value = value.Replace("&Uuml;", "Ü");
            value = value.Replace("&Ccedil;", "Ç");
            value = value.Replace("&Ouml;", "Ö");
            value = value.Replace("&uuml;", "ü");
            value = value.Replace("&ccedil;", "ç");
            value = value.Replace("&ouml;", "ö");

            value = value.Replace("Ãœ", "Ü");
            value = value.Replace("Å", "Ş");
            value = value.Replace("Ä", "Ğ");
            value = value.Replace("Ã‡", "Ç");
            value = value.Replace("Ä°", "İ");
            value = value.Replace("Ã–", "Ö");
            value = value.Replace("Ã¼", "ü");
            value = value.Replace("ÅŸ", "ş");
            value = value.Replace("ÄŸ", "ğ");
            value = value.Replace("Ã§", "ç");
            value = value.Replace("Ä±", "ı");
            value = value.Replace("Ã¶", "ö");
            value = value.Replace("Ã¢", "â");
            return value;
        }

        public static bool EmailIsValid(this string q)
        {
            return Regex.IsMatch(q, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool TcDogrula(this string tcKimlikNo)
        {
            bool returnvalue = false;

            if (string.IsNullOrEmpty(tcKimlikNo)) return false;

            try
            {
                Convert.ToInt64(tcKimlikNo);
            }
            catch
            {
                return false;
            }

            if (tcKimlikNo.Length == 11)
            {
                Int64 ATCNO, BTCNO, TcNo;
                long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

                TcNo = Int64.Parse(tcKimlikNo);

                ATCNO = TcNo / 100;
                BTCNO = TcNo / 100;

                C1 = ATCNO % 10; ATCNO = ATCNO / 10;
                C2 = ATCNO % 10; ATCNO = ATCNO / 10;
                C3 = ATCNO % 10; ATCNO = ATCNO / 10;
                C4 = ATCNO % 10; ATCNO = ATCNO / 10;
                C5 = ATCNO % 10; ATCNO = ATCNO / 10;
                C6 = ATCNO % 10; ATCNO = ATCNO / 10;
                C7 = ATCNO % 10; ATCNO = ATCNO / 10;
                C8 = ATCNO % 10; ATCNO = ATCNO / 10;
                C9 = ATCNO % 10; ATCNO = ATCNO / 10;
                Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                returnvalue = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
            }
            return returnvalue;
        }

        public static string ToPhoneClear(this string phone)
        {
            if (string.IsNullOrEmpty(phone)) return phone;

            phone = phone.Replace("-", "");
            phone = phone.Replace("_", "");
            phone = phone.Replace("(", "");
            phone = phone.Replace(")", "");
            return phone;
        }

        public static string ToFileFormatString(this string value)
        {
            value = value.ToSeoUrl();

            var match = Regex.Match(value, @"(-(jpg|jpeg|png|gif|bmp|txt|doc|docx|xls|xlsx|ppt|pptx|pps|ppsx|zip|rar|pdf))", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                var fileExtension = match.Groups[0].Value;
                var cacheExt = match.Groups[0].Value.Replace("-", ".");
                value = value.Replace(fileExtension, cacheExt);
            }

            return String.Format("{0:ddMMyyyy_HHmmss}", DateTime.Now) + "_" + value;
        }
        public static bool isPermit(this int point)
        {
            return new CPBaseController().PERMIT(point);
        }

        public static string Encrypt(this string q)
        {
            return _Encrypt(q, "Pa34sdfpr@se", "s@cvt&5hh", "SHA1", 2, "@1B2c3D4e5F6g7H8", 256);
        }

        public static string Decrypt(this string q)
        {
            return _Decrypt(q, "Pa34sdfpr@se", "s@cvt&5hh", "SHA1", 2, "@1B2c3D4e5F6g7H8", 256);
        }

        public static string GetIP(this string q)
        {
            var ip = "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                ip = HttpContext.Current.Request.UserHostAddress;
            }
            return ip;
        }
        public static string GetUserAgent(this string q)
        {
            return HttpContext.Current.Request.UserAgent;
        }

        public static IHtmlString MyGrid(this HtmlHelper htmlHelper, System.Collections.Generic.IEnumerable<dynamic> Model, System.Collections.Generic.IEnumerable<WebGridColumn> Columns, string divId, int rowsPerPage = 15)
        {
            var grid = new WebGrid(Model, defaultSort: "NAME", ajaxUpdateContainerId: divId, rowsPerPage: rowsPerPage);
            return grid.GetHtml("table", columns: Columns, nextText: "İleri", previousText: "Geri", firstText: "İlk", lastText: "Son", mode: WebGridPagerModes.All, selectedRowStyle:"selected");
        }

        #region CRYPT TOOL

        private static string _Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            plainText = plainText.Trim();
            var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            var keyBytes = password.GetBytes(keySize / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            var cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }
        private static string _Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            cipherText = cipherText.Trim();
            var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            var cipherTextBytes = Convert.FromBase64String(cipherText);
            var password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            var keyBytes = password.GetBytes(keySize / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
            var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            var plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            return plainText;
        }

        #endregion
    }


}