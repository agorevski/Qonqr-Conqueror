namespace Qonqr.Exceptions;

/// <summary>
/// Base exception for all QONQR-related errors
/// </summary>
public class QonqrException : Exception
{
    public QonqrException(string message) : base(message)
    {
    }

    public QonqrException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when API calls fail
/// </summary>
public class QonqrApiException : QonqrException
{
    public int? StatusCode { get; }
    public string? Endpoint { get; }

    public QonqrApiException(string message) : base(message)
    {
    }

    public QonqrApiException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public QonqrApiException(string message, int statusCode, string endpoint) : base(message)
    {
        StatusCode = statusCode;
        Endpoint = endpoint;
    }
}

/// <summary>
/// Exception thrown when login fails
/// </summary>
public class LoginFailedException : QonqrException
{
    public string Username { get; }

    public LoginFailedException(string username, string message) : base(message)
    {
        Username = username;
    }

    public LoginFailedException(string username, string message, Exception innerException) 
        : base(message, innerException)
    {
        Username = username;
    }
}

/// <summary>
/// Exception thrown when account configuration is invalid
/// </summary>
public class AccountConfigurationException : QonqrException
{
    public int LineNumber { get; }

    public AccountConfigurationException(string message, int lineNumber) : base(message)
    {
        LineNumber = lineNumber;
    }
}
