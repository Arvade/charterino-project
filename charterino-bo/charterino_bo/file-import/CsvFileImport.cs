using System;
using System.Collections.Generic;
using System.IO;
using charterino_bo.model;

namespace charterino_bo.import {
    public class CsvFileImport : FileImportData<Product> {
        private const string SUPPORTED_EXTENSION = "csv";

        public List<Product> Import(StreamReader streamReader) {
            List<Product> result = new List<Product>();

            while (!streamReader.EndOfStream) {
                string line = streamReader.ReadLine();
                result.Add(fromCsvLine(line));
            }

            return result;
        }

       
        private Product fromCsvLine(string line) {
            string[] values = extractFromLine(line);
            int id = Convert.ToInt32(values[0]);
            string name = values[1];
            double price = double.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture);
            int sold = Convert.ToInt32(values[3]);
            return new Product(id, name, price, sold);
        }

        // TODO: Refactor
        private string[] extractFromLine(string line) {
            if (line.Contains("\"")) {
                string[] result = new string[4];

                string[] parts = line.Split(new[] {",\""}, StringSplitOptions.None);
                string[] numbers = parts[1].Substring(parts[1].IndexOf("\",", StringComparison.Ordinal)).Split(',');

                result[0] = parts[0];
                result[1] = parts[1].Substring(0, parts[1].IndexOf("\",", StringComparison.Ordinal));
                result[2] = numbers[1];
                result[3] = numbers[2];
                return result;
            }

            return line.Split(',');
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