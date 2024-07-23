using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioDvD.Core.DomainObjects
{
    //Excessao personalizada quando estivermos fazendo validacao de dominio (domain)
    public class DomainException : Exception
    {
        //Assim como o Java no C# eu posso criar construtores com o mesmo nome, porem seus parametros devem ser diferentes


        /// <summary>
        /// Este construtor não faz nada além de chamar o construtor padrão da classe base Exception.
        /// </summary>
        public DomainException()
        {
            
        }

        /// <summary>
        /// Este construtor chama o construtor da classe base[Exception] que aceita uma string como parâmetro. A string message é passada para o construtor da classe base, que normalmente é usada para armazenar a mensagem de erro.
        /// </summary>
        /// <param name="message"></param>
        public DomainException(string message) : base(message)
        {
            
        }


        /// <summary>
        /// Este construtor chama o construtor da classe base[Exception] que aceita uma string e uma exceção interna como parâmetros. Aqui, message é a mensagem de erro, e innerException é uma exceção que pode ter causado a exceção atual. Isso é útil para encadear exceções, permitindo que você mantenha o contexto de erros anteriores.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DomainException(string message, Exception innerException) : base(message, innerException) 
        {
            
        }
    }
}


//O que o `base` esta fazendo aqui?
//R: DomainException é uma subclasse da classe Exception, que é a classe base
//Ao usar base(message) ou base(message, innerException), você está garantindo que a lógica do construtor da classe base seja executada,
//inicializando adequadamente a instância da exceção com a mensagem e/ou a exceção interna fornecidas. Isso é importante porque a classe
//base Exception tem comportamento padrão para armazenar e gerenciar essas informações, que é reutilizado em sua classe derivada DomainException.