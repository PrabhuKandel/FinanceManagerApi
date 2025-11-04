using HandlebarsDotNet;

namespace FinanceManager.Infrastructure.Helpers
{
    public static class HandlebarsHelpers
    {

        public static void RegisterHandlers()
        {
            Handlebars.RegisterHelper("plusOne", (writer, context, parameters) =>
            {
                int index = 0;

                if (parameters.Length > 0 && parameters[0] != null)
                {
                    index = Convert.ToInt32(parameters[0]);
                }

                writer.WriteSafeString(index + 1);
            });


            Handlebars.RegisterHelper("eq", (writer, context, parameters) =>
            {
                if (parameters.Length != 2)
                    throw new ArgumentException("eq helper requires exactly 2 parameters");

                var left = parameters[0]?.ToString() ?? "";
                var right = parameters[1]?.ToString() ?? "";

                bool isEqual = left.Equals(right, StringComparison.OrdinalIgnoreCase);

                // For #if, only write something if true
                if (isEqual)
                    writer.WriteSafeString("true"); // any non-empty string works
                else
                    writer.WriteSafeString(""); // empty string = false in #if
            });


            Handlebars.RegisterHelper("formatDate", (writer, context, parameters) =>
            {
                if (parameters.Length != 2)
                    throw new ArgumentException("formatDate requires 2 parameters: date and format string");

                if (parameters[0] is DateTime dt && parameters[1] is string format)
                {
                    writer.WriteSafeString(dt.ToString(format));
                }
            });
            Handlebars.RegisterHelper("formatPercent", (writer, context, parameters) =>
            {
                if (parameters.Length == 1 && parameters[0] is IConvertible val)
                {
                    writer.WriteSafeString(string.Format("{0:0.##}%", val)); // removes trailing zeros
                }
            });
        }
    }
}
