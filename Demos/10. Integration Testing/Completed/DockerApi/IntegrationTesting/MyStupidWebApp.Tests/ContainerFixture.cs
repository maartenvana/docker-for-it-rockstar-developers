using Docker.DotNet;
using Docker.DotNet.Models;
using FluentAssertions;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyStupidWebApp.Tests
{
    public class ContainerFixture : IDisposable
    {
        private const string _containerNamePrefix = "MyStupidWebAppTestContainer_";
        private const string _dockerFileFolderName = "../../../../MyStupidWebApp";
        private const string _imageName = "MyStupidWebAppContainer";
        private readonly DockerClient _dockerClient;
        private readonly DockerClientConfiguration _dockerClientConfiguration;
        private bool _disposedValue = false;

        public ContainerFixture()
        {
            _dockerClientConfiguration = new DockerClientConfiguration(new Uri(@"npipe://./pipe/docker_engine"));
            _dockerClient = _dockerClientConfiguration.CreateClient();

            var asdfasdf = _dockerClient.Containers.ListContainersAsync(new ContainersListParameters
            {
                All = true
            }).Result;
        }

        public async Task<(string containerId, HttpClient httpClient)> CreateAndStartContainerForTestCase(string testCaseName)
        {
            var containerId = await CreateTestContainer(testCaseName);

            await StartContainer(containerId);

            var httpClient = await CreateHttpClientForContainer(containerId);

            return (containerId, httpClient);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task RemoveContainerById(string containerId)
        {
            await _dockerClient.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters
            {
                Force = true
            });
        }

        protected static async Task WaitFor(Func<Task> action, TimeSpan waitTime)
        {
            var waitUntill = DateTime.Now.Add(waitTime);

            while (DateTime.Now <= waitUntill)
            {
                try
                {
                    await action.Invoke();

                    return;
                }
                catch (Exception)
                {
                    // ignored
                }

                await Task.Delay(500);
            }

            // Invoke again for test results
            await action.Invoke();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    CleanupExistingTestContainers().Wait();

                    _dockerClientConfiguration.Dispose();
                    _dockerClient.Dispose();
                }

                _disposedValue = true;
            }
        }

        private static string TarFolder(string relativeFolderPath)
        {
            var tarFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}.tar.gz";

            var files = Directory.GetFiles(relativeFolderPath, "*", SearchOption.AllDirectories);

            var tarball = new MemoryStream();

            var archiveCreationStream = File.Open(tarFileName, FileMode.OpenOrCreate);
            var outStream = new GZipOutputStream(archiveCreationStream);
            var outputArchive = TarArchive.CreateOutputTarArchive(outStream);

            foreach (var file in files)
            {
                var tarEntry = TarEntry.CreateEntryFromFile(file);
                tarEntry.Name = tarEntry.Name.Replace(relativeFolderPath, "").TrimStart('/');
                outputArchive.WriteEntry(tarEntry, false);
            }
            outputArchive.Close();

            return tarFileName;
        }

        private async Task<string> BuildImage(string tarFileName, string imageTag, IDictionary<string, string> buildArgs)
        {
            using (var stream = File.Open(tarFileName, FileMode.Open))
            {
                var outputStream = await _dockerClient.Images.BuildImageFromDockerfileAsync(
                    stream,
                    new ImageBuildParameters
                    {
                        BuildArgs = buildArgs,
                        PullParent = true,
                        Tags = new List<string>()
                        {
                        imageTag
                        }
                    }
                );

                using (var streamReader = new StreamReader(outputStream))
                {
                    var stringBuilder = new StringBuilder();

                    while (outputStream.CanRead)
                    {
                        var line = streamReader.ReadLine();
                        if (line != null)
                        {
                            var streamMessage = JsonConvert.DeserializeObject<DockerStreamMessage>(line);
                            stringBuilder.Append(streamMessage.Stream);
                        }
                        else
                        {
                            return stringBuilder.ToString();
                        }
                    }

                    return string.Empty;
                }
            }
        }

        private async Task CleanupExistingTestContainers()
        {
            var containers = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters
            {
                All = true
            });
            var existingContainers = containers.Where(x => x.Names.Any(y => y.StartsWith(_containerNamePrefix)));

            foreach (var existingContainer in existingContainers)
            {
                await _dockerClient.Containers.RemoveContainerAsync(existingContainer.ID, new ContainerRemoveParameters
                {
                    Force = true
                });
            }
        }

        private async Task<HttpClient> CreateHttpClientForContainer(string containerId)
        {
            var containerInformation = await _dockerClient.Containers.InspectContainerAsync(containerId);

            var hostPort = containerInformation.NetworkSettings.Ports[$"80/tcp"].Single().HostPort;
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"http://localhost:{hostPort}")
            };

            return httpClient;
        }

        private async Task<string> CreateTestContainer(string testCaseName)
        {
            var buildOutput = string.Empty;
            try
            {
                var tarFileName = TarFolder(_dockerFileFolderName);
                var imageTag = $"{_imageName}:{Guid.NewGuid().ToString().Replace("-", "")}";
                buildOutput = await BuildImage(tarFileName, imageTag, buildArgs: new Dictionary<string, string>());

                var containerName = $"{_containerNamePrefix}{testCaseName}";

                await CleanupExistingTestContainers();

                var container = await _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
                {
                    Name = containerName,
                    Image = imageTag,
                    ExposedPorts = new Dictionary<string, EmptyStruct>
                    {
                        { "80", default }
                    },
                    HostConfig = new HostConfig
                    {
                        PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            "80",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                }
                            }
                        }
                    }
                    }
                });

                return container.ID;
            }
            catch (Exception exc)
            {
                throw new Exception($"Exception during creation of container, logs: \r\n{buildOutput}", exc);
            }
        }

        private async Task StartContainer(string containerId)
        {
            var startedContainer = await _dockerClient.Containers.StartContainerAsync(containerId, new ContainerStartParameters());
            startedContainer.Should().BeTrue();
        }
    }
}