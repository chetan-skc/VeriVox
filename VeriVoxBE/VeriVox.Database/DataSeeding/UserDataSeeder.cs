using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Database.DataSeeding
{
    public static class UserDataSeeder
    {
        public static void UserDataSeed(this ModelBuilder modelBuilder)
        {
            var users = new List<User>
            {
                new User
                 {
                     Id = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     FirstName = "Rajendra",
                     LastName = "Patel",
                     EmailId = "rajendra.patel@qburst.com",
                     Password = "/Dn3WgjF47dAXRh6I1oUJmtN/N5tozR9vaX4duVFdAHsIvtPGmj7g7Zpfs3fQQUHzSZtZNtJqFL860A3tuuKkg==",
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy =  Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     CreatedDate = DateTime.UtcNow,
                     ModifiedBy =  Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     ModifiedDate = DateTime.UtcNow

                 },
                 new User
                 {
                     Id = Guid.Parse("8824B12B-2061-44A6-904A-413FA1BA806E"),
                     FirstName = "Anurag",
                     LastName = "Kumar",
                     EmailId = "anurag.kumar@qburst.com",
                     Password = "0Z4kiKSItbQnLiprARI1bHnhpJppsNfpF65TscmX75lnIaLKW1eazfTL01UOuCghlwxhvLQ8C7cCc7sXaffznQ==",
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy = Guid.Parse("8824B12B-2061-44A6-904A-413FA1BA806E"),
                     CreatedDate = DateTime.UtcNow,
                     ModifiedBy = Guid.Parse("8824B12B-2061-44A6-904A-413FA1BA806E"),
                     ModifiedDate = DateTime.UtcNow

                 },
                 new User
                 {
                     Id = Guid.Parse("26873A44-C003-47E9-A7EC-EEAC3CC23A76"),
                     FirstName = "Chetan",
                     LastName = "Goyal",
                     EmailId = "chetan.goyal@qburst.com",
                     Password = "zyp5XjqIMoMUnP6Qe6VMmuVVfpMlXkeK8icVyXfA5jPC3YLYEbZyvLRkqd76tF6ZUtnc62YdnhXcOutrjk6cEg==",
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy = Guid.Parse("26873A44-C003-47E9-A7EC-EEAC3CC23A76"),
                     CreatedDate = DateTime.UtcNow,
                     ModifiedBy = Guid.Parse("26873A44-C003-47E9-A7EC-EEAC3CC23A76"),
                     ModifiedDate = DateTime.UtcNow

                 },
                 new User
                 {
                     Id = Guid.Parse("8F0B777A-3D51-4C3F-BFCB-C6F6A1CCF474"),
                     FirstName = "Aman",
                     LastName = "Pandey",
                     EmailId = "aman.pandey@qburst.com",
                     Password = "+wdd+tJJYRLSvfMeEz65Y+Yk8pdkDOTmZP6kCryzdZVxbPq0W5sIPvSozgQOKAXxT1M9FGx/ao2zcets5Hkjug==",
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy = Guid.Parse("8F0B777A-3D51-4C3F-BFCB-C6F6A1CCF474"),
                     CreatedDate = DateTime.UtcNow,
                     ModifiedBy = Guid.Parse("8F0B777A-3D51-4C3F-BFCB-C6F6A1CCF474"),
                     ModifiedDate = DateTime.UtcNow

                 },
                 new User
                 {
                     Id = Guid.Parse("D753378F-0D34-432F-052A-08DBC57806A8"),
                     FirstName = "Deva",
                     LastName = "Raj",
                     EmailId = "deva@qburst.com",
                     Password = "viawTnXYTYW5c3Fb/IJXH9vf/bHi/vboXIfyAzJqfDTqfXBoL6V5Uu5aECbz7UucyEx7y1I4Zd5vmOEQQkEvRw==",
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy =   Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     CreatedDate = DateTime.UtcNow,
                     ModifiedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     ModifiedDate = DateTime.UtcNow

                 },
                 new User
                 {
                     Id = Guid.Parse("4FC6C89D-8050-4B98-052B-08DBC57806A8"),
                     FirstName = "Abhi",
                     LastName = "Raj",
                     EmailId = "abhi@qburst.com",
                     Password = "fGDytmtc9n5s9dxBqlBtyM7KhAvW4KIV81GvZ6pbc0uCRxZAJVYQnUXuxYpPE3i7JWOyLITArhc8RXQh3n8w/Q==",
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy =  Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     CreatedDate = DateTime.UtcNow,
                     ModifiedBy =  Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     ModifiedDate = DateTime.UtcNow

                 },
                 new User
                 {
                     Id = Guid.Parse("9A8716C2-111A-4387-052C-08DBC57806A8"),
                     FirstName = "Ayush",
                     LastName = "Agrawal",
                     EmailId = "ayush@qburst.com",
                     Password = "wo2dBCiSbKRd9rpDSARI+XT/ij5Lj+CSz/oVPEUU8eXIkicxx7X9k/den+bCMSusZSOotVPzcRfiVwGw88UQWA==",
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy =  Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     CreatedDate = DateTime.UtcNow,
                     ModifiedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     ModifiedDate = DateTime.UtcNow

                 },
                 new User
                 {
                     Id = Guid.Parse("67889B9B-4DAF-4DFB-052D-08DBC57806A8"),
                     FirstName = "Sunil",
                     LastName = "Nagar",
                     EmailId = "sunil@qburst.com",
                     Password = "1XVIUwLrlL/7sNQV5uSiWjfKSZPoMu23vA4Kp42N1pkCGYBgadc0kdwMYOQTDMe1oHM4udK5i+zghR2GDsqk7Q==",
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy = Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     CreatedDate = DateTime.UtcNow,
                     ModifiedBy =  Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     ModifiedDate = DateTime.UtcNow

                 },
                 new User
                 {
                     Id = Guid.Parse("B2BF5561-25B7-4A99-052E-08DBC57806A8"),
                     FirstName = "Ritik",
                     LastName = "Kumar",
                     EmailId = "ritik@qburst.com",
                     Password = "8gISX060w6J3e94azqVl/V96v80bU8fYC71d8z88AKbDkH51QiyxBCFuE0ss7WBy8EG7NQjmEQVfzid3wkW24Q==",
                     IsActive = true,
                     IsDeleted = false,
                     CreatedBy =  Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     CreatedDate = DateTime.UtcNow,
                     ModifiedBy =  Guid.Parse("713C6A4B-DDDF-4266-B525-08DBB34B621D"),
                     ModifiedDate = DateTime.UtcNow

                 }

            };

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
