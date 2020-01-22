using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using charterino_bo.model;
using CsvHelper;

namespace charterino_bo.import {
    /// <summary>
    /// Strategy for importing Csv files
    /// </summary>
    public class CsvFileImport : FileImportData<Product> {
        private const string SUPPORTED_EXTENSION = "csv";

        /// <summary>
        /// Extract list of products from csv file 
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns>List of products</returns>
        public List<Product> Import(StreamReader streamReader) {
            using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture)) {
                var records = csv.GetRecords<Product>();
                return records.ToList();
            }
        }
        
        /// <summary>
        /// Checks if strategy supports passed StreamReader
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns>true if strategy supports passed StreamReader, false otherwise</returns>
        public bool supports(StreamReader streamReader) {
            string path = (streamReader.BaseStream as FileStream).Name;
            string[] pathParts = path.Split('/');
            string[] filenameParts = pathParts[pathParts.Length - 1].Split('.');
            if (filenameParts.Length > 1) {
                return filenameParts[1] == SUPPORTED_EXTENSION;
            }

            return false;
        }
    }
}