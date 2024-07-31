namespace DesafioDvD.Application.Features.Directors.Commands.CreateDirector
{
    //Response e o que vamos responder ao usuario
    public record CreateDirectorResponse(string Id, string FullName, DateTime CreatedAt, DateTime UpdatedAt);
}   
