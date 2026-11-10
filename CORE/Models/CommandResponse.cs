using CORE.Domain;

namespace CORE.Models
{
    public class CommandResponse : Record
    {
        public bool IsSuccessful { get; }

        public string Message { get; }

        public CommandResponse(bool isSuccessful, string message, int id = 0) : base(id)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
