// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace Course.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("resource_catalog"){Scopes = { "catalog_fullpermission"}},
                new ApiResource("resource_photo_stock"){Scopes = {"photo_stock_fullpermission"}},
                new ApiResource("resource_basket"){Scopes = {"basket_fullpermission"}},
                new ApiResource("resource_discount"){Scopes = {"discount_fullpermission"}},
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.Profile(),
                       new IdentityResources.OpenId(),
                       new IdentityResource(){Name = "roles",DisplayName = "Roles",Description ="User Roles",UserClaims =new[]{"role"}}
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
               new ApiScope("catalog_fullpermission","it's full access for catalog API "),
               new ApiScope("photo_stock_fullpermission","it's full access for photostock API "),
               new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
               new ApiScope("basket_fullpermission","it's provide to full access to basket"),
               new ApiScope("discount_fullpermission","it's full provide to full access to discounts")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientId = "WebClient",
                    ClientSecrets = new List<Secret>(){new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"catalog_fullpermission","photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName}
                },
                new Client()
                {
                ClientId = "WebClientForUser",
                ClientSecrets = new List<Secret>(){new Secret("secret".Sha256())},
                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = {"basket_fullpermission","discount_fullpermission",IdentityServerConstants.LocalApi.ScopeName,IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId
                    ,IdentityServerConstants.StandardScopes.OfflineAccess,IdentityServerConstants.StandardScopes.Profile,"roles"},
                AccessTokenLifetime = 1*60*60,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                RefreshTokenUsage = TokenUsage.ReUse
                }
                

            };

        
    }
}