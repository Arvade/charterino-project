using System.Collections.Generic;
using System.IO;
using charterino_bo.import;
using charterino_bo.model;
using NUnit.Framework;

namespace charterino_test {
    public class CsvFileImportTest {
        private CsvFileImport _csvFileImport = new CsvFileImport();

        [Test]
        public void ShouldReadDataFromCsvFile() {
            // Given
            StreamReader reader = readCsvTestFile();

            List<Product> expectedResult = new List<Product>();
            expectedResult.Add(new Product(1, "name", 2.50, 10));
            expectedResult.Add(new Product(2, "name2", 3.20, 1));

            // When
            List<Product> result = _csvFileImport.Import(reader);

            // Then
            Assert.AreEqual(expectedResult[0], result[0]);
            Assert.AreEqual(expectedResult[1], result[1]);
        }

        [Test]
        public void ShouldReadEmptyCsvFile() {
            // Given
            StreamReader reader = readEmptyCsvTestFile();

            // When
            List<Product> result = _csvFileImport.Import(reader);

            // Then
            Assert.IsEmpty(result);
        }

        private StreamReader readEmptyCsvTestFile() {
            return new StreamReader("../../../data/empty_csv_test.csv");
        }

        private StreamReader readCsvTestFile() {
            return new StreamReader("../../../data/csv_test.csv");
        }
    }
}