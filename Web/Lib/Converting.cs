using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace Web.Lib
{
    public class Converting
    {

        public static string GetString(object input)
        {
            if (input == null)
            {
                return "";
            }
            try
            {
                return (input.ToString().ToCleanString().Trim());
            }
            catch
            {
                return "";
            }
        }

        public static bool GetBoolean(object input)
        {
            try { return (Convert.ToBoolean(input)); }
            catch { return false; }
        }

        public static bool? GetBoolean(object input, bool forNullable)
        {
            bool? x;

            if (input == "")
            {
                x = null;
            }
            else
            {
                try { x = Convert.ToBoolean(Int32.Parse(input.ToString())); }
                catch { x = false; }
            }

            return x;
        }

        public static Int16 GetInt16(object input)
        {
            try { return Int16.Parse(input.ToString()); }
            catch { return 0; }
        }

        public static Int16? GetInt16(object input, bool forNullable)
        {
            Int16? x;

            try { x = Int16.Parse(input.ToString()); }
            catch { x = null; }

            return forNullable ? (x == 0 ? null : x) : (x ?? 0);
        }

        public static Int32 GetInt32(object input)
        {
            try { return Int32.Parse(input.ToString()); }
            catch { return 0; }
        }

        public static Int32? GetInt32(object input, bool forNullable)
        {
            int? x;

            try { x = Int32.Parse(input.ToString()); }
            catch { x = null; }

            return forNullable ? (x == 0 ? null : x) : (x ?? 0);
        }

        public static Int64 GetInt64(object input)
        {
            try { return Int64.Parse(input.ToString()); }
            catch { return 0; }
        }

        public static Int64? GetInt64(object input, bool forNullable)
        {
            long? x;

            try { x = Int64.Parse(input.ToString()); }
            catch { x = null; }

            return forNullable ? (x == 0 ? null : x) : (x ?? 0);
        }

        public static Double GetDouble(object input)
        {
            try { return Convert.ToDouble(input.ToString()); }
            catch { return 0; }
        }

        public static DateTime? GetDateTime(object input)
        {
            try { return (DateTime.Parse(input.ToString())); }
            catch { return null; }
        }

        public static DateTime GetDateTime(object input, bool forNullable)
        {
            try { return (DateTime.Parse(input.ToString())); }
            catch { return new DateTime(0,0,0,0,0,0); }
        }

        public static Decimal GetDecimal(object input)
        {
            try { return Decimal.Parse(input.ToString().Replace('.', ',')); }
            catch { return 0; }
        }

        public static bool IsEmail(string inputEmail)
        {
            if (string.IsNullOrEmpty(inputEmail)) return false;
            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var re = new Regex(strRegex);
            return re.IsMatch(inputEmail);
        }

        public static bool TcDogrula(string tcKimlikNo)
        {
            var returnvalue = false;

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

        public static string GetArg(string query, string key)
        {
            var x = Regex.Match(query, key + "=([^&#]*)");
            return x.Groups[1].Value;
        }

   
    }
}