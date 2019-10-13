using System.Text.Json;

namespace Tqxr.Capture.Lib.OperatingModel
{
    public class ServerInteraction<TRequest, TResponse>
    {
        public string RequestAsString { get; set; }
        public TRequest Request { get; set; }
        public string ResponseAsString { get; set; }
        public TResponse Response { get; set; }

        public static implicit operator string(ServerInteraction<TRequest, TResponse> s)
        {
            return $@"
--- REQUEST
{s.RequestAsString}
--- RESPONSE
{s.ResponseAsString}
--- RESPONSE DATA
{JsonSerializer.Serialize(s.Response)}
";
        }
    }
}
