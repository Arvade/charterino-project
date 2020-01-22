using System;
using System.Collections.Generic;
using System.IO;
using charterino_bo.import;
using charterino_bo.model;
using NUnit.Framework;

namespace charterino_test {
    public class CompositeFileImportTest {
        [Test]
        public void ShouldThrowExceptionWhenNoStrategyFoundForFile() {
            // Given
            CompositeProductFileImport composite = anyNotSupported();
            StreamReader reader = readNotSupportedFile();

            // Then
            Assert.Throws<ArgumentException>(() => composite.Import(reader));
        }

        [Test]
        public void ShouldUseJsonFileImportWhenMultipleStrategiesArePresent() {
            // Given
            CompositeProductFileImport composite = csvAndJsonSupported();
            StreamReader reader = readJsonTestFile();

            List<Product> expectedResult = new List<Product>();
            expectedResult.Add(new Product(1, "name", "category", 2.50, 10));
            expectedResult.Add(new Product(2, "name2", "category", 3.20, 1));

            // When
            List<Product> result = composite.Import(reader);

            // Then
            Assert.AreEqual(expectedResult[0], result[0]);
            Assert.AreEqual(expectedResult[1], result[1]);
        }

        [Test]
        public void ShouldUseCsvFileImportWheMultipleStrategiesArePresent() {
            // Given
            CompositeProductFileImport composite = csvAndJsonSupported();
            StreamReader reader = readEmptyCsvTestFile();

            // When
            List<Product> result = composite.Import(reader);

            // Then
            Assert.IsEmpty(result);
        }

        private StreamReader readJsonTestFile() {
            return new StreamReader("../../../data/json_test.json");
        }

        private StreamReader readEmptyCsvTestFile() {
            return new StreamReader("../../../data/empty_csv_test.csv");
        }

        private StreamReader readNotSupportedFile() {
            return new StreamReader("../../../data/empty_test_file.not_supported");
        }

        private CompositeProductFileImport csvAndJsonSupported() {
            return CompositeProductFileImport.Builder()
                .registerStrategy(new JsonFileImport())
                .registerStrategy(new CsvFileImport())
                .build();
        }

        private CompositeProductFileImport anyNotSupported() {
            return CompositeProductFileImport.Builder()
                .build();
        }
    }
}