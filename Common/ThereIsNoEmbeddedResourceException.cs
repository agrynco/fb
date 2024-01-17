using System;
using System.Runtime.Serialization;

namespace Common;

[Serializable]
public class ThereIsNoEmbeddedResourceException : Exception
{
    public ThereIsNoEmbeddedResourceException()
    {
    }

    public ThereIsNoEmbeddedResourceException(string message) : base(message)
    {
    }

    public ThereIsNoEmbeddedResourceException(string message, Exception innerException) : base(message,
        innerException)
    {
    }
}