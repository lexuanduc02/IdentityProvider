using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using IdentityModel;

namespace IdentityProvider;

public class Config
{
    public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                //Recruiter Client
                new Client
                {
                    ClientId = "lms_student",
                    ClientSecrets = { new Secret("6BF43B235893BAF9687C3F84753E3".Sha256()) },

                    AllowedGrantTypes = { GrantType.AuthorizationCode, GrantType.ResourceOwnerPassword},

                    // where to redirect to after login
                    RedirectUris = {"https://localhost:7260/signin-oidc" },
                    

                    // where to redirect to after logout
                    PostLogoutRedirectUris = {"https://localhost:7260/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowAccessTokensViaBrowser = true,

                    AccessTokenLifetime = 3600, //1 hours

                    AbsoluteRefreshTokenLifetime = 2592000, //30 days

                    SlidingRefreshTokenLifetime = 1296000, //15 days

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.OpenId,
                        "lms_student"
                    }
                },

                //Admin Client
                new Client
                {
                    ClientId = "lms_teacher",
                    ClientSecrets = { new Secret("Enp23ZUd80hLjxlkeFZo1sdX3wAHJtNw".Sha256()) },

                    AllowedGrantTypes = { GrantType.AuthorizationCode, GrantType.ResourceOwnerPassword},

                    // where to redirect to after login
                    RedirectUris = {"https://localhost:7261/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:7261/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowAccessTokensViaBrowser = true,

                    AccessTokenLifetime = 3600, //1 hours

                    AbsoluteRefreshTokenLifetime = 2592000, //30 days

                    SlidingRefreshTokenLifetime = 1296000, //15 days

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.OpenId,
                        "lms_teacher"
                    }
                },

                //User Client
                new Client
                {
                    ClientId = "lms_admin",
                    ClientSecrets = { new Secret("dmAfzutnfoupYgRxs1fOk6IoTCrYL3Io".Sha256()) },

                    AllowedGrantTypes = { GrantType.AuthorizationCode, GrantType.ResourceOwnerPassword},

                    // where to redirect to after login
                    RedirectUris = {"https://localhost:7262/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = {"https://localhost:7262/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowAccessTokensViaBrowser = true,

                    AccessTokenLifetime = 3600, //1 hours

                    AbsoluteRefreshTokenLifetime = 2592000, //30 days

                    SlidingRefreshTokenLifetime = 1296000, //15 days

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.OpenId,
                        "lms_admin"
                    }
                },
            };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
                new ApiScope(name: "lms_admin", displayName: "LMS Admin API"),
                new ApiScope(name: "lms_student", displayName: "LMS Student API"),
                new ApiScope(name: "lms_teacher", displayName: "LMS Teacher API")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
                new IdentityResource(
                    name: "UserProfile",
                    userClaims: new[] { JwtClaimTypes.Id, JwtClaimTypes.Name, JwtClaimTypes.Email, JwtClaimTypes.PhoneNumber, JwtClaimTypes.Role },
                    displayName: "User profile data"),

                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
        };
}
