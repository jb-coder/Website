using Portfolio.Web.Features.Architecture;
using Portfolio.Web.Features.Career;
using Portfolio.Web.Features.Home;
using Portfolio.Web.Features.Projects;
using Portfolio.Web.Features.Skills;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Builders;
using Portfolio.Web.Services.Factories;
using Portfolio.Web.Services.Mediator;
using Portfolio.Web.Services.Options;
using Portfolio.Web.Services.Repositories;
using Portfolio.Web.Services.Strategies;

namespace Portfolio.Web.Services;

/// <summary>
/// Single composition root for the application services. Keeping the wiring in
/// one extension makes lifetimes auditable and Program.cs declarative.
/// </summary>
public static class PortfolioServiceCollectionExtensions
{
    public static IServiceCollection AddPortfolio(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddOptions<PortfolioOptions>()
            .Bind(configuration.GetSection(PortfolioOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        AddRepositories(services);
        AddStrategies(services);
        AddApplicationServices(services);
        AddMediator(services);

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddSingleton<IProjectRepository, ProjectRepository>();
        services.AddSingleton<ISkillRepository, SkillRepository>();
        services.AddSingleton<ITechnologyRepository, TechnologyRepository>();
        services.AddSingleton<IExperienceRepository, ExperienceRepository>();
        services.AddSingleton<IArchitecturePatternRepository, ArchitecturePatternRepository>();
    }

    private static void AddStrategies(IServiceCollection services)
    {
        services.AddSingleton<ITechnologyBadgeStrategy, BackendTechnologyBadgeStrategy>();
        services.AddSingleton<ITechnologyBadgeStrategy, FrontendTechnologyBadgeStrategy>();
        services.AddSingleton<ITechnologyBadgeStrategy, MobileTechnologyBadgeStrategy>();
        services.AddSingleton<ITechnologyBadgeStrategy, CloudTechnologyBadgeStrategy>();
        services.AddSingleton<ITechnologyBadgeStrategy, ArchitectureTechnologyBadgeStrategy>();

        services.AddSingleton<IProjectFilterStrategy, AllProjectsFilterStrategy>();
        services.AddSingleton<IProjectFilterStrategy, ApiProjectsFilterStrategy>();
        services.AddSingleton<IProjectFilterStrategy, WebAppProjectsFilterStrategy>();
        services.AddSingleton<IProjectFilterStrategy, MobileProjectsFilterStrategy>();
        services.AddSingleton<IProjectFilterStrategy, CloudProjectsFilterStrategy>();
    }

    private static void AddApplicationServices(IServiceCollection services)
    {
        services.AddSingleton<IProjectCardFactory, ProjectCardFactory>();
        services.AddTransient<IPortfolioSectionBuilder, PortfolioSectionBuilder>();
    }

    private static void AddMediator(IServiceCollection services)
    {
        services.AddScoped<IMediator, PortfolioMediator>();

        services.AddScoped<IRequestHandler<GetProjectsQuery, IReadOnlyList<ProjectCardViewModel>>, GetProjectsQueryHandler>();
        services.AddScoped<IRequestHandler<GetFeaturedProjectsQuery, IReadOnlyList<ProjectCardViewModel>>, GetFeaturedProjectsQueryHandler>();
        services.AddScoped<IRequestHandler<GetSkillGroupsQuery, IReadOnlyList<SkillGroupViewModel>>, GetSkillGroupsQueryHandler>();
        services.AddScoped<IRequestHandler<GetCareerTimelineQuery, IReadOnlyList<Experience>>, GetCareerTimelineQueryHandler>();
        services.AddScoped<IRequestHandler<GetPortfolioStatisticsQuery, IReadOnlyList<StatsCardViewModel>>, GetPortfolioStatisticsQueryHandler>();
        services.AddScoped<IRequestHandler<GetCoreTechnologiesQuery, IReadOnlyList<TechnologyBadgeViewModel>>, GetCoreTechnologiesQueryHandler>();
        services.AddScoped<IRequestHandler<GetArchitecturePatternsQuery, IReadOnlyList<ArchitecturePattern>>, GetArchitecturePatternsQueryHandler>();

        services.AddScoped<INotificationHandler<PageVisitedNotification>, PageVisitedLoggingHandler>();
    }
}
