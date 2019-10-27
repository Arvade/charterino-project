using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charterino_bo.import
{
    public interface FileImportData<T>
    {
        List<T> Import(FileStream file);
    }
}
