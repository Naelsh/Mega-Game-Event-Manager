using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer.Config;

public class IdConfig
{
    // testusers
    public static List<TestUser> testUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                Username = "Test1234",
                Password = "1234Test",
                SubjectId = "11",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, " Erika")
                }
            }
        };

    public static IEnumerable<IdentityResource> identityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> apiScopes =>
        new ApiScope[]
        {
            new ApiScope("MegagameAPI.write"),
            new ApiScope("MegagameAPI.read")
        };

    public static IEnumerable<ApiResource> apiResources =>
        new ApiResource[]
        {
            new ApiResource("MegagameAPI")
            {
                Scopes = new List<string> { "MegagameAPI.write", "MegagameAPI.read"},
                ApiSecrets = new List<Secret> { new Secret("9123i0lgmal19elmadfioj91ngaöjh".Sha256())}
            }
        };

    public static IEnumerable<Client> clients =>
        new Client[]
        {
            new Client()
            {
                ClientName = "MegagameFrontend",
                ClientId = "1337",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> { new Secret("jfa091§jfmlaf99af".Sha256())},
                AllowedScopes = new List<string> { "MegagameAPI.write", "MegagameAPI.read" },
            }
        };
 
}
