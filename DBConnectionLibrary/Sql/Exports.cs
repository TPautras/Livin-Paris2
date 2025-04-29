using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SqlConnector.Models;

namespace DBConnectionLibrary.Sql
{
    public class Exports
    {
        public static string ExportToJson(List<Type> types)
        {
            var data = new List<object>();
            foreach (Type modelType in types)
            {
                dynamic model = Activator.CreateInstance(modelType);
                var allData = model.DataService.GetAll();
                if (allData != null)
                {
                    data.AddRange(allData);
                }
            }

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            try
            {
                File.WriteAllText("Exports.json", json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return json;
        }
    }
}