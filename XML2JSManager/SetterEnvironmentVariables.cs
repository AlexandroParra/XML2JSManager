using System;
using System.Collections.Generic;
using System.Text;

namespace XML2JSManager
{
    public static class SetterEnvironmentVariable
    {

        private const string Cabecera =
        """
            /***************************************************************
            * Script for XML Response
            ***************************************************************/
            function GetValidReference(objeto, propiedad) {
                var valor;                           
                if (objeto[propiedad] && Array.isArray(objeto[propiedad]) && objeto[propiedad].length > 0) {valor = objeto[propiedad][0];}
                if (!valor && objeto[propiedad]) {valor = objeto[propiedad];}
                return valor;
            }
            var response = xml2Json(responseBody);
            <VariablePath>            
            """;



        public static string SetEnvironmentVariable(Stack<(string name, bool isArray)> stack)
        {
            var environmentVariable = Cabecera;
            environmentVariable = environmentVariable.Replace("<VariablePath>", WriteVariablePathFromStackContent(stack));
            return environmentVariable;
        }

        private static string WriteVariablePathFromStackContent(Stack<(string name, bool isArray)> stack)
        {
            var path = new StringBuilder();
            var previousItem = string.Empty;
            bool isFirstArrayElement = true;

            foreach (var item in stack)
            {
                var value = item.name;
                if (item.isArray)
                {
                    if (isFirstArrayElement)
                    {
                        isFirstArrayElement = false;
                        path.Insert(0, $"var {previousItem} = response");
                        path.AppendLine(";");
                    }
                    var expression = $"var {value} = GetValidReference({previousItem}, '{value}');";
                    path.AppendLine(expression);
                }
                else
                    path.Append($"['{value}']");

                previousItem = value;
            }

            path.AppendLine($"pm.environment.set('{previousItem}', {previousItem});");

            return path.ToString();
        }
    }
}
