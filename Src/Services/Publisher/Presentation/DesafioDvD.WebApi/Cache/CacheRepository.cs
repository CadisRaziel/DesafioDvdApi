using DesafioDvD.Query.Application.Features.Dvds.Queries.GetDvd;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DesafioDvD.WebApi.Cache
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _redisCache; // ->
        private readonly DistributedCacheEntryOptions _cacheEntryOptions; //-> Para podermos configurar o cache


        public CacheRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
            _cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20), //-> tempo de expiracao absoluto, os dados do cache vai sumir depois de 30 segundos
                SlidingExpiration = TimeSpan.FromSeconds(20), //-> toda vez que eu salvo no cache porem nao houve procuras por ele(dados do cache) dentro de um determinado tempo, entao apaga o cache, se houve alguma procura deixa la, porem lembre-se eu defini em cima para 30 segundos
            };
        }

        public async Task<GetDvdResponse> Get(string title)
        {
            //Redis -> Banco em memoria de chave e valor
            //a chave pode ser qualquer coisa, aqui usaremos o title
            var response = await _redisCache.GetStringAsync(title); //-> Normalmente salvamos cache em string
            if (response is null)
                return default;

            return JsonSerializer.Deserialize<GetDvdResponse>(response);
        }

        public async Task Update(GetDvdResponse response)
        {
            //Primeiro: passamos a chave `title
            //Segundo: fazemos a JsonSerializer a serializacao do objeto, a transformacao do objeto para string
            //terceiro: _cacheEntryOptions, formaliza para o redis as propriedades de expiracao que eu fiz no construtor
            await _redisCache.SetStringAsync(response.Title, JsonSerializer.Serialize(response), _cacheEntryOptions);
        }
    }
}
