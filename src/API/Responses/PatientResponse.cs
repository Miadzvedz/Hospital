using API.Constants;


namespace API.Responses;

public record class PatientResponse(
    PatienNameResponse Name,
    Genders Gender,
    DateTime BirthDate,
    bool Active);