namespace PST.Models
{
    public class Language
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Language() { }

        public Language(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}