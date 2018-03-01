using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BRRF.IdentityServer.Repository;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Linq;


namespace BRRF.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private IAuthRepository _repository;

        public ProfileService(IAuthRepository rep)
        {
            this._repository = rep;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subjectId = context.Subject.GetSubjectId();
                var user = _repository.GetUserById(Convert.ToInt32(subjectId));

                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
			    };
                user.UserRole.ToList().ForEach(usrRol => claims.Add(new Claim(JwtClaimTypes.Role, usrRol.Role.Name)));
                user.UserBrand.ToList().ForEach(usrBrand => claims.Add(new Claim("brandname", usrBrand.Brand.Name)));

                context.IssuedClaims = claims;
                return Task.FromResult(0);
            }
            catch (Exception x)
            {
                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _repository.GetUserById(Convert.ToInt32(context.Subject.GetSubjectId()));
            //context.IsActive = (user != null) && user.Active;
            return Task.FromResult(0);
        }
    }
}
