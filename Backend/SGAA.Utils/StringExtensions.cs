namespace SGAA.Utils
{
    using System;

    public static class StringExtensions
    {
        private const string ALLOWED_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GenerateRandomString(int length)
        {
            int totalChars = ALLOWED_CHARS.Length;
            Random random = new();
            return new string(Enumerable.Range(0, length).Select(r => ALLOWED_CHARS[random.Next(0, totalChars)]).ToArray());
        }
    }
}
