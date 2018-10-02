using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ng_oidc_client_server {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);
            services.AddIdentityServer ()
                .AddInMemoryClients (Clients)
                .AddInMemoryIdentityResources (IdentityResources)
                .AddInMemoryApiResources (ApiResources)
                .AddTestUsers (TestUsers)
                .AddDeveloperSigningCredential ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseMvc ();
            app.UseIdentityServer ();
            app.UseStaticFiles ();
            app.UseMvcWithDefaultRoute ();

        }

        public static List<TestUser> TestUsers =
            new List<TestUser> {
                new TestUser {
                SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                Username = "ngoidcclient",
                Password = "password",
                Claims = new List<Claim> {
                new Claim (JwtClaimTypes.Email, "ng-oidc-client@gmail.com"),
                new Claim (JwtClaimTypes.Role, "admin")
                }
                }
            };

        public static List<Client> Clients = new List<Client> {
            new Client {
            ClientId = "ng-oidc-client-identity",
            ClientName = "Example client for ng-oidc-client and IdentityServer 4",
            AllowedGrantTypes = GrantTypes.Implicit,
            AllowedScopes = new List<string> {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email,

            "role",
            "customAPI.write"
            },
            AllowedCorsOrigins = new List<string> { "http://localhost:4200" },
            RedirectUris = new List<string> { "http://localhost:4200/callback.html" },
            PostLogoutRedirectUris = new List<string> { "http://localhost:4200/singout-callback.html" },
            AllowAccessTokensViaBrowser = true,
            AllowOfflineAccess = true
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource> {
            new IdentityResources.OpenId (),
            new IdentityResources.Profile (),
            new IdentityResources.Email (),
            new IdentityResource {
            Name = "role",
            UserClaims = new List<string> { "role" }
            }
        };

        public static IEnumerable<ApiResource> ApiResources = new List<ApiResource> {
            new ApiResource {
            Name = "customAPI",
            DisplayName = "Custom API",
            Description = "Custom API Access",
            UserClaims = new List<string> { "role" },
            ApiSecrets = new List<Secret> { new Secret ("scopeSecret".Sha256 ()) },
            Scopes = new List<Scope> {
            new Scope ("customAPI.read"),
            new Scope ("customAPI.write")
            }
            }
        };

    }

}