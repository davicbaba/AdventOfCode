using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSpaceLeftOnDevice.Model
{
    public class SimpleFile : IFileSystem
    {
        public SimpleFile(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get;  set; }

        public int Size { get;  set; }

        public string GetName()
        {
            return Name;
        }

        public int GetTotalSize()
        {
            return Size;
        }
    }
}
