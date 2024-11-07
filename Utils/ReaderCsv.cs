using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.Models.Setting_Data_Models;

namespace uni_cap_pro_be.Utils
{
    // DONE
    public class ReaderCsv
    {
        private readonly string directory = @"D:\UniCapProject\Vietnam-Address";

        public ReaderCsv() { }

        public List<T> ReadCsv<T>(string fileName, ClassMap<T> classMap)
            where T : BaseEntity<string>
        {
            string filePath = Path.Combine(directory, fileName);

            var records = new List<T>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Register the provided ClassMap dynamically
                csv.Context.RegisterClassMap(classMap);
                foreach (var record in csv.GetRecords<T>())
                {
                    if (!string.IsNullOrEmpty(record.Id)) // Example check for valid Id
                    {
                        records.Add(record);
                    }
                }
            }
            return records;
        }
    }
}
