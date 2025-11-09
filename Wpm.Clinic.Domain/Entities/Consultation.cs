using Wpm.Clinic.Domain.Events;
using Wpm.Clinic.Domain.ValuesObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Entities;

public class Consultation : AggregateRoot
{
    private readonly List<DrugAdministration> administeredDrugs = new();
    private readonly List<VitalSigns> vitalSignsReadings = new();
    
    public Text? Diagnosis { get; private set; }
    
    public Text? Treatment { get; private set; }
    
    public DateTimeRange When { get; private set; }
    
    public PatientId PatientId { get; private set; }
    
    public Weight? CurrentWeight { get; private set; }
    
    public ConsultationStatus Status { get; private set; }
    
    public IReadOnlyCollection<DrugAdministration> AdministeredDrugs => administeredDrugs;
    
    public IReadOnlyCollection<VitalSigns> VitalSignsReadings => vitalSignsReadings;


    public Consultation(PatientId patientId)
    {
        ApplyDomainEvent(new ConsultationStarted(
            Guid.NewGuid(),
            patientId,
            DateTime.UtcNow
            ));
    }

    public Consultation(IEnumerable<IDomainEvent> domainEvents)
    {
        Load(domainEvents);
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
        ApplyDomainEvent(new ConsultationEnded(Id, DateTime.UtcNow));
    }

    public void SetWeight(Weight weight)
    {
        ApplyDomainEvent(new WeightUpdated(Id, weight));
    }

    public void SetDiagnosis(Text diagnosis)
    {
        ApplyDomainEvent(new DiagnosisUpdated(Id, diagnosis));
        
    }

    public void SetTreatment(Text treatment)
    {
        ApplyDomainEvent(new TreatmentStarted(Id, treatment));
    }

    private void ValidateConsultationStatus()
    {
        if (Status == ConsultationStatus.Closed)
            throw new InvalidOperationException("Consultation is already closed");
    }

    protected override void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case ConsultationStarted e:
                Id = e.Id;
                PatientId = e.PatientId;
                When = e.StartedAt;
                Status = ConsultationStatus.Open;
                break;
            case DiagnosisUpdated e:
                ValidateConsultationStatus();
                Diagnosis = e.Diagnosis;
                break;
            case TreatmentStarted e:
                ValidateConsultationStatus();
                Treatment = e.Treatment;
                break;
            case WeightUpdated e:
                ValidateConsultationStatus();
                CurrentWeight = e.Weight;
                break;
            case ConsultationEnded e:
                ValidateConsultationStatus();
                if (CurrentWeight == null || Diagnosis == null || Treatment == null)
                    throw new InvalidOperationException("Consultation has not been ended.");
        
                Status = ConsultationStatus.Closed;
                When = new DateTimeRange(When.StartedAt, DateTime.UtcNow);
                break;
        }
    }
}

public enum ConsultationStatus
{
    Open,
    Closed
}