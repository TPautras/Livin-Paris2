using System;
using System.Text.RegularExpressions;

namespace SqlConnector.DataServices
{
    public static class ValidationHelper
    {
        public static void ValidateStringField(
            string fieldValue, 
            string fieldName, 
            int maxLength, 
            bool allowNull = true)
        {
            if (string.IsNullOrEmpty(fieldValue))
            {
                if (!allowNull)
                {
                    throw new ArgumentException($"{fieldName} ne peut pas être vide.");
                }
            }
            else
            {
                if (fieldValue.Length > maxLength)
                {
                    throw new ArgumentException($"{fieldName} ne doit pas dépasser {maxLength} caractères.");
                }
                if (!Regex.IsMatch(fieldValue, @"^[a-zA-Z0-9À-ÖØ-öø-ÿ @\-]*$"))
                {
                    throw new ArgumentException($"Le champ {fieldName} contient des caractères non pris en charge.");
                }
            }
        }
    }
}