using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Application.Dtos.Clients;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Domain.Interface;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServer.Infra.Extensions;
using Client = IdentityServer4.EntityFramework.Entities.Client;

namespace WQLIdentity.Application.Services
{
    public class ClientAppService: IClientAppService
    {
        private IConfigurationRepository<Client> _clientRepository;
        private IConfigurationRepository<ClientSecret> _clientSecretRepository;
        private IConfigurationRepository<ClientProperty> _clientPropertyRepository;
        private IMapper _mapper;
        public ClientAppService(IConfigurationRepository<Client> clientRepository, IConfigurationRepository<ClientSecret> clientSecretRepository, IConfigurationRepository<ClientProperty> clientPropertyRepository,IMapper mapper)
        {
            _clientRepository = clientRepository;
            _clientSecretRepository = clientSecretRepository;
            _clientPropertyRepository = clientPropertyRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddClient(CreateClientDto client)
        {
            await IsExists(client.ClientId,0);
            var entity = _mapper.Map<Client>(client);
            await _clientRepository.AddAsync(entity);
            var result = await _clientRepository.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<ClientDto> GetClient(int Id)
        {
            var result = await _clientRepository.GetAll()
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.ClientSecrets)
                .Include(x => x.Claims)
                .Include(x => x.Properties)
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();
            return _mapper.Map<ClientDto>(result);
        }


        public Pagelist<ClientListDto> GetClients(PageInputDto pageInput)
        {
            var clients =  _clientRepository.GetAll().WhereIf(!string.IsNullOrEmpty(pageInput.Search),d=>d.ClientName.Contains(pageInput.Search)).PageBy(pageInput,c=>c.Id);
            return _mapper.Map<Pagelist<ClientListDto>>(clients);

        }

        public async Task IsExists(string clientId,int Id)
        {
            var client =await _clientRepository.GetAll().SingleOrDefaultAsync(d => d.ClientId == clientId&&d.Id!= Id);
            if (client != null) throw new Exception("clientId不允许重复");
        }

        public async Task<bool> RemoveClient(int clientId)
        {
            var entity = await _clientRepository.GetByIdAsync(clientId);
            _clientRepository.Remove(entity);
            return await _clientRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateClient(UpdateClientDto clientDto)
        {
            await IsExists(clientDto.ClientId,clientDto.Id);
            var client = await _clientRepository.GetAll().Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.AllowedCorsOrigins).FirstOrDefaultAsync(c => c.Id == clientDto.Id);
            if (client == null)
            {
                throw new Exception("Client不存在");
            }
            var entity = _mapper.Map<UpdateClientDto, Client>(clientDto, client);
            _clientRepository.Update(entity);
            return await _clientRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddClientSecretAsync(ClientSecretDto clientSecret)
        {
            var client = await _clientRepository.GetAll().Where(x => x.Id == clientSecret.ClientId).SingleOrDefaultAsync();

            var secret = _mapper.Map<ClientSecret>(clientSecret);

            if (clientSecret.Hash == HashType.Sha256)
            {
                secret.Value = clientSecret.Value.Sha256();
            }
            else if (clientSecret.Hash == HashType.Sha512)
            {
                secret.Value = clientSecret.Value.Sha512();
            }
            secret.Client = client;

            await _clientSecretRepository.AddAsync(secret);
            return await _clientSecretRepository.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteClientSecretAsync(int secretId)
        {
            var secretToDelete = await _clientSecretRepository.GetAll().Where(x => x.Id == secretId).SingleOrDefaultAsync();
            _clientSecretRepository.Remove(secretToDelete);

           return await _clientSecretRepository.SaveChangesAsync() > 0;
        }

        public Pagelist<ClientSecretDto> GetClientSecrets(PageInputDto pageInput, int clientId)
        {
            var scopes = _clientSecretRepository.GetAll().Where(d => d.ClientId == clientId).PageBy(pageInput, d => d.Id);
            return _mapper.Map<Pagelist<ClientSecretDto>>(scopes);
        }
        public async Task<bool> AddClientPropertyAsync(ClientPropertyDto clientSecret)
        {
            var client = await _clientRepository.GetByIdAsync(clientSecret.ClientId);
            var entity = _mapper.Map<ClientProperty>(clientSecret);
            entity.Client = client ?? throw new Exception("客户端不存在");
            await _clientPropertyRepository.AddAsync(entity);
            return await _clientPropertyRepository.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteClientPropertyAsync(int propertyId)
        {
            var secretToDelete = await _clientPropertyRepository.GetAll().Where(x => x.Id == propertyId).SingleOrDefaultAsync();
            _clientPropertyRepository.Remove(secretToDelete);

            return await _clientPropertyRepository.SaveChangesAsync() > 0;
        }
        public Pagelist<ClientPropertyDto> GetClientProperties(PageInputDto pageInput, int clientId)
        {
            var scopes = _clientPropertyRepository.GetAll().Where(d => d.ClientId == clientId).PageBy(pageInput, d => d.Id);
            return _mapper.Map<Pagelist<ClientPropertyDto>>(scopes);
        }
    }
}
