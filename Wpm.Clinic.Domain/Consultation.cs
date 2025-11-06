using Wpm.Clinic.Domain.ValuesObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain;

public class Consultation : AggregateRoot
{
    private readonly List<DrugAdministration> administeredDrugs = new();
    private readonly List<VitalSigns> vitalSignsReadings = new();
    
    public Text Diagnosis { get; private set; }
    
    public Text Treatment { get; private set; }
    
    public DateTime StatedAt { get; init; }
    
    public DateTime? EndedAt { get; private set; }
    
    public PatientId PatientId { get; init; }
    
    public Weight CurrentWeight { get; private set; }
    
    public ConsultationStatus Status { get; private set; }
    
    public IReadOnlyCollection<DrugAdministration> AdministeredDrugs => administeredDrugs;
    
    public IReadOnlyCollection<VitalSigns> VitalSignsReadings => vitalSignsReadings;


    public Consultation(PatientId patientId)
    {
        PatientId = patientId;
        Id = Guid.NewGuid();
        Status = ConsultationStatus.Open;
        StatedAt = DateTime.UtcNow;
    }

    public void AdministerDrug(DrugId drugId, Dose dose)
    {
        ValidateConsultationStatus();
        var drugAdministration = new DrugAdministration(drugId, dose);
        administeredDrugs.Add(drugAdministration);
    }

    public void RegisterVitalSigns(IEnumerable<VitalSigns> vitalSigns)
    {
        ValidateConsultationStatus();
        vitalSignsReadings.AddRange(vitalSigns);
    }
    
    public void End()
    {
        if (CurrentWeight == null || Diagnosis == null || Treatment == null)
            throw new InvalidOperationException("Consultation has not been ended.");
        
        Status = ConsultationStatus.Closed;
        EndedAt = DateTime.UtcNow;
    }

    public void SetWeight(Weight weight)
    {
        ValidateConsultationStatus();
        CurrentWeight = weight;
    }

    public void SetDiagnosis(Text diagnosis)
    {
        ValidateConsultationStatus();
        Diagnosis = diagnosis;
    }

    public void SetTreatment(Text treatment)
    {
        ValidateConsultationStatus();
        Treatment = treatment;
    }

    private void ValidateConsultationStatus()
    {
        if (Status == ConsultationStatus.Closed)
            throw new InvalidOperationException("Consultation is already closed");
    }
}

public enum ConsultationStatus
{
    Open,
    Closed
}