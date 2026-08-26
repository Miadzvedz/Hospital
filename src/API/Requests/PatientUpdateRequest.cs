using API.Constants;
using System.ComponentModel.DataAnnotations;


namespace API.Requests
{
    public class PatientUpdateRequest
    {
        public NameUpdateRequest Name { get; init; }
        public Genders Gender { get; init; }
        [Required(ErrorMessage = "the field does not contain a value")]
        public DateTime BirthDate { get; init; }
        public bool Active { get; init; }
    }
}
