using System.Collections.Generic;
using System.IO;
using charterino_bo.import;
using charterino_bo.model;
using NUnit.Framework;

namespace charterino_test {
    public class JsonFileImportTest {
        
        private JsonFileImport jsonFileImport = new JsonFileImport();

        [Test]
        public void ShouldReadDataFromJsonFile() {
            // Given
            StreamReader reader = readJsonTestFile();

            List<Product> expectedResult = new List<Product>();
            expectedResult.Add(new Product(1, "name", 2.50, 10));
            expectedResult.Add(new Product(2, "name2", 3.20, 1));

            // When
            List<Product> result = jsonFileImport.Import(reader);

            // Then
            Assert.AreEqual(expectedResult[0], result[0]);
            Assert.AreEqual(expectedResult[1], result[1]);
        }

        [Test]
        public void ShouldReturnEmptyWhenJsonFileIsEmpty() {
            // Given
            StreamReader reader = readEmptyJsonTestFile();

            // When
            List<Product> result = jsonFileImport.Import(reader);

            // Then
            Assert.IsEmpty(result);
        }

        private StreamReader readEmptyJsonTestFile() {
            return new StreamReader(
                "../../../data/empty_json_test.json");
        }

        private StreamReader readJsonTestFile() {
            return new StreamReader(
                "../../../data/json_test.json");
        }
    }
}