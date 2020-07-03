using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.OpenCover;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.PushNugetPackages);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitVersion] readonly GitVersion GitVersion;
    [Parameter] string ArtifactsPath = RootDirectory + "/artifacts/";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            GlobDirectories(Solution.Directory, "**/bin", "**/obj").ForEach(DeleteDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() => DotNetRestore(a => a.SetProjectFile(Solution)));

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetNoRestore(true)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemVer)
                .SetInformationalVersion(GitVersion.InformationalVersion));
        });

    Target RunUnitTests => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
           
        });

    Target PublishUnitTestsCoverageResult => _ => _
        .DependsOn(RunUnitTests)
        .Executes(() =>
        {

        });

    Target RunIntegrationTests => _ => _
        .DependsOn(PublishUnitTestsCoverageResult)
        .Executes(() =>
        {

        });

    Target PackNugetPackages => _ => _
        .DependsOn(RunIntegrationTests)
        .Executes(() =>
        {

        });

    Target PushNugetPackages => _ => _
        .DependsOn(PackNugetPackages)
        .Executes(() =>
        {

        });
}
