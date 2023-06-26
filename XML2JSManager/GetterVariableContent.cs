using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML2JSManager
{
    public static class GetterVariableContent
    {
        private const string Cabecera =
           """
            /***************************************************************
            * Script for XML Response
            ***************************************************************/           
            var response = xml2Json(responseBody);
            <VariablePath>            
            """;

        private const char Tabulador = ' ';
        private const int lengthTabulador = 4;

        public static string GetVariableContent(Stack<(string name, bool isArray)> stack)
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
            int numTab = 0;

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
                    
                    var forExpression = $"for(var i{value}=0 ; i{value} < {previousItem}['{value}'].length ; i{value}++) " + "{";
                    path.AppendLine(new string(Tabulador, numTab * lengthTabulador)).Append(forExpression);
                    numTab++;

                    var assignationExpresion = $"var {value} = {previousItem}['{value}'][i{value}];";
                    path.AppendLine(new string(Tabulador, numTab * lengthTabulador)).Append(assignationExpresion);
                    path.AppendLine();
                    numTab++;

                }
                else
                    path.Append($"['{value}']");

                previousItem = value;
            }

            path.AppendLine(new string(Tabulador, numTab * lengthTabulador)).Append($"console.log('{previousItem}:' + {previousItem});");

            return path.ToString();
        }
    }
}
