using Microsoft.Build.Evaluation;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace InMemoryMSBuild
{
    class Program
    {
        static void Main(string[] args)
        {
            var sut = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\..\..\LegacyDotNetSUT\LegacyDotNetSUT.csproj"));
            Debug.Assert(File.Exists(sut));
            var project = new Project(sut);
            foreach (var filePath in project.GetItems("Compile").Select(o => o.GetMetadataValue("FullPath")))
            {
                Console.WriteLine(filePath);
            }
        }
    }
}
