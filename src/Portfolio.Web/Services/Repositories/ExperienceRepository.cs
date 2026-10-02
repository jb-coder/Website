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
            Id: "senior-net-engineer",
            Role: "Senior .NET Engineer",
            Company: "Nordwind Solutions",
            Location: "Barcelona, Spain · Remote",
            StartDate: new DateOnly(2022, 4, 1),
            EndDate: null,
            Summary: "Technical lead for cloud-native .NET platforms in the manufacturing and logistics domain.",
            Type: ExperienceType.Work,
            Icon: "briefcase",
            Highlights:
            [
                "Designed the event-driven core of a multi-plant master data platform",
                "Introduced Clean Architecture and CQRS guidelines adopted by 5 squads",
                "Mentored 6 developers through code reviews and internal workshops",
            ],
            Technologies: [".NET", "Azure", "Clean Architecture", "CQRS", "Blazor"]),
        new Experience(
            Id: "net-developer",
            Role: ".NET Developer",
            Company: "Atlas Digital Systems",
            Location: "Valencia, Spain",
            StartDate: new DateOnly(2020, 1, 1),
            EndDate: new DateOnly(2022, 3, 31),
            Summary: "Built APIs and back-office applications for industrial inventory management.",
            Type: ExperienceType.Work,
            Icon: "code",
            Highlights:
            [
                "Delivered a real-time inventory platform used in 3 warehouses",
                "Cut deployment time from hours to minutes with Azure DevOps pipelines",
            ],
            Technologies: ["ASP.NET Core", "SQL Server", "Blazor", "Docker", "Azure DevOps"]),
        new Experience(
            Id: "software-developer",
            Role: "Software Developer",
            Company: "BitForge Studio",
            Location: "Remote",
            StartDate: new DateOnly(2018, 6, 1),
            EndDate: new DateOnly(2019, 12, 31),
            Summary: "Full-stack delivery of web and mobile line-of-business applications.",
            Type: ExperienceType.Work,
            Icon: "terminal",
            Highlights:
            [
                "Shipped 12 client projects with a 4-person team",
                "Built the studio's first production Xamarin.Forms application",
            ],
            Technologies: ["C#", "Xamarin.Forms", "JavaScript", "SQL Server"]),
        new Experience(
            Id: "azure-developer-associate",
            Role: "Microsoft Certified: Azure Developer Associate",
            Company: "Microsoft",
            Location: "Online",
            StartDate: new DateOnly(2021, 11, 1),
            EndDate: null,
            Summary: "Certification focused on designing, building and testing cloud-native applications on Azure.",
            Type: ExperienceType.Certification,
            Icon: "shield",
            Highlights: ["Validated cloud-native development with App Service, Functions and Storage"],
            Technologies: ["Azure", "Cloud-native", "Security"]),
        new Experience(
            Id: "computer-science-degree",
            Role: "B.Sc. Computer Science",
            Company: "Universitat Politècnica de València",
            Location: "Valencia, Spain",
            StartDate: new DateOnly(2014, 9, 1),
            EndDate: new DateOnly(2018, 6, 30),
            Summary: "Computer science fundamentals with a focus on distributed systems and software engineering.",
            Type: ExperienceType.Education,
            Icon: "book-open",
            Highlights:
            [
                "Final project: distributed task scheduling engine",
                "Graduated with honours",
            ],
            Technologies: ["Algorithms", "Databases", "Distributed Systems"]),
    ];

    public Task<IReadOnlyList<Experience>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Entries);
    }
}
