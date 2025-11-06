using Wpm.Clinic.Domain.ValuesObjects;

namespace Wpm.Clinic.Api.Commands;

public record StartConsultationCommand(Guid PatientId);

public record EndConsultationCommand(Guid ConsultationId);

public record SetDiagnosisCommand(Guid ConsultationId, string Diagnosis);

public record SetTreatmentCommand(Guid ConsultationId, string Treatment);

public record SetWeightCommand(Guid ConsultationId, decimal Weight);

public record AdministerDrugCommand(Guid ConsultationId, Guid DrugId, decimal Quantity);

public record RegisterVitalSignsCommand(Guid ConsultationId, IEnumerable<VitalSigns> VitalSigns);



