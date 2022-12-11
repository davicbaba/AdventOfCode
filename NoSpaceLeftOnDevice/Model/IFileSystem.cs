using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSpaceLeftOnDevice.Model
{
    public interface IFileSystem
    {

        int GetTotalSize();

        string GetName(); 

    }
}
