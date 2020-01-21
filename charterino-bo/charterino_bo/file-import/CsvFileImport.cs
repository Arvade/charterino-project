using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using charterino_bo.model;
using CsvHelper;

namespace charterino_bo.import {
    public class CsvFileImport : FileImportData<Product> {
        private const string SUPPORTED_EXTENSION = "csv";

        public List<Product> Import(StreamReader streamReader) {
            using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture)) {
                var records = csv.GetRecords<Product>();
                return records.ToList();
            }
        }

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