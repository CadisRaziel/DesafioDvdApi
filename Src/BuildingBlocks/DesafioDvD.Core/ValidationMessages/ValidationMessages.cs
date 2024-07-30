namespace DesafioDvD.Core.ValidationMessages
{
    public static class ValidationMessages
    {
        //Constantes de validacoes para utilizar nas validacoes dos CQRS
        public const string MIN_LENGTH_ERROR_MESSAGE = "{PropertyName} must have at least {MinLength} characters";
        public const string MAX_LENGTH_ERROR_MESSAGE = "{PropertyName} must not reach {MaxLength} characters";
        public const string EMPTY_STRING_ERROR_MESSAGE = "{PropertyName} can not be empty";
        public const string ERROR_MESSAGE = "Invalid {PropertyName}";
    }
}

//{PropertyName} -> O fluentValidation passa o nome da propriedade que a gente ta setando
//{MinLength} -> Nome do erro que estamos passando la na validacao
//{MaxLength}} -> Nome do erro que estamos passando la na validacao