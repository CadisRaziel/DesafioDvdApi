
using DesafioDvD.Core.DomainObjects;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DesafioDvD.Domain.Entities
{

    //Dominio de escrita
    public class Director : Entity
    {
        protected Director()
        {
            /*
             - Construtor protected
            O construtor protected é usado para permitir que a classe Director seja herdada por outras classes, 
            mas impede que instâncias dessa classe sejam criadas diretamente usando este construtor.
            Esse padrão é útil para garantir que a inicialização da classe ocorra de maneira controlada.
            Por exemplo, pode ser utilizado quando você deseja forçar a inicialização de certas propriedades,
            como Name e Surname, que não podem ser deixadas vazias ou com valores inválidos.
             */
        }

        public Director(string name, string surname)
        {
            // Inicializa as propriedades através dos métodos de atualização.
            UpdateName(name);
            UpdateSurname(surname);
        }


        public string Name { get; private set; } //private -> Podemos ler essa propriedade porem nao podemos altera-la
        public string Surname { get; private set; }

        public const int MIN_LENGTH = 3; //Minimo que o nome e sobrenome pode ter
        public const int MAX_LENGTH = 30; //Maximo que o nome e sobrenome pode ter

        private List<Dvd> _dvds = new List<Dvd>(); //-> Atribuo(devolvo) uma instancia para evitar `null reference exception`


        /// <summary>
        /// Metodo que retorna os _dvds
        /// </summary>
        public IReadOnlyList<Dvd> Dvds => _dvds; 

        /// <summary>
        /// devolve uma string com o nome e sobrenome do diretor
        /// </summary>
        /// <returns>Nome e sobrenome do diretor</returns>
        public string FullName()
        {
            return $"{Name} {Surname}";
        }

        /// <summary>
        /// Atualizacao do nome do diretor
        /// </summary>
        /// <param name="name"></param>
        public void UpdateName(string name)
        {
            if (!ValidateName(name))           
                //DomainException -> Esta no nosso buildingBlocks, eu precisei que o DesafioDvd.Domain adicionasse a referencia do DomainExceptions do blockbuilding
                throw new DomainException($"Invalid name for director");
            
                Name = name ;
        }

       /// <summary>
       /// Atualiza o sobrenome do diretor
       /// </summary>
       /// <param name="surname"></param>
        public void UpdateSurname(string surname)
        {
            if (!ValidateName(surname))
                //DomainException -> Esta no nosso buildingBlocks, eu precisei que o DesafioDvd.Domain adicionasse a referencia do DomainExceptions do blockbuilding
                throw new DomainException($"Invalid surname for director");

            Surname = surname;
        }


        /// <summary>
        /// Verificacao se o nome e nulo ou vazio, se ele e maior que 3 caracteres ou menor que 30 caracteres e se tem caracter especial
        /// </summary>
        /// <param name="value"></param>
        /// <returns>verdadeiro ou falso</returns>
        private bool ValidateName(string value)
        {
            //Verificando se o valor e nulo ou vazio
            if (string.IsNullOrEmpty(value) || value.Length < MIN_LENGTH || value.Length > MAX_LENGTH)
                return false;


            return Regex.IsMatch(value, "^(?=.*[A-ZÀ-ÿ~])(?=.*[a-zà-ÿ~])[A-Za-zÀ-ÿ~]+$"); //-> Se tem caracter especial ou so tem letras (Retorna um bool)
        }

    }
}

/*
 Aqui nos metodos retornamos varias  throw new DomainException caso der algo errado.
 E mais corretor ser realizado dentro dessa classe essas excecoes, pois se algo deu errado e chegou ate aqui vale a pena parar tudo lancnado uma exception
 */