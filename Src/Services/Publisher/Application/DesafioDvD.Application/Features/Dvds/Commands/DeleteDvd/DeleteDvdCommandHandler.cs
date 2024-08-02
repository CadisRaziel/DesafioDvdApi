
using DesafioDvD.Application.Contracts;
using MediatR;

namespace DesafioDvD.Application.Features.Dvds.Commands.DeleteDvd
{
    public class DeleteDvdCommandHandler : IRequestHandler<DeleteDvdCommand, DeleteDvdResponse>
    {      
        private readonly IDvdsWriteRepository _repository;

        public DeleteDvdCommandHandler(IDvdsWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteDvdResponse> Handle(DeleteDvdCommand request, CancellationToken cancellationToken)
        {
            //Nao teremos validation no delete, pois so tem o atributo Id para ser validado, e faremos aqui com um simples IF
            if (request.Id == Guid.Empty)
                return default;

            //Busco qual id que e o dvd
            var dvd = await _repository.Get(request.Id);
            if (dvd is null)
                return default;


            //Lembre-se nos iremos deletar o dvd apenas para o usuario, porem no banco nao, por isso iremos utilizar metodos construidos no dominio
            dvd.DeleteDvD();

            //Repare que aqui vamos apenas atualizar para ele pegar a data de delecao
            var result = await _repository.Update(dvd);
            if (!result)
                return default;

            return new DeleteDvdResponse(dvd.Id.ToString(), (DateTime)dvd.DeletedAt);
        }
    }
}
