using Wpm.Clinic.Api.Commands;
using Wpm.Clinic.Api.Infrastructure;
using Wpm.Clinic.Domain;
using Wpm.Clinic.Domain.Entities;
using Wpm.Clinic.Domain.ValuesObjects;

namespace Wpm.Clinic.Api.Application;

public class ClinicApplicationService(ClinicDbContext dbContext)
{
    public async Task<Guid> Handle(StartConsultationCommand command)
    {
        var newConsultation = new Consultation(command.PatientId);
        await dbContext.Consultations.AddAsync(newConsultation);
        await dbContext.SaveChangesAsync();
        return newConsultation.Id;
    }

    public async Task Handle(EndConsultationCommand command)
    {
        var consultationDb = await dbContext.Consultations.FindAsync(command.ConsultationId);
        consultationDb.End();
        await dbContext.SaveChangesAsync();
    }

    public async Task Handle(SetDiagnosisCommand command)
    {
        var consultationDb = await dbContext.Consultations.FindAsync(command.ConsultationId);
        consultationDb.SetDiagnosis(command.Diagnosis);
        await dbContext.SaveChangesAsync();
    }

    public async Task Handle(SetTreatmentCommand command)
    {
        var consultationDb = await dbContext.Consultations.FindAsync(command.ConsultationId);
        consultationDb.SetTreatment(command.Treatment);
        await dbContext.SaveChangesAsync();
    }
    
    
    public async Task Handle(SetWeightCommand command)
    {
        var consultationDb = await dbContext.Consultations.FindAsync(command.ConsultationId);
        consultationDb.SetWeight(command.Weight);
        await dbContext.SaveChangesAsync();
    }

    public async Task Handle(AdministerDrugCommand command)
    {
        var consultationDb = await dbContext.Consultations.FindAsync(command.ConsultationId);
        consultationDb.AdministerDrug(command.DrugId, new Dose(command.Quantity, UnitOfMeasure.ml));
        await dbContext.SaveChangesAsync();
    }

    public async Task Handle(RegisterVitalSignsCommand command)
    {
        var consultationDb = await dbContext.Consultations.FindAsync(command.ConsultationId);
        consultationDb.RegisterVitalSigns(command.VitalSigns);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<VitalSignsReading>> Handle(Guid consultationId)
    {
        
        var consultationDb = await dbContext.Consultations.FindAsync(consultationId);
        return consultationDb.VitalSignsReadings.Select(v => new VitalSignsReading(v.ReadingDateTime, v.Temperature, v.HeartRate, v.RespiratoryRate));
    }
}