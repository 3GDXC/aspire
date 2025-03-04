// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Azure;
using Aspire.Hosting.Azure.AppContainers;
using Aspire.Hosting.Lifecycle;
using Azure.Provisioning;
using Azure.Provisioning.AppContainers;
using Azure.Provisioning.Authorization;
using Azure.Provisioning.ContainerRegistry;
using Azure.Provisioning.Expressions;
using Azure.Provisioning.OperationalInsights;
using Azure.Provisioning.Roles;

namespace Aspire.Hosting;

/// <summary>
/// Provides extension methods for customizing Azure Container App definitions for projects.
/// </summary>
public static class AzureContainerAppExtensions
{
    /// <summary>
    /// Adds the necessary infrastructure for Azure Container Apps to the distributed application builder.
    /// </summary>
    /// <param name="builder">The distributed application builder.</param>
    public static IDistributedApplicationBuilder AddAzureContainerAppsInfrastructure(this IDistributedApplicationBuilder builder)
    {
        builder.Services.TryAddLifecycleHook<AzureContainerAppsInfrastructure>();

        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IResourceBuilder<AzureContainerAppEnvironmentResource> AddContainerAppEnvironment(this IDistributedApplicationBuilder builder, string name = "resources")
    {
        builder.AddAzureContainerAppsInfrastructure();

        // TODO: Do we support adding multiple of these?

        var containerAppEnvResource = new AzureContainerAppEnvironmentResource(name, infra =>
        {
            var locationParam = new ProvisioningParameter("location", typeof(string))
            {
                Value = BicepFunction.GetResourceGroup().Location
            };

            infra.Add(locationParam);

            var principalId = new ProvisioningParameter("principalId", typeof(string));

            infra.Add(principalId);

            var principalType = new ProvisioningParameter("principalType", typeof(string));

            infra.Add(principalType);

            var tags = new ProvisioningParameter("tags", typeof(object))
            {
                Value = new BicepDictionary<string>()
            };
            infra.Add(tags);

            var identity = new UserAssignedIdentity("mi")
            {
                Location = locationParam,
                Tags = tags
            };

            infra.Add(identity);

            var containerRegistry = new ContainerRegistryService("acr")
            {
                Location = locationParam,
                Sku = new() { Name = ContainerRegistrySkuName.Basic },
                Tags = tags
            };

            infra.Add(containerRegistry);

            var pullRa = containerRegistry.CreateRoleAssignment(ContainerRegistryBuiltInRole.AcrPull, identity);

            // There's a bug in the CDK, see https://github.com/Azure/azure-sdk-for-net/issues/47265
            pullRa.Name = BicepFunction.CreateGuid(containerRegistry.Id, identity.Id, pullRa.RoleDefinitionId);
            infra.Add(pullRa);

            var pushRa = containerRegistry.CreateRoleAssignment(ContainerRegistryBuiltInRole.AcrPush, identity);
            pushRa.Name = BicepFunction.CreateGuid(containerRegistry.Id, identity.Id, pushRa.RoleDefinitionId);
            infra.Add(pushRa);

            var laWorkspace = new OperationalInsightsWorkspace("law")
            {
                Location = locationParam,
                Sku = new() { Name = OperationalInsightsWorkspaceSkuName.PerGB2018 },
                Tags = tags
            };
            infra.Add(laWorkspace);

            var containerAppEnvironment = new ContainerAppManagedEnvironment("cae")
            {
                Location = locationParam,
                WorkloadProfiles = [
                    new ContainerAppWorkloadProfile()
                {
                    WorkloadProfileType = "Consumption",
                    Name = "consumption"
                }
                ],
                AppLogsConfiguration = new()
                {
                    Destination = "log-analytics",
                    LogAnalyticsConfiguration = new()
                    {
                        CustomerId = laWorkspace.CustomerId,
                        SharedKey = laWorkspace.GetKeys().PrimarySharedKey
                    }
                },
                Tags = tags
            };

            infra.Add(containerAppEnvironment);

            var dashboard = new ContainerAppEnvironmentDotnetComponentResource("aspireDashboard", "2024-10-02-preview")
            {
                Name = "aspire-dashboard",
                ComponentType = "AspireDashboard",
                Parent = containerAppEnvironment
            };

            infra.Add(dashboard);

            var bicepPrincipalType = new BicepValue<RoleManagementPrincipalType>(((BicepExpression?)principalType.Value)!);

            var roleAssignment = containerAppEnvironment.CreateRoleAssignment(AppContainersBuiltInRole.Contributor,
                bicepPrincipalType,
                principalId);

            infra.Add(roleAssignment);

            // TODO: Add secret outputs and volume generation

            infra.Add(new ProvisioningOutput("MANAGED_IDENTITY_CLIENT_ID", typeof(string))
            {
                Value = identity.ClientId
            });

            infra.Add(new ProvisioningOutput("MANAGED_IDENTITY_NAME", typeof(string))
            {
                Value = identity.Name
            });

            infra.Add(new ProvisioningOutput("MANAGED_IDENTITY_PRINCIPAL_ID", typeof(string))
            {
                Value = identity.PrincipalId
            });

            infra.Add(new ProvisioningOutput("AZURE_LOG_ANALYTICS_WORKSPACE_NAME", typeof(string))
            {
                Value = laWorkspace.Name
            });

            infra.Add(new ProvisioningOutput("AZURE_LOG_ANALYTICS_WORKSPACE_ID", typeof(string))
            {
                Value = laWorkspace.Id
            });

            infra.Add(new ProvisioningOutput("AZURE_CONTAINER_REGISTRY_NAME", typeof(string))
            {
                Value = containerRegistry.Name
            });

            infra.Add(new ProvisioningOutput("AZURE_CONTAINER_REGISTRY_ENDPOINT", typeof(string))
            {
                Value = containerRegistry.LoginServer
            });

            infra.Add(new ProvisioningOutput("AZURE_CONTAINER_REGISTRY_MANAGED_IDENTITY_ID", typeof(string))
            {
                Value = identity.Id
            });

            infra.Add(new ProvisioningOutput("AZURE_CONTAINER_APPS_ENVIRONMENT_NAME", typeof(string))
            {
                Value = containerAppEnvironment.Name
            });

            infra.Add(new ProvisioningOutput("AZURE_CONTAINER_APPS_ENVIRONMENT_ID", typeof(string))
            {
                Value = containerAppEnvironment.Id
            });

            infra.Add(new ProvisioningOutput("AZURE_CONTAINER_APPS_ENVIRONMENT_DEFAULT_DOMAIN", typeof(string))
            {
                Value = containerAppEnvironment.DefaultDomain
            });
        });

        return builder.AddResource(containerAppEnvResource)
                      .WithManifestPublishingCallback(containerAppEnvResource.WriteToManifest);
    }
}
