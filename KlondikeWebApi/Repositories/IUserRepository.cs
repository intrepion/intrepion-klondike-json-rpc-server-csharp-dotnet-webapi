using Intrepion.JsonRpc;
using System.Security.Claims;

namespace KlondikeWebApi.Repositories
{
    public interface IUserRepository : IDisposable
    {
        Task<JsonRpcResponse> LoginAsync(JsonRpcRequest request);
        JsonRpcResponse Logout(JsonRpcRequest request);
        JsonRpcResponse Refresh(JsonRpcRequest request);
        Task<JsonRpcResponse> RegisterAsync(JsonRpcRequest request);
        JsonRpcResponse Revoke(ClaimsPrincipal claimsPrincipal, JsonRpcRequest request);
    }
}
