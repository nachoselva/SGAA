namespace SGAA.Utils
{
    using System.Linq;

    public static class EnumTools
    {
        public static IEnumerable<EnumOption<T>> GetOptions<T>() where T : struct, Enum
        {
            return Enum.GetValues<T>().Select(e => new EnumOption<T>(Convert.ToInt32(e), e, Enum.GetName(e)!));
        }
    }

    public class EnumOption<T> where T : struct, Enum
    {
        public EnumOption(int id, T option, string name)
        {
            Id = id;
            Option = option;
            Name = name;
        }

        public int Id { get; set; }
        public T Option { get; set; }
        public string Name { get; set; }

    }
}