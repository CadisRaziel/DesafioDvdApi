namespace DesafioDvD.Core.DomainObjects
{
    public class Entity
    {

        /// <summary>
        /// //Quando essa entidade for chamada ela vai criar um novo Guid e vai setar o valor de CreatedAt para o horario que ela for criado
        /// </summary>
        public Entity()
        {            
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; protected set; } //-> A Data sera escrita Momento da criacao
        public DateTime UpdatedAt { get; protected set; } //-> A Data sera escrita em qualquer momento da atualizacao 
        public DateTime? DeletedAt { get; protected set; } //-> A Data sera escrita no momento em que apagar a entidade (E Nullable pois ele nao precisa necessariamente apaga do banco de escrita SqlServer)

    }
}

//protected -> Somente as classes que herdadem de entity poderao alterar essa propriedade
//CreatedAt e UpdatedAt -> Sempre quando crio um objeto esses dois tem o mesmo valor ! Porem ao fim da criacao do objeto o updatedAt vai troca o valor 