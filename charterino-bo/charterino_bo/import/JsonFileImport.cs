using charterino_bo.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charterino_bo.import
{
    public class JsonFileImport : FileImportData<Product>
    {
        public List<Product> Import(FileStream file)
        {
            return new List<Product>();
        }
    }
}
