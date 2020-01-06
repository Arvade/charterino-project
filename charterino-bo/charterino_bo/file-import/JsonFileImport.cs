using System.Collections.Generic;
using System.IO;
using charterino_bo.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace charterino_bo.import {
    public class JsonFileImport : FileImportData<Product> {
        private const string SUPPORTED_EXTENSION = "json";

        public List<Product> Import(StreamReader streamReader) {
            using (JsonTextReader reader = new JsonTextReader(streamReader)) {
                JToken token = JToken.ReadFrom(reader);
                if (token.Type == JTokenType.Array) {
                    return token.ToObject<List<Product>>();
                }

                Product data = token.ToObject<Product>();
                return new List<Product>(new[] {data});
            }
        }

        public bool supports(StreamReader streamReader) {
            string path = (streamReader.BaseStream as FileStream)?.Name;
            string[] pathParts = path.Split('/');
            string[] filenameParts = pathParts[pathParts.Length - 1].Split('.');
            if (filenameParts.Length > 1) {
                return filenameParts[1] == SUPPORTED_EXTENSION;
            }

            return false;
        }
    }
}