using System.ComponentModel.DataAnnotations;

namespace API.Requests
{
    public class NameCreateRequest
    {
        public string Use { get; init; }
        [Required(ErrorMessage = $"the field does not contain a value for value")]
        public string Family { get; init; }
        public List<string> Given { get; init; }
    }
}
