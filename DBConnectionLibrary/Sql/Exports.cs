using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
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

        public static string ExportAllToXml()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<ExportedItems>");

            AppendSection(sb, "Clients", new Client().DataService.GetAll().ConvertAll(ClientExportDtoMapper.FromModel));
            AppendSection(sb, "Commandes", new Commande().DataService.GetAll().ConvertAll(CommandeExportDtoMapper.FromModel));
            AppendSection(sb, "CompositionDeLaRecette", new CompositionDeLaRecette().DataService.GetAll().ConvertAll(CompositionDeLaRecetteExportDtoMapper.FromModel));
            AppendSection(sb, "Creation", new Creation().DataService.GetAll().ConvertAll(CreationExportDtoMapper.FromModel));
            AppendSection(sb, "Cuisiniers", new Cuisinier().DataService.GetAll().ConvertAll(CuisinierExportDtoMapper.FromModel));
            AppendSection(sb, "Entreprises", new Entreprise().DataService.GetAll().ConvertAll(EntrepriseExportDtoMapper.FromModel));
            AppendSection(sb, "Ingredients", new Ingredient().DataService.GetAll().ConvertAll(IngredientExportDtoMapper.FromModel));
            AppendSection(sb, "Livraisons", new Livraison().DataService.GetAll().ConvertAll(LivraisonExportDtoMapper.FromModel));
            AppendSection(sb, "Livre", new Livre().DataService.GetAll().ConvertAll(LivreExportDtoMapper.FromModel));
            AppendSection(sb, "Personnes", new Personne().DataService.GetAll().ConvertAll(PersonneExportDtoMapper.FromModel));
            AppendSection(sb, "Plats", new Plat().DataService.GetAll().ConvertAll(PlatExportDtoMapper.FromModel));
            AppendSection(sb, "Recettes", new Recette().DataService.GetAll().ConvertAll(RecetteExportDtoMapper.FromModel));

            sb.AppendLine("</ExportedItems>");
            var xml = sb.ToString();
            File.WriteAllText("Exports.xml", xml, Encoding.UTF8);
            return xml;
        }

        private static void AppendSection<T>(StringBuilder sb, string tagName, List<T> data)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, data);
                var sectionXml = writer.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "").Trim();
                sb.AppendLine($"<{tagName}>");
                sb.AppendLine(sectionXml);
                sb.AppendLine($"</{tagName}>");
            }
        }
        
        public static string ExportToCsv(List<Type> types)
        {
            var allLines = new List<string>();
            foreach (Type modelType in types)
            {
                dynamic model = Activator.CreateInstance(modelType);
                var data = model.DataService.GetAll();
                if (data == null || data.Count == 0) continue;

                var typeName = modelType.Name;
                var properties = modelType.GetProperties();

                allLines.Add($"[{typeName}]");
                allLines.Add(string.Join(",", Array.ConvertAll(properties, p => p.Name)));
                foreach (var item in data)
                {
                    var values = new List<string>();
                    foreach (var prop in properties)
                    {
                        object value = prop.GetValue(item);
                        values.Add(value != null ? value.ToString().Replace(",", " ") : "");
                    }
                    allLines.Add(string.Join(",", values));
                }
                allLines.Add("");
            }

            string csv = string.Join("\n", allLines);
            try
            {
                File.WriteAllText("Exports.csv", csv, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return csv;
        }
    }
}
