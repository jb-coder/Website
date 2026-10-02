using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Repositories;

/// <summary>
/// In-memory project catalogue. Stateless and immutable, therefore registered
/// as a singleton. Swapping it for a CMS or API client only requires a new
/// <see cref="IProjectRepository"/> implementation.
/// </summary>
public sealed class ProjectRepository : IProjectRepository
{
    private static readonly IReadOnlyList<Project> Projects =
    [
        new Project(
            Id: "manufacturing-master-data",
            Title: "Manufacturing Master Data Platform",
            Summary: "Centralized master data platform for a multi-plant manufacturing group.",
            Description: "Event-driven platform that governs materials, bills of materials and production recipes across plants, replacing spreadsheet-driven processes. Built with Clean Architecture, CQRS and DDD tactical patterns on top of Azure SQL, with a Blazor back-office for data stewards.",
            Category: ProjectCategory.WebApp,
            Status: ProjectStatus.InProduction,
            Year: 2024,
            Accent: "#58A6FF",
            Icon: "layers",
            IsFeatured: true,
            Highlights:
            [
                "Master data governance for 4 plants",
                "Reduced data quality incidents by 62%",
                "Near real-time sync with the ERP",
            ],
            Technologies:
            [
                Tech(".NET", TechnologyCategory.Backend, "cpu", "#58A6FF"),
                Tech("ASP.NET Core", TechnologyCategory.Backend, "server", "#79C0FF"),
                Tech("Blazor", TechnologyCategory.Frontend, "layout", "#A5D6FF"),
                Tech("EF Core", TechnologyCategory.Backend, "database", "#58A6FF"),
                Tech("Azure SQL", TechnologyCategory.Cloud, "hard-drive", "#79C0FF"),
                Tech("Clean Architecture", TechnologyCategory.Architecture, "layers", "#A5D6FF"),
            ]),
        new Project(
            Id: "airline-operations-api",
            Title: "Airline Operations API",
            Summary: "Flight and crew operations API consumed by ground and cabin teams.",
            Description: "Domain-driven Minimal API that orchestrates flight schedules, crew rosters and irregular operations. Contract-first OpenAPI, idempotent command endpoints, rate limiting, and integration events published over Azure Service Bus.",
            Category: ProjectCategory.Api,
            Status: ProjectStatus.InProduction,
            Year: 2023,
            Accent: "#79C0FF",
            Icon: "send",
            IsFeatured: true,
            Highlights:
            [
                "99.98% measured availability",
                "p95 latency under 120 ms",
                "Contract-first OpenAPI consumed by 6 clients",
            ],
            Technologies:
            [
                Tech(".NET", TechnologyCategory.Backend, "cpu", "#58A6FF"),
                Tech("Minimal APIs", TechnologyCategory.Backend, "zap", "#79C0FF"),
                Tech("DDD", TechnologyCategory.Architecture, "hexagon", "#A5D6FF"),
                Tech("Azure Service Bus", TechnologyCategory.Cloud, "share-2", "#58A6FF"),
                Tech("Redis", TechnologyCategory.Backend, "layers", "#79C0FF"),
                Tech("OpenTelemetry", TechnologyCategory.Cloud, "activity", "#A5D6FF"),
            ]),
        new Project(
            Id: "inventory-management",
            Title: "Inventory Management Platform",
            Summary: "Real-time inventory control for industrial warehouses.",
            Description: "Platform for stock, movements and cycle counting with role-based access, audit trail and operational reporting. Implemented as a modular monolith with vertical slices over ASP.NET Core and SQL Server.",
            Category: ProjectCategory.WebApp,
            Status: ProjectStatus.InProduction,
            Year: 2022,
            Accent: "#A5D6FF",
            Icon: "box",
            IsFeatured: false,
            Highlights:
            [
                "Keeps 18,000+ SKUs in sync",
                "Cycle counting time cut by 40%",
                "Full audit trail for every movement",
            ],
            Technologies:
            [
                Tech(".NET", TechnologyCategory.Backend, "cpu", "#58A6FF"),
                Tech("ASP.NET Core", TechnologyCategory.Backend, "server", "#79C0FF"),
                Tech("Blazor", TechnologyCategory.Frontend, "layout", "#A5D6FF"),
                Tech("SQL Server", TechnologyCategory.Backend, "database", "#58A6FF"),
                Tech("Docker", TechnologyCategory.Cloud, "box", "#79C0FF"),
            ]),
        new Project(
            Id: "mobile-workforce",
            Title: "Mobile Workforce App",
            Summary: "Offline-first companion app for shop-floor operators.",
            Description: "MAUI application that guides operators through production orders with barcode scanning and offline data capture, synchronizing through a resilient API as soon as connectivity returns.",
            Category: ProjectCategory.Mobile,
            Status: ProjectStatus.Delivered,
            Year: 2022,
            Accent: "#6CB6FF",
            Icon: "smartphone",
            IsFeatured: true,
            Highlights:
            [
                "Works fully offline on the shop floor",
                "Deployed to 120+ rugged devices",
                "Scan-driven, mistake-proof workflows",
            ],
            Technologies:
            [
                Tech(".NET MAUI", TechnologyCategory.Mobile, "smartphone", "#58A6FF"),
                Tech("C#", TechnologyCategory.Backend, "code", "#79C0FF"),
                Tech("SQLite", TechnologyCategory.Mobile, "database", "#A5D6FF"),
                Tech("REST", TechnologyCategory.Backend, "globe", "#58A6FF"),
                Tech("Azure App Service", TechnologyCategory.Cloud, "cloud", "#79C0FF"),
            ]),
        new Project(
            Id: "cloud-integration-hub",
            Title: "Cloud Integration Hub",
            Summary: "Integration backbone connecting manufacturing systems.",
            Description: "Serverless hub that translates and routes messages between ERP, MES and WMS using Azure Functions, Service Bus topics and Event Grid, with end-to-end tracing and automated deployments.",
            Category: ProjectCategory.Cloud,
            Status: ProjectStatus.InProduction,
            Year: 2024,
            Accent: "#388BFD",
            Icon: "share-2",
            IsFeatured: false,
            Highlights:
            [
                "1.2M messages processed per day",
                "Zero-touch deployments via pipelines",
                "End-to-end distributed tracing",
            ],
            Technologies:
            [
                Tech("Azure Functions", TechnologyCategory.Cloud, "zap", "#58A6FF"),
                Tech("Service Bus", TechnologyCategory.Cloud, "share-2", "#79C0FF"),
                Tech("Event Grid", TechnologyCategory.Cloud, "activity", "#A5D6FF"),
                Tech(".NET", TechnologyCategory.Backend, "cpu", "#58A6FF"),
                Tech("Azure DevOps", TechnologyCategory.Cloud, "git-branch", "#79C0FF"),
            ]),
        new Project(
            Id: "observability-toolkit",
            Title: "Observability Toolkit",
            Summary: "Reusable observability package for .NET services and workers.",
            Description: "Internal NuGet that standardizes structured logging, distributed tracing, health probes and dashboards across platform teams, with sensible defaults and opt-in modules.",
            Category: ProjectCategory.Cloud,
            Status: ProjectStatus.Delivered,
            Year: 2023,
            Accent: "#79C0FF",
            Icon: "activity",
            IsFeatured: false,
            Highlights:
            [
                "Adopted by 9 production services",
                "OpenTelemetry-native instrumentation",
                "Cut mean time to resolution by 35%",
            ],
            Technologies:
            [
                Tech(".NET", TechnologyCategory.Backend, "cpu", "#58A6FF"),
                Tech("OpenTelemetry", TechnologyCategory.Cloud, "activity", "#79C0FF"),
                Tech("Application Insights", TechnologyCategory.Cloud, "cloud", "#A5D6FF"),
                Tech("GitHub Actions", TechnologyCategory.Cloud, "github", "#58A6FF"),
            ]),
    ];

    public Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Projects);
    }

    public Task<Project?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        cancellationToken.ThrowIfCancellationRequested();

        var project = Projects.FirstOrDefault(candidate =>
            string.Equals(candidate.Id, id, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(project);
    }

    private static Technology Tech(string name, TechnologyCategory category, string icon, string accent) =>
        new(name, category, icon, accent);
}
