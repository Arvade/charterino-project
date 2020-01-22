using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using charterino_bo.model;

namespace charterino_bo.import {
    
    /// <summary>
    ///  Strategy which groups FileImportData<T> implementations
    /// </summary>
    public class CompositeProductFileImport : FileImportData<Product> {
        private List<FileImportData<Product>> strategies = new List<FileImportData<Product>>();

        public CompositeProductFileImport(List<FileImportData<Product>> strategies) {
            this.strategies = strategies;
        }

        public List<Product> Import(StreamReader streamReader) {
            List<FileImportData<Product>> supportedStrategies =
                strategies.Where(strategy => strategy.supports(streamReader)).ToList();
            if (supportedStrategies.Count == 0) {
                throw new ArgumentException("No strategy found for " + streamReader);
            }

            return supportedStrategies[0].Import(streamReader);
        }

        public bool supports(StreamReader streamReader) {
            return strategies.Where(strategy => strategy.supports(streamReader)).ToList().Count > 0;
        }

        public static CompositeProductFileImportBuilder Builder() {
            return new CompositeProductFileImportBuilder();
        }

        public class CompositeProductFileImportBuilder {
            private List<FileImportData<Product>> strategies = new List<FileImportData<Product>>();

            public CompositeProductFileImportBuilder registerStrategy(FileImportData<Product> strategy) {
                strategies.Add(strategy);
                return this;
            }

            public CompositeProductFileImport build() {
                return new CompositeProductFileImport(strategies);
            }
        }
    }
}