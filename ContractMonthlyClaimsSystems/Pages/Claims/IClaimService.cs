using System.Security.Claims;

namespace ContractMonthlyClaimsSystems.Pages.Claims
{
    internal interface IClaimService
    {
        Task<IList<Claim>> GetPendingClaimsAsync();
        Task<bool> UpdateClaimStatusAsync(int claimId, string status);
    }
}