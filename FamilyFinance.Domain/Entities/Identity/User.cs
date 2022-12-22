using System.Text.Json.Serialization;
using FamilyFinance.Domain.Entities.Addition;

namespace FamilyFinance.Domain.Entities.Identity
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
