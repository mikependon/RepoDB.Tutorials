using Newtonsoft.Json;
using PropertyHandler.Models;
using RepoDb.Interfaces;

namespace PropertyHandler.PropertyHandlers
{
    public class PersonExtendedInfoHandler : IPropertyHandler<string, PersonExtendedInfo>
    {
        public PersonExtendedInfo Get(string input)
        {
            return JsonConvert.DeserializeObject<PersonExtendedInfo>(input);
        }

        public string Set(PersonExtendedInfo input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}
