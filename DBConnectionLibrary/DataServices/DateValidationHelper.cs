using System;

namespace SqlConnector.DataServices
{
    public static class DateValidationHelper
    {
        /// <summary>
        /// Valide qu'une date n'est pas dans le passé.
        /// </summary>
        public static void ValidateDateNotInPast(DateTime? date, string fieldName)
        {
            if(date < DateTime.Now.Date)
            {
                throw new ArgumentException($"{fieldName} ne peut pas être dans le passé.");
            }
        }
    }
}