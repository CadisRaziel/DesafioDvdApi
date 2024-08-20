

using MediatR;
using System.Windows.Input;

namespace DesafioDvD.Query.Application.Features.Directors.Commands.UpdateDirector
{
    public record UpdateDirectorCommand(
         string Id,
         string FullName,
         DateTime UpdatedAt) : IRequest<bool>;
}
