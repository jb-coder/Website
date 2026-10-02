using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Repositories;

/// <summary>
/// In-memory professional timeline, ordered from most recent to oldest.
/// </summary>
public sealed class ExperienceRepository : IExperienceRepository
{
    private static readonly IReadOnlyList<Experience> Entries =
    [
        new Experience(
            Id: "tech-lead-plexus-tech",
            Role: "Tech Lead",
            Company: "Plexus Tech",
            Location: "Málaga, Spain",
            StartDate: new DateOnly(2025, 9, 1),
            EndDate: null,
            Summary: "Leading a development team while acting as the main point of contact with the client.",
            Type: ExperienceType.Work,
            Icon: "briefcase",
            Highlights:
            [
                "Lead a development team and act as the primary technical contact for the client",
                "Contribute to the definition and evolution of applications across different business areas",
                "Maintain solutions built with .NET, .NET MAUI, Python and Angular, keeping quality, scalability and business alignment",
                "Manage AWS cloud environments, optimize performance with Redis and automate deployments with Jenkins",
                "Promote engineering best practices, cross-disciplinary collaboration and continuous process improvement",
            ],
            Technologies: [".NET", ".NET MAUI", "Python", "Angular", "AWS", "Redis", "Jenkins"]),
        new Experience(
            Id: "software-developer-plexus-tech",
            Role: "Software Developer",
            Company: "Plexus Tech",
            Location: "Málaga, Spain",
            StartDate: new DateOnly(2025, 2, 1),
            EndDate: new DateOnly(2025, 9, 30),
            Summary: "Analysis, development and maintenance of corporate applications for Volotea.",
            Type: ExperienceType.Work,
            Icon: "code",
            Highlights:
            [
                "Implemented new features and resolved incidents across Volotea's corporate applications",
                "Collaborated with different business areas to define and deliver solutions aligned with company goals",
                "Built web, backend and mobile applications with .NET, .NET MAUI and Angular",
                "Integrated and deployed services in Azure and AWS cloud environments",
                "Improved application performance with Redis and automated deployments with Jenkins",
            ],
            Technologies: [".NET", ".NET MAUI", "Angular", "Azure", "AWS", "Redis", "Jenkins"]),
        new Experience(
            Id: "software-developer-anovo",
            Role: "Software Developer",
            Company: "Anovo Comlink Málaga",
            Location: "Málaga, Spain",
            StartDate: new DateOnly(2022, 7, 1),
            EndDate: new DateOnly(2025, 2, 28),
            Summary: "Development and evolution of enterprise logistics and industrial solutions with .NET.",
            Type: ExperienceType.Work,
            Icon: "code",
            Highlights:
            [
                "Designed and implemented new features, integrations and improvements focused on operational efficiency",
                "Developed web and mobile applications with C#, VB.NET, ASP.NET and Xamarin",
                "Managed and optimized SQL Server databases, ensuring performance and data integrity",
                "Integrated systems through REST APIs and web services",
                "Worked in agile (SCRUM) teams using Git for version control and collaborative development",
            ],
            Technologies: ["C#", "VB.NET", "ASP.NET", "Xamarin", "SQL Server", "REST APIs", "Git"]),
        new Experience(
            Id: "associate-professional-dedalus",
            Role: "Associate Professional Application Delivery",
            Company: "Dedalus Group",
            Location: "Málaga, Spain",
            StartDate: new DateOnly(2022, 1, 1),
            EndDate: new DateOnly(2022, 6, 30),
            Summary: "Internship focused on healthcare integration engines and interoperability standards.",
            Type: ExperienceType.Work,
            Icon: "activity",
            Highlights:
            [
                "Trained on different integration engines",
                "Trained on HL7 and FHIR healthcare interoperability standards",
            ],
            Technologies: ["HL7", "FHIR", "Kibana", "Elasticsearch", "SQL Server"]),
        new Experience(
            Id: "electronics-technician-anovo",
            Role: "Electronics Repair Technician",
            Company: "Anovo Comlink Málaga",
            Location: "Málaga, Spain",
            StartDate: new DateOnly(2016, 1, 1),
            EndDate: new DateOnly(2022, 6, 30),
            Summary: "Technical service and repair of electronic equipment for major consumer brands.",
            Type: ExperienceType.Work,
            Icon: "cpu",
            Highlights:
            [
                "Repaired equipment for Nintendo Ibérica, HP, Pace Portugal, Philips Audio, Philips PAE and PComponentes",
                "Handled incident management, warehouse control and quality control",
            ],
            Technologies: ["Electronics Repair", "Incident Management", "Quality Control"]),
    ];

    public Task<IReadOnlyList<Experience>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Entries);
    }
}
