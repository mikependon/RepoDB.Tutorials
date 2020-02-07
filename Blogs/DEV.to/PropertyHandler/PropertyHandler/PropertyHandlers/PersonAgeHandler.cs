using RepoDb;
using RepoDb.Interfaces;

namespace PropertyHandler.PropertyHandlers
{
    public class PersonAgeHandler : IPropertyHandler<int, string>
    {
        public string Get(int input,
            ClassProperty property)
        {
            return $"{input} years old";
        }

        public int Set(string input,
            ClassProperty property)
        {
            return int.Parse(input.Replace(" years old", string.Empty));
        }
    }
}
