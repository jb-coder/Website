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
            Role: new("Tech Lead", "Tech Lead"),
            Company: "Plexus Tech",
            Location: "Málaga, Spain",
            StartDate: new DateOnly(2025, 9, 1),
            EndDate: null,
            Summary: new(
                "Liderando un equipo de desarrollo y actuando como principal punto de contacto con el cliente.",
                "Leading a development team while acting as the main point of contact with the client."),
            Type: ExperienceType.Work,
            Icon: "briefcase",
            Highlights:
            [
                new(
                    "Lidero un equipo de desarrollo y actúo como principal punto de contacto técnico con el cliente",
                    "Lead a development team and act as the primary technical contact for the client"),
                new(
                    "Participo en la definición y evolución de aplicaciones en diferentes áreas",
                    "Contribute to the definition and evolution of applications across different business areas"),
                new(
                    "Mantengo soluciones con .NET, .NET MAUI, Python y Angular, garantizando calidad, escalabilidad y alineación con el negocio",
                    "Maintain solutions built with .NET, .NET MAUI, Python and Angular, keeping quality, scalability and business alignment"),
                new(
                    "Gestiono entornos cloud AWS, optimización mediante Redis y automatización de despliegues con Jenkins",
                    "Manage AWS cloud environments, optimize performance with Redis and automate deployments with Jenkins"),
                new(
                    "Promuevo buenas prácticas de ingeniería, colaboración multidisciplinar y mejora continua de los procesos",
                    "Promote engineering best practices, cross-disciplinary collaboration and continuous process improvement"),
            ],
            Technologies: [".NET", ".NET MAUI", "Python", "Angular", "AWS", "Redis", "Jenkins"]),
        new Experience(
            Id: "software-developer-plexus-tech",
            Role: new("Desarrollador de software", "Software Developer"),
            Company: "Plexus Tech",
            Location: "Málaga, Spain",
            StartDate: new DateOnly(2025, 2, 1),
            EndDate: new DateOnly(2025, 9, 30),
            Summary: new(
                "Análisis, desarrollo y mantenimiento de aplicaciones corporativas para Volotea.",
                "Analysis, development and maintenance of corporate applications for Volotea."),
            Type: ExperienceType.Work,
            Icon: "code",
            Highlights:
            [
                new(
                    "Implementé nuevas funcionalidades y resolví incidencias en las aplicaciones corporativas de Volotea",
                    "Implemented new features and resolved incidents across Volotea's corporate applications"),
                new(
                    "Colaboré con distintas áreas de negocio para definir y entregar soluciones alineadas con los objetivos de la compañía",
                    "Collaborated with different business areas to define and deliver solutions aligned with company goals"),
                new(
                    "Desarrollé aplicaciones web, backend y móviles con .NET, .NET MAUI y Angular",
                    "Built web, backend and mobile applications with .NET, .NET MAUI and Angular"),
                new(
                    "Integré y desplegué servicios en entornos cloud Azure y AWS",
                    "Integrated and deployed services in Azure and AWS cloud environments"),
                new(
                    "Mejoré el rendimiento de las aplicaciones con Redis y automaticé despliegues con Jenkins",
                    "Improved application performance with Redis and automated deployments with Jenkins"),
            ],
            Technologies: [".NET", ".NET MAUI", "Angular", "Azure", "AWS", "Redis", "Jenkins"]),
        new Experience(
            Id: "software-developer-anovo",
            Role: new("Desarrollador de software", "Software Developer"),
            Company: "Anovo Comlink Málaga",
            Location: "Málaga, Spain",
            StartDate: new DateOnly(2022, 7, 1),
            EndDate: new DateOnly(2025, 2, 28),
            Summary: new(
                "Desarrollo y evolución de soluciones logísticas e industriales empresariales con .NET.",
                "Development and evolution of enterprise logistics and industrial solutions with .NET."),
            Type: ExperienceType.Work,
            Icon: "code",
            Highlights:
            [
                new(
                    "Diseñé e implementé nuevas funcionalidades, integraciones y mejoras orientadas a la eficiencia operativa",
                    "Designed and implemented new features, integrations and improvements focused on operational efficiency"),
                new(
                    "Desarrollé aplicaciones web y móviles con C#, VB.NET, ASP.NET y Xamarin",
                    "Developed web and mobile applications with C#, VB.NET, ASP.NET and Xamarin"),
                new(
                    "Gestioné y optimicé bases de datos SQL Server, garantizando rendimiento e integridad de la información",
                    "Managed and optimized SQL Server databases, ensuring performance and data integrity"),
                new(
                    "Integré sistemas mediante APIs REST y Web Services",
                    "Integrated systems through REST APIs and web services"),
                new(
                    "Trabajé en entornos ágiles (SCRUM) y con Git para el control de versiones y el desarrollo en equipo",
                    "Worked in agile (SCRUM) teams using Git for version control and collaborative development"),
            ],
            Technologies: ["C#", "VB.NET", "ASP.NET", "Xamarin", "SQL Server", "REST APIs", "Git"]),
        new Experience(
            Id: "associate-professional-dedalus",
            Role: new("Associate Professional Application Delivery", "Associate Professional Application Delivery"),
            Company: "Dedalus Group",
            Location: "Málaga, Spain",
            StartDate: new DateOnly(2022, 1, 1),
            EndDate: new DateOnly(2022, 6, 30),
            Summary: new(
                "Prácticas centradas en motores de integración y estándares de interoperabilidad sanitaria.",
                "Internship focused on healthcare integration engines and interoperability standards."),
            Type: ExperienceType.Work,
            Icon: "activity",
            Highlights:
            [
                new(
                    "Formación en distintos motores de integración",
                    "Trained on different integration engines"),
                new(
                    "Formación en HL7 y FHIR",
                    "Trained on HL7 and FHIR healthcare interoperability standards"),
            ],
            Technologies: ["HL7", "FHIR", "Kibana", "Elasticsearch", "SQL Server"]),
        new Experience(
            Id: "electronics-technician-anovo",
            Role: new("Técnico de Reparación de Equipos Electrónicos", "Electronics Repair Technician"),
            Company: "Anovo Comlink Málaga",
            Location: "Málaga, Spain",
            StartDate: new DateOnly(2016, 1, 1),
            EndDate: new DateOnly(2022, 6, 30),
            Summary: new(
                "Servicio técnico y reparación de equipos electrónicos para grandes marcas de consumo.",
                "Technical service and repair of electronic equipment for major consumer brands."),
            Type: ExperienceType.Work,
            Icon: "cpu",
            Highlights:
            [
                new(
                    "Reparación de equipos para Nintendo Ibérica, HP, Pace Portugal, Philips Audio, Philips PAE y PComponentes",
                    "Repaired equipment for Nintendo Ibérica, HP, Pace Portugal, Philips Audio, Philips PAE and PComponentes"),
                new(
                    "Gestión de incidencias, control de almacén y control de calidad",
                    "Handled incident management, warehouse control and quality control"),
            ],
            Technologies: ["Electronics Repair", "Incident Management", "Quality Control"]),
    ];

    public Task<IReadOnlyList<Experience>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Entries);
    }
}
