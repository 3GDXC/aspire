//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aspire.Hosting.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Aspire.Hosting.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Anonymous volumes cannot be read-only..
        /// </summary>
        internal static string ContainerMountAnonymousVolumesReadOnlyExceptionMessage {
            get {
                return ResourceManager.GetString("ContainerMountAnonymousVolumesReadOnlyExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bind mounts must specify an absolute path..
        /// </summary>
        internal static string ContainerMountBindMountsRequireRootedPaths {
            get {
                return ResourceManager.GetString("ContainerMountBindMountsRequireRootedPaths", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bind mounts must specify a source path..
        /// </summary>
        internal static string ContainerMountBindMountsRequireSourceExceptionMessage {
            get {
                return ResourceManager.GetString("ContainerMountBindMountsRequireSourceExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Container runtime &apos;{0}&apos; could not be found. The error from the container runtime check was: {1}.
        ///See https://aka.ms/dotnet/aspire/containers for more details on supported container runtimes..
        /// </summary>
        internal static string ContainerRuntimePrerequisiteMissingExceptionMessage {
            get {
                return ResourceManager.GetString("ContainerRuntimePrerequisiteMissingExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Container runtime &apos;{0}&apos; was found but appears to be unhealthy. The error from the container runtime check was {1}..
        /// </summary>
        internal static string ContainerRuntimeUnhealthyExceptionMessage {
            get {
                return ResourceManager.GetString("ContainerRuntimeUnhealthyExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Container runtime &apos;{0}&apos; was found but appears to be unresponsive. The command &apos;{0}&apos; did not return after {1} seconds..
        /// </summary>
        internal static string ContainerRuntimeUnresponsiveExceptionMessage {
            get {
                return ResourceManager.GetString("ContainerRuntimeUnresponsiveExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application orchestrator dependency check returned an error: {0}.
        /// </summary>
        internal static string DcpDependencyCheckFailedMessage {
            get {
                return ResourceManager.GetString("DcpDependencyCheckFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Newer version of the Aspire.Hosting.AppHost package is required to run the application. Ensure you are referencing at least version &apos;{0}&apos;..
        /// </summary>
        internal static string DcpVersionCheckTooLowMessage {
            get {
                return ResourceManager.GetString("DcpVersionCheckTooLowMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Launch settings file does not contain &apos;{0}&apos; profile..
        /// </summary>
        internal static string LaunchSettingsFileDoesNotContainProfileExceptionMessage {
            get {
                return ResourceManager.GetString("LaunchSettingsFileDoesNotContainProfileExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Project does not contain project metadata..
        /// </summary>
        internal static string ProjectDoesNotContainMetadataExceptionMessage {
            get {
                return ResourceManager.GetString("ProjectDoesNotContainMetadataExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Project file &apos;{0}&apos; was not found..
        /// </summary>
        internal static string ProjectFileNotFoundExceptionMessage {
            get {
                return ResourceManager.GetString("ProjectFileNotFoundExceptionMessage", resourceCulture);
            }
        }
    }
}
