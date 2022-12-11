using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NoSpaceLeftOnDevice.Model
{
    public class SystemDirectory : IFileSystem
    {
        public SystemDirectory(string name, SystemDirectory patternDirectory)
        {
            Name = name;
            Files = new List<IFileSystem>();
            PatternDirectory = patternDirectory;
        }

        public string Name { get;  set; }

        public List<IFileSystem> Files { get;  set; }

        public SystemDirectory PatternDirectory { get; set; }

        public void AddFiles(IFileSystem file)
        {
           

            Files.Add(file);
        }

        public string GetName()
        {
            return Name;
        }

        public SystemDirectory? NavigateTo(string target)
        {
            if (target == "..")
                return PatternDirectory;

            if (target == "/")
            {
                if(PatternDirectory == null)
                    return this;

                return PatternDirectory.NavigateTo("/");
            }

          

            return Files.OfType<SystemDirectory>().FirstOrDefault(x => x.GetName() == target);

        }

        public List<SystemDirectory> FindDirectoriesWithSizeLowerThan(int expectedSize)
        {
            List<SystemDirectory> result = new List<SystemDirectory>();
            
            foreach(var directory in Files.OfType<SystemDirectory>())
            {
                if(directory.GetTotalSize() <= expectedSize)
                    result.Add(directory);

                var subDirectories = directory.FindDirectoriesWithSizeLowerThan(expectedSize);

                if (subDirectories.Any())
                    result.AddRange(subDirectories);
            }

            return result;

        }

        public SystemDirectory GetSmallerDirectoryExcept(List<string> exceptions)
        {
            List<SystemDirectory> files = Files.OfType<SystemDirectory>().ToList();

            var smaller = files.Where(x => exceptions.Contains(x.Name) == false)
                               .OrderBy(x => x.GetTotalSize())
                               .FirstOrDefault();

            if (smaller == null)
                return this;

            return smaller.GetSmallerDirectoryExcept(exceptions);
        }

        public SystemDirectory FindBestFileToReduceSpace(int expectedUnusedSpace, int totalDiskSpace)
        {
            int unUsedSpace = totalDiskSpace - this.GetTotalSize();

            List<string> exceptions = new List<string>();

            SystemDirectory bestChoice = null;

            do
            {
                var smallerDirectory = GetSmallerDirectoryExcept(exceptions);

                exceptions.Add(smallerDirectory.Name);

                if(smallerDirectory.GetTotalSize() + unUsedSpace >= expectedUnusedSpace)
                    bestChoice = smallerDirectory;

            } while (bestChoice == null);

            return bestChoice;
        }


        public List<SystemDirectory> GetAllDirectories()
        {
            List<SystemDirectory> allDirectories = new List<SystemDirectory>();

            foreach (var directory in Files.OfType<SystemDirectory>())
            {
                allDirectories.AddRange(directory.GetAllDirectories());
            }

            return allDirectories.ToList();

        }

        public int GetTotalSize()
        {
            return Files.Sum(x => x.GetTotalSize());
        }
    }
}
