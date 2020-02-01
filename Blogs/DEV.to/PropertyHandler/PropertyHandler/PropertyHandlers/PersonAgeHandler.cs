using RepoDb.Interfaces;

namespace PropertyHandler.PropertyHandlers
{
    public class PersonAgeHandler : IPropertyHandler<int, string>
    {
        public string Get(int input)
        {
            return $"{input} years old";
        }

        public int Set(string input)
        {
            return int.Parse(input.Replace(" years old", string.Empty));
        }
    }
}
