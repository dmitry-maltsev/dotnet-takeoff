using System.Text;
using Murmur;
using Serilog.Core;
using Serilog.Events;

namespace DotnetTakeoff.Api.Infrastructure.Logging;

internal class EventTypeEnricher : ILogEventEnricher
{
    private static readonly Murmur32 Murmur = MurmurHash.Create32();

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent is null)
        {
            throw new ArgumentNullException(nameof(logEvent));
        }

        if (propertyFactory is null)
        {
            throw new ArgumentNullException(nameof(propertyFactory));
        }

        var bytes = Encoding.UTF8.GetBytes(logEvent.MessageTemplate.Text);
        var hash = Murmur.ComputeHash(bytes);
        var eventType = propertyFactory.CreateProperty("EventType", Convert.ToHexString(hash));
        logEvent.AddPropertyIfAbsent(eventType);
    }
}
