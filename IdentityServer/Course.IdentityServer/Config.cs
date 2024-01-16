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
                new ApiResource("resource_order"){Scopes ={"order_fullpermission" }},
                new ApiResource("resource_payment"){Scopes = {"payment_fullpermission"}},
                new ApiResource("resource_gateway"){Scopes = {"gateway_fullpermission"}},
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
               new ApiScope("catalog_fullpermission","it provides to full access for catalog API "),
               new ApiScope("photo_stock_fullpermission","it provides to full access for photostock API "),
               new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
               new ApiScope("basket_fullpermission","it provides to full access to basket API"),
               new ApiScope("discount_fullpermission","it provides to full access to discount API"),
               new ApiScope("order_fullpermission","it provides to full access to order API"),
               new ApiScope("payment_fullpermission","it provides to full access on payment API"),
               new ApiScope("gateway_fullpermission","it provides to full access on Gateway")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientId = "WebClient",
                    ClientSecrets = new List<Secret>(){new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"catalog_fullpermission","photo_stock_fullpermission", "gateway_fullpermission", IdentityServerConstants.LocalApi.ScopeName}
                },
                new Client()
                {
                ClientId = "WebClientForUser",
                ClientSecrets = new List<Secret>(){new Secret("secret".Sha256())},
                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = {"catalog_fullpermission","gateway_fullpermission","payment_fullpermission","order_fullpermission","basket_fullpermission","discount_fullpermission",IdentityServerConstants.LocalApi.ScopeName,IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId
                    ,IdentityServerConstants.StandardScopes.OfflineAccess,IdentityServerConstants.StandardScopes.Profile,"roles"},
                AccessTokenLifetime = 1*60*60,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(1)-DateTime.Now).TotalSeconds,
                RefreshTokenUsage = TokenUsage.OneTimeOnly
                }
                

            };

        
    }
}