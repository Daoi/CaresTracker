using CaresTracker.Importing.FileModel;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace CaresTracker.Importing
{
    public class FileManager
    {
        public List<ImportFile> results;

        public FileManager(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IgnoreBlankLines = true,
                ShouldSkipRecord = (records) => records.Record.All(field => string.IsNullOrWhiteSpace(field)) //Get rid of blank rows

        };

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                results = csv.GetRecords<ImportFile>().ToList();
            }
        }
    }
}