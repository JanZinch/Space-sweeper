using SimpleSQL;

namespace CodeBase.ApplicationLibrary.Data
{
    public class PrefLong
    {
        [PrimaryKey] public string PrefName { get; set; }

        public long Value { get; set; }
    }
}