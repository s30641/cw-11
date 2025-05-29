using Cwiczenia11.DTOs;

namespace Cwiczenia11.Services
{
    public interface IDbService
    {
        Task<int> AddPrescription(CreatePrescriptionRequestDto request);
        
        Task<PatientDetailsDto> GetPatientDetails(int idPatient);
    }
}