using DesafioDvD.Core.DomainObjects;
using DesafioDvD.Domain.Entities.Enums;

namespace DesafioDvD.Domain.Entities
{
    public class Dvd : Entity
    {
        protected Dvd()
        {
            /*
             Porque estou usando `protected` no construtor?
             O EF precisa de pelo menos 1 construtor vazio para poder criar as classes no banco
             Se nao na hora da migration nao vai funcionar
             */
        }

        public Dvd(string title, int genre, DateTime published, int copies, Guid directorId)
        {
            Available = true;
            UpdateTitle(title);
            UpdateGenre(genre);
            UpdatePublishedDate(published);
            UpdateCopies(copies);
            UpdateDirector(DirectorId);
        }     

        public string Title { get; private set; }
        public EGenre Genre { get; private set; }
        public DateTime Published { get; private set; }
        public bool Available { get; private set; }
        public int Copies { get; private set; }
        public Guid DirectorId { get; private set; }
        public Director Director { get; set; }

        public const int MIN_TITLE_LENGTH = 2;
        public const int MAX_TITLE_LENGTH = 50;


        //Available -> Variavel para verificar se o DVD esta disponivel ou nao !

        //RentCopy - ReturnCopy -> Metodos exigidos na tabela UML do DVD


        /// <summary>
        /// Alugar uma copia
        /// Valida se eu tenho 0 copias ou se o dvd esta indisponivel, se tiver lanca uma execption
        /// Caso estiver disponivel eu removo 1 dvd e atualizo o UpdateCopies
        /// </summary>
        /// <exception cref="DomainException"></exception>
        public void RentCopy()
        {
            //RentCopy -> Alugar uma dvd
            
            if (Copies == 0 || !Available)
                throw new DomainException($"DVD {Title} is not available to rent.");
            
            var copies = Copies - 1;
            UpdateCopies(copies);
        }


        /// <summary>
        /// Devolver um dvd
        /// Valida se o dvd esta indisponivel se estiver lanca uma exception 
        /// Caso estiver disponivel eu atualizo meu UpdateCopies com a devolucao
        /// </summary>
        /// <exception cref="DomainException"></exception>
        public void ReturnCopy()
        {
            //ReturnCopy -> devolver a copia
            
            if (!Available)
                throw new DomainException($"DVD {Title} is not available.");

            var copies = Copies + 1;
            UpdateCopies(copies);
        }


        /// <summary>
        /// Recebemos um directorId
        /// Valida se o dvd esta indisponivel se estiver lanca uma exception 
        /// Caso estiver disponivel farei uma validacao para ver se o directorId e igual Guid.Empty(ou seja se ele e valido) se nao valido for eu lanco uma exception
        /// Caso contrario eu atualizo o DirectorId com o directorId recebido e atualizo o UpdatedAt com o horario atual
        /// </summary>
        /// <param name="directorId"></param>
        /// <exception cref="DomainException"></exception>
        public void UpdateDirector(Guid directorId)
        {
            if (!Available)
                throw new DomainException($"DVD {Title} is not available.");

            if (directorId == Guid.Empty)
                throw new DomainException("Invalid director's Id");

            DirectorId = directorId;
            UpdatedAt = DateTime.Now;
        }


        /// <summary>
        /// Recebemos um copies
        /// Valida se o dvd esta indisponivel se estiver lanca uma exception 
        /// Caso estiver disponivel faremos mais uma validacao para ver se Copies e menor que 0, se for lanca uma exception
        /// Caso nao for atualizamos a variavel Copies com o valor copies e o UpdatedAt com a data atual
        /// </summary>
        /// <param name="copies"></param>
        /// <exception cref="DomainException"></exception>
        public void UpdateCopies(int copies)
        {
            if (!Available)
                throw new DomainException($"DVD {Title} is not available.");

            if (Copies < 0)
                throw new DomainException("Number of copies must be greater than zero.");

            Copies = copies;
            UpdatedAt = DateTime.Now;
        }


        /// <summary>
        /// Recebemos um date
        /// Valida se o dvd esta indisponivel se estiver lanca uma exception 
        /// Caso estiver disponivel pegamos a data atual em uma variavel todayDate(data atual)
        /// Validamos se todayDate(data atual) e menor que o parametro date, se for lanca uma exception (pois imagine se o usuario poe uma data maior do que a atual, o dvd nao existiria)
        /// Caso for maior atualizamos a data do Published
        /// E atualizamos o UpdatedAt com a data atual da variavel todayDate(data atual)
        /// </summary>
        /// <param name="date"></param>
        /// <exception cref="DomainException"></exception>
        public void UpdatePublishedDate(DateTime date)
        {
            if (!Available)
                throw new DomainException($"DVD {Title} is not available.");
            var todayDate = DateTime.Now;

            if (todayDate < date)
                throw new DomainException("Invalid published date");

            Published = date;
            UpdatedAt = todayDate;
        }


        /// <summary>
        /// Recebemos um genre (Porem nao e o ENUM e sim um int), pois assim estamos escondendo nosso dominio das partes externas
        /// Valida se o dvd esta indisponivel se estiver lanca uma exception 
        /// Caso estiver disponivel, faremos um switch e caso nosso genre for de 0...18 ele tem os casos proprios
        /// Se nao qualquer outro caso que passe 18, ele lanca uma exception
        /// Apos isso atualizamos a hora no UpdatedAt
        /// </summary>
        /// <param name="genre"></param>
        /// <exception cref="DomainException"></exception>
        public void UpdateGenre(int genre)
        {
            if (!Available)
                throw new DomainException($"DVD {Title} is not available.");

            Genre = genre switch
            {
                0 => EGenre.Action,
                1 => EGenre.Adventure,
                2 => EGenre.Animation,
                3 => EGenre.Comedy,
                4 => EGenre.Crime,
                5 => EGenre.Documentary,
                6 => EGenre.Drama,
                7 => EGenre.Fantasy,
                8 => EGenre.Horror,
                9 => EGenre.Musical,
                10 => EGenre.Mistery,
                11 => EGenre.Romance,
                12 => EGenre.SciFi,
                13 => EGenre.Thriller,
                14 => EGenre.Western,
                15 => EGenre.Biography,
                16 => EGenre.Historic,
                17 => EGenre.War,
                18 => EGenre.Family,
                _ => throw new DomainException("Invalid genre option!")
            };

            UpdatedAt = DateTime.Now;
        }


        /// <summary>
        /// Recebemos um titulo
        /// Valida se o dvd esta indisponivel se estiver lanca uma exception 
        /// Caso estiver disponivel vamos validar se o titulo tem espacos em branco ou esta vazio, validamos tambem o tamanho do titulo com nossas constantes MIN_TITLE_LENGTH/MAX_TITLE_LENGTH
        /// Caso passe das duas validacoes passamos o title para o Title e atualizamos o horario(UpdatedAt)
        /// </summary>
        /// <param name="title"></param>
        /// <exception cref="DomainException"></exception>
        public void UpdateTitle(string title)
        {
            if(!Available)
                throw new DomainException($"DVD {Title} is not available.");

            if (string.IsNullOrWhiteSpace(title) || title.Length < MIN_TITLE_LENGTH || title.Length > MAX_TITLE_LENGTH)
                throw new DomainException($"Invalid name {Title} to a DVD");

            Title = title;
            UpdatedAt = DateTime.Now;
        }


        /// <summary>
        /// Valida se o dvd esta indisponivel se estiver lanca uma exception 
        /// Caso estiver disponivel setamos o Available como false (Dizemos que esta indisponivel)
        /// Setamos o Copies como 0 (Zeramos as copias)
        /// Atualizamos a hora do DeletedAt para data atual
        /// </summary>
        public void DeleteDvD()
        {
            /*
            OBS:
            Nessa funcao nos nao apagaremos o DVD do banco, so indicaremos que ele esta indisponivel
            (O dvd continuara no banco mesmo deletando ele)
            */

            if (!Available)
                throw new DomainException($"DVD is already deleted.");

            Available = false;
            Copies = 0;
            DeletedAt = DateTime.Now;
        }
    }
}
