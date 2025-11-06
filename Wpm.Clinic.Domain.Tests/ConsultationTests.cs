using Wpm.Clinic.Domain.Entities;
using Wpm.Clinic.Domain.ValuesObjects;
using Xunit;

namespace Wpm.Clinic.Domain.Tests;

public class ConsultationTests
{
    [Fact]
    public void Consultation_ShouldBeOpen()
    {
        var consultation = new Consultation(Guid.NewGuid());

        Assert.True(consultation.Status == ConsultationStatus.Open);
    }

    [Fact]
    public void Consultation_ShouldNotHaveEndedTimestamp()
    {
        var consultation = new Consultation(Guid.NewGuid());
        Assert.Null(consultation.When.EndedAt);
    }
    
    [Fact]
    public void Consultation_ShouldNotEnded_WhenMissingData() {
        var consultation = new Consultation(Guid.NewGuid());
        Assert.Throws<InvalidOperationException>(consultation.End);
    }

    [Fact]
    public void Consultation_ShouldEndWithCompleteData()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetWeight(20);
        consultation.SetDiagnosis("Diagnosis Test");
        consultation.SetTreatment("Treatment test");
        consultation.End();
        
        Assert.True(consultation.Status == ConsultationStatus.Closed);
    }

    [Fact]
    public void Consultation_ShouldNotAllowWeightUpdates_WhenClosed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetWeight(20);
        consultation.SetDiagnosis("Diagnosis Test");
        consultation.SetTreatment("Treatment test");
        consultation.End();

        Assert.Throws<InvalidOperationException>(() => consultation.SetWeight(20));
    }

    [Fact]
    public void Consultation_ShouldNotAllowDiagnosis_WhenClosed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetWeight(20);
        consultation.SetDiagnosis("Diagnosis Test");
        consultation.SetTreatment("Treatment test");
        consultation.End();

        Assert.Throws<InvalidOperationException>(() => consultation.SetDiagnosis("diagTest"));
    }

    [Fact]
    public void Consultation_ShouldNotAllowTreatment_WhenClosed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetWeight(20);
        consultation.SetDiagnosis("Diagnosis Test");
        consultation.SetTreatment("Treatment test");
        consultation.End();

        Assert.Throws<InvalidOperationException>(() => consultation.SetTreatment("treatmentTest2"));
    }

    [Fact]
    public void Consultation_ShouldAdministerDrug()
    {
        var drugId = new DrugId(Guid.NewGuid());
        var c = new Consultation(Guid.NewGuid());
        c.AdministerDrug(drugId, new Dose(20, UnitOfMeasure.ml));
        Assert.True(c.AdministeredDrugs.Count == 1);
        Assert.True(c.AdministeredDrugs.First().DrugId == drugId);
    }

    [Fact]
    public void Consultation_ShouldRegisterVitalSign()
    {
        var consultation = new Consultation(Guid.NewGuid());
        IEnumerable<VitalSigns> vitalSigns = new []{new VitalSigns(DateTime.UtcNow, 22, 100, 120)};
        consultation.RegisterVitalSigns(vitalSigns);
        Assert.True(consultation.VitalSignsReadings.Count == 1);
        Assert.True(consultation.VitalSignsReadings.First() == vitalSigns.First());
    }

    [Fact]
    public void DateTimeRange_ShouldBeEquals()
    {
        var theDate = new DateTime(2027, 01, 01);
        var dr1 = new DateTimeRange(theDate);
        var dr2 = new DateTimeRange(theDate);
        Assert.Equal(dr1, dr2);
    }
    
}