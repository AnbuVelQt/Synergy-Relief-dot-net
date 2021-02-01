using Synergy.ReliefCenter.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synergy.ReliefCenter.Data.Repositories.Abstraction.PolicyRepository;

namespace Synergy.ReliefCenter.Services
{
    public class AuthorizationPolicyService : IAuthorizationPolicyService
    {
        private readonly IAccessPoliciesRepository _accessPoliciesRepository;
        private readonly IPolicyUsersRepository _policyUsersRepository;
        private readonly IPolicyRolesRepository _policyRolesRepository;
        public AuthorizationPolicyService(IAccessPoliciesRepository accessPoliciesRepository, IPolicyUsersRepository policyUsersRepository, IPolicyRolesRepository policyRolesRepository)
        {
            _accessPoliciesRepository = accessPoliciesRepository;
            _policyUsersRepository = policyUsersRepository;
            _policyRolesRepository = policyRolesRepository;
        }

        public bool ValidateRole(string token, string policy)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwttoken = token.Replace("bearer ", string.Empty);

                var tokenClaims = handler.ReadToken(jwttoken) as JwtSecurityToken;
                var tokenRoles = tokenClaims.Claims.Where(claim => claim.Type.Equals("role")).Select(c => c.Value).ToList();
                var tokenRank = tokenClaims.Claims.Where(claim => claim.Type.Equals("rank")).Select(c => c.Value).FirstOrDefault();
                var tokenUserId = tokenClaims.Claims.Where(claim => claim.Type.Equals("user_id")).Select(c => c.Value).FirstOrDefault();
                var policyList = _accessPoliciesRepository.GetAllIncluding().FirstOrDefault(x => x.Identifier.Equals(policy));
                if (policyList != null)
                {
                    var roles = _policyRolesRepository.GetAllIncluding().Where(x => x.AccessPolicyId.Equals(policyList.Id)).ToList();
                    var users = _policyUsersRepository.GetAllIncluding().Where(x => x.AccessPolicyId.Equals(policyList.Id)).ToList();

                    foreach (var tokenRole in tokenRoles)
                    {
                        var roleList = roles.FirstOrDefault(R => R.Role.Equals(tokenRole));

                        if (roleList != null)
                        {
                            return (users.Count() > 0 ? _policyUsersRepository.GetAllIncluding().Where(w => w.AccessPolicyId.Equals(policyList.Id) && w.UserId.Equals(tokenUserId)).Count() > 0 : true) && (roleList.AllowedRanks != null ? roleList.AllowedRanks.First().Equals(tokenRank) : true) ? true : false;
                        }

                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
