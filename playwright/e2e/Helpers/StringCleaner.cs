

namespace e2e.helpers
{
    public static class StringCleaner
    {
        public static string GetCleanName(string fullText)
        {
            string[] parts = fullText.Split('(')[0]
                            .Split(',');
            string name = parts[1].Trim() + " " + parts[0].Trim();

            return name;
        }
    }
}