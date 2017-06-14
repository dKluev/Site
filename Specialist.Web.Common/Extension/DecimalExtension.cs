namespace SimpleUtils.Extension
{
    public static class DecimalExtension
    {
        public static string ToIntString(this decimal d)
        {
            
            return d.ToString("n0");
        }
    }
}