// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;
using k8s.Models;

namespace Aspire.Hosting.Dcp.Model;

internal sealed class ContainerVolumeSpec
{
    /// <summary>
    /// Name of the container volume.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// True if the volume needs to be preserved between application runs.
    /// </summary>
    [JsonPropertyName("persistent")]
    public bool? Persistent { get; set; } = true;
}

internal static class ContainerVolumeState
{
    /// <summary>
    /// The volume has not been checked for existence, or has not been created yet.
    /// </summary>
    public const string Pending = "Pending";

    /// <summary>
    /// Indicates that volume creation is blocked because the container runtime isn't healthy.
    /// Volume will be created once the runtime has recovered.
    /// </summary>
    public const string RuntimeUnhealthy = "RuntimeUnhealthy";

    /// <summary>
    /// Indicates that the underlying Docker/Podman volume has been created and is ready for use.
    /// </summary>
    public const string Ready = "Ready";
}

internal sealed class ContainerVolumeStatus : V1Status
{
    /// <summary>
    /// The current state of the container volume. See <see cref="ContainerVolumeState"/> for possible values.
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }
}
