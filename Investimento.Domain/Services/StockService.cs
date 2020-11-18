using CsvHelper;
using CsvHelper.Configuration;
using Investimento.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Investimento.Domain.Services
{
    public static class StockService
    {
        public static List<OutStock> ReadCSVFile(MemoryStream memoryStream)
        {
            try
            {
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(memoryStream, Encoding.Default))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        csv.Configuration.RegisterClassMap<OutStockMap>();
                        csv.Configuration.Delimiter = ",";
                        var records = csv.GetRecords<OutStock>().ToList();
                        return records;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public sealed class OutStockMap : ClassMap<OutStock>
    {
        public OutStockMap()
        {
            Map(x => x.Symbol).Name("symbol");
            Map(x => x.Name).Name("name");
        }
    }
}
