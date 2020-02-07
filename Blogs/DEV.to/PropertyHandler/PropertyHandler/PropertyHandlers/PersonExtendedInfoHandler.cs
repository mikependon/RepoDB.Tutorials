using Newtonsoft.Json;
using PropertyHandler.Models;
using RepoDb;
using RepoDb.Interfaces;

namespace PropertyHandler.PropertyHandlers
{
    public class PersonExtendedInfoHandler : IPropertyHandler<string, PersonExtendedInfo>
    {
        public PersonExtendedInfo Get(string input,
            ClassProperty property)
        {
            return JsonConvert.DeserializeObject<PersonExtendedInfo>(input);
        }

        public string Set(PersonExtendedInfo input,
            ClassProperty property)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}
