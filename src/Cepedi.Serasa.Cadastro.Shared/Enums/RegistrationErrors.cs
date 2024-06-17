using Cepedi.Serasa.Cadastro.Shared.Enums;

namespace Cepedi.Serasa.Cadastro.Shared.Exceptions
{
    public class RegistrationErrors
    {
        public static readonly ErrorResult Generic = new()
        {
            Title = "Oops, an error occurred in our system",
            Description = "Currently, our system is unavailable. Please try again later",
            Type = ErrorType.Error
        };

        public static readonly ErrorResult NoResults = new()
        {
            Title = "Your search did not return any results",
            Description = "Please try searching again",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult InvalidData = new()
        {
            Title = "Invalid data",
            Description = "The data sent in the request is invalid",
            Type = ErrorType.Error
        };

        public static readonly ErrorResult UserSaveError = new()
        {
            Title = "Error saving user",
            Description = "An error occurred while saving the user. Please try again",
            Type = ErrorType.Error
        };

        public static readonly ErrorResult PersonSaveError = new()
        {
            Title = "Error saving Person",
            Description = "An error occurred while saving Person. Please try again",
            Type = ErrorType.Error
        };

        public static readonly ErrorResult TransactionSaveError = new()
        {
            Title = "Error saving Transaction",
            Description = "An error occurred while saving Transaction. Please try again",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult QuerySaveError = new()
        {
            Title = "Error saving Query",
            Description = "An error occurred while saving Query. Please try again",
            Type = ErrorType.Error
        };

        public static readonly ErrorResult InvalTransactionIdId = new()
        {
            Title = "Invalid data",
            Description = "The specified Transaction ID is invalid",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult InvalTransactionIdIdType = new()
        {
            Title = "Invalid data",
            Description = "The specified Transaction Type ID is invalid",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult InvalidPersonId = new()
        {
            Title = "Invalid data",
            Description = "The specified Person ID is invalid",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult InvalidQueryId = new()
        {
            Title = "Invalid data",
            Description = "The specified Query ID is invalid",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult InvalScoreIdId = new()
        {
            Title = "Invalid data",
            Description = "The specified Score ID is invalid",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult InvalidUserId = new()
        {
            Title = "Invalid data",
            Description = "The specified User ID is invalid",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult ScoreAlreadyExists = new()
        {
            Title = "Invalid data",
            Description = "This Person already has an associated score",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult EmptyUserList = new()
        {
            Title = "Empty list",
            Description = "The returned list of users is empty",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult EmptyTransactionsList = new()
        {
            Title = "Empty list",
            Description = "The returned list of Transactions is empty",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult EmptyScoresList = new()
        {
            Title = "Empty list",
            Description = "The returned list of scores is empty",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult EmptyTransactionTypesList = new()
        {
            Title = "Empty list",
            Description = "The returned list of Transaction types is empty",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult EmptyQueriesList = new()
        {
            Title = "Empty list",
            Description = "The returned list of queries is empty",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult DuplicateUsername = new()
        {
            Title = "Duplicate Username",
            Description = "The provided Username is already in use by another user",
            Type = ErrorType.Alert
        };

        public static readonly ErrorResult InvalidAuthentication = new()
        {
            Title = "Invalid authentication",
            Description = "The provided credentials are invalid",
            Type = ErrorType.Error
        };
    }
}
