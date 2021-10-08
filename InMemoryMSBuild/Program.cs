using Microsoft.Build.Evaluation;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace InMemoryMSBuild
{
    class Program
    {
        static void Main(string[] args)
        {
            var sut = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\..\..\LegacyDotNetSUT\LegacyDotNetSUT.csproj"));
            Debug.Assert(File.Exists(sut));

            Console.WriteLine("Reading from disk");
            var project = new Project(sut);
            foreach (var filePath in project.GetItems("Compile").Select(o => o.GetMetadataValue("FullPath")))
            {
                Console.WriteLine($"File.Exists(\"{filePath}\") = {File.Exists(filePath)}");
            }
            ProjectCollection.GlobalProjectCollection.UnloadAllProjects();

            Console.WriteLine("Reading from memory");
            project = new Project(XmlReader.Create(new MemoryStream(File.ReadAllBytes(sut))));
            foreach (var filePath in project.GetItems("Compile").Select(o => o.GetMetadataValue("FullPath")))
            {
                Console.WriteLine($"File.Exists(\"{filePath}\") = {File.Exists(filePath)}");
            }
        }
    }
}
