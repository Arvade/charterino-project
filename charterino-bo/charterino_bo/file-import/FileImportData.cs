using System.Collections.Generic;
using System.IO;

namespace charterino_bo.import {
    
    /// <summary>
    /// Imports specific data type from file
    /// </summary>
    /// <typeparam name="T">Type of data</typeparam>
    public interface FileImportData<T> {
        
        /// <summary>
        /// Imports list of T type from provided streamReader
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns>List of T type objects</returns>
        List<T> Import(StreamReader streamReader);

        /// <summary>
        /// Checks if strategy supports passed StreamReader
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns>true if strategy supports passed StreamReader, false otherwise</returns>
        bool supports(StreamReader streamReader);
    }
}