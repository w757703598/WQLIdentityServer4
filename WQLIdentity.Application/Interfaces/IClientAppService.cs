using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos.Clients;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Interfaces
{
    public interface IClientAppService
    {
        Pagelist<ClientListDto> GetClients(PageInputDto pageInput);
        Task<ClientDto> GetClient(int Id);

        Task<bool> AddClient(CreateClientDto client);

        Task<bool> UpdateClient(UpdateClientDto client);
        Task<bool> RemoveClient(int Id);

        Task<bool> AddClientSecretAsync(ClientSecretDto clientSecret);
        Task<bool> DeleteClientSecretAsync(int secretId);

        Pagelist<ClientSecretDto> GetClientSecrets(PageInputDto pageInput, int clientId);
        Task<bool> AddClientPropertyAsync(ClientPropertyDto clientSecret);
        Task<bool> DeleteClientPropertyAsync(int propertyId);
        Pagelist<ClientPropertyDto> GetClientProperties(PageInputDto pageInput, int clientId);

        //Task<IEnumerable<SecretViewModel>> GetSecrets(string clientId);
        //Task RemoveSecret(RemoveClientSecretViewModel model);
        //Task SaveSecret(SaveClientSecretViewModel model);
        //Task<IEnumerable<ClientPropertyViewModel>> GetProperties(string clientId);
        //Task RemoveProperty(RemovePropertyViewModel model);
        //Task SaveProperty(SaveClientPropertyViewModel model);
        //Task<IEnumerable<ClaimViewModel>> GetClaims(string clientId);
        //Task RemoveClaim(RemoveClientClaimViewModel model);
        //Task SaveClaim(SaveClientClaimViewModel model);
        ////Task Save(SaveClientViewModel client);
        //Task Save(SaveClientViewModel client);
        //Task Remove(RemoveClientViewModel client);
        //Task Copy(CopyClientViewModel client);
        //Task<Client> GetClientDefaultDetails(string clientId);
    }
}
