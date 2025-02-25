using System;

namespace SqlConnector.DataServices
{
    public static class NumericValidationHelper
    {
        public static void ValidatePositiveInt(int value, string fieldName)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{fieldName} ne peut pas être négatif.");
            }
        }

        public static void ValidatePositiveDecimal(decimal value, string fieldName)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{fieldName} ne peut pas être négatif.");
            }
        }
    }
}