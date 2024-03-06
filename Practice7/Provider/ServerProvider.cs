using Microsoft.Owin.Security.OAuth;
using Practice7.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Practice7.Provider
{
    public class ServerProvider:OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using(UserRepo repo=new UserRepo())
            {
                var user=repo.ValidateUser(context.UserName, context.Password);
                if(user == null)
                {
                    context.SetError("Invalid", "Incorrect");
                }
                var ident =new ClaimsIdentity(context.Options.AuthenticationType);
                ident.AddClaim(new Claim(ClaimTypes.Name,user.UserName));
                ident.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                foreach(var item in user.Roles.Split(','))
                {
                    ident.AddClaim(new Claim(ClaimTypes.Role, item.Trim()));
                }
                context.Validated(ident);
            }
        }
    }
}