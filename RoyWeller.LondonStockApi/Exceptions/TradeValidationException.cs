using System;
using System.Runtime.Serialization;

namespace RoyWeller.LondonStockApi.Exceptions;

[Serializable]
internal sealed class TradeValidationException : Exception
{
    private TradeValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    public TradeValidationException(string message) : base(message) { }
}
