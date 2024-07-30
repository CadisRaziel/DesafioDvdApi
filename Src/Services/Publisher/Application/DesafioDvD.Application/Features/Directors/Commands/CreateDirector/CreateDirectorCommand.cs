using MediatR;

namespace DesafioDvD.Application.Features.Directors.Commands.CreateDirector
{
    //Aos olhos do mediator todo comando e query sao requisicoes
    //IRequest -> Requisicao
    //<CreateDirectorResponse> -> Retorno da requisicao
    public record CreateDirectorCommand(string Name, string Surname) : IRequest<CreateDirectorResponse>;    
  
}

/*
 O que e records ?
    - Sao objetos que possuem dados imutaveis, sempre que a gente passar dados pra esse record a gente nao pode mudar esse record,
    Se quisermos mudar algo precisamos criar um novo record.
    - E bom utilizar caso nao for fazer nenhuma mudanca
    - Receber esses parametros para criacao do objeto e essencial
    - Eu nao posso criar o record apenas com o Name ou apenas com o Surname, tem que ser obrigatorio com os dois.
 */
