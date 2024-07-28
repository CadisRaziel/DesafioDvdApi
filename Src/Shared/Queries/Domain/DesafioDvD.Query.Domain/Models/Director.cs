using MongoDB.Bson.Serialization.Attributes;

namespace DesafioDvD.Query.Domain.Models
{
    //Dominio de leitura(MONGO) (ele nao precisa ter tantas regras de negocio)
    public class Director
    {
        [BsonId]
        public string Id { get; set; }
        public string FullName { get; set; } //-> Principal diferenca no nosso dominio de leitura
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}

/*
 Porque nossos Id's sao string ?
 No mongoDb normalmente ele nao se da muito bem o campo Guid
 Entao podemos simplesmente converter o nosso Guid para string e dizer que o nosso campo e um tipo string
 Fora isso tem algumas questoes:
    - Sempre que iremos salvar um dado diretamente no nosso mongoDb ele ja cria uma propriedade Id por padrao (_Id e seta um valor do tipo ObjectId),
Nos nao queremos isso, porem ele faz isso de maneira automatica, em teoria os nosso objetos no mongoDb teria um id de leitura diferente do banco de escrita se a gente permitisse que isso aconteca.
Entao para nos especificar que o campo Id(string) seja nossa chave primaria vamos instalar um package `MongoDb.Driver` e vamos dizer que nossa propriedade string Id seja uma [BsonId]
  
  [BsonId] -> Com esse atribudo o mongoDb ja vai entender que a propriedade string Id vai ter o valor de chave do tipo Id, vai ser nosso ObjectId na hora de salvar nosso document
 */