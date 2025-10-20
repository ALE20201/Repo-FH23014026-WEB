using System;
using System.Text;

namespace BinaryOperationsApp.Services
{
    public static class BinaryOps
    {
        private static (string left, string right) AlignRight(string a, string b)
        {
            int max = Math.Max(a?.Length ?? 0, b?.Length ?? 0);
            string A = (a ?? "").PadLeft(max, '0');
            string B = (b ?? "").PadLeft(max, '0');
            return (A, B);
        }

        public static string And(string a, string b)
        {
            var (A, B) = AlignRight(a, b);
            if (string.IsNullOrEmpty(A) && string.IsNullOrEmpty(B)) return "0";
            var sb = new StringBuilder();
            for (int i = 0; i < A.Length; i++)
                sb.Append((A[i] == '1' && B[i] == '1') ? '1' : '0');
            return TrimLeadingZeros(sb.ToString());
        }

        public static string Or(string a, string b)
        {
            var (A, B) = AlignRight(a, b);
            if (string.IsNullOrEmpty(A) && string.IsNullOrEmpty(B)) return "0";
            var sb = new StringBuilder();
            for (int i = 0; i < A.Length; i++)
                sb.Append((A[i] == '1' || B[i] == '1') ? '1' : '0');
            return TrimLeadingZeros(sb.ToString());
        }

        public static string Xor(string a, string b)
        {
            var (A, B) = AlignRight(a, b);
            if (string.IsNullOrEmpty(A) && string.IsNullOrEmpty(B)) return "0";
            var sb = new StringBuilder();
            for (int i = 0; i < A.Length; i++)
                sb.Append((A[i] != B[i]) ? '1' : '0');
            return TrimLeadingZeros(sb.ToString());
        }

        public static string TrimLeadingZeros(string s)
        {
            if (string.IsNullOrEmpty(s)) return "0";
            int i = 0;
            while (i < s.Length - 1 && s[i] == '0') i++;
            var r = s.Substring(i);
            return string.IsNullOrEmpty(r) ? "0" : r;
        }

        //  devuelve 0 si la cadena no es parseable como binario.
        public static int BinToInt(string? bin)
        {
            if (string.IsNullOrWhiteSpace(bin)) return 0;
            bin = bin.Trim();
            // Validar que sólo tenga 0/1
            foreach (char c in bin)
            {
                if (c != '0' && c != '1') return 0;
            }
            try
            {
                return Convert.ToInt32(bin, 2);
            }
            catch
            {
                return 0; // fallback seguro
            }
        }

        public static string IntToBin(int value) => Convert.ToString(value, 2);
        public static string IntToOct(int value) => Convert.ToString(value, 8);
        public static string IntToHex(int value) => Convert.ToString(value, 16).ToUpper();
    }
}
