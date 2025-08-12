namespace e2e.helpers
{
    public static class Randomizer
    {
        public static string GetRandomAlphaNumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            return new string([.. Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)])]);

        }

        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
        
    }
}