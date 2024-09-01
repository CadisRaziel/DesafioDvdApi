namespace DesafioDvD.Core.EventBus.Events
{
    public record DirectorCreatedEvent(string Id, string FullName, DateTime CreatedAt, DateTime UpdatedAt);
    //Aqui serao os eventos enviados ao RabbitMQ
}
