using System.Collections.Generic;
using System.IO;

namespace charterino_bo.import {
    public interface FileImportData<T> {
        List<T> Import(StreamReader streamReader);

        bool supports(StreamReader streamReader);
    }
}