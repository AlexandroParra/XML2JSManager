using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XML2JSManager
{
    public class JavaScriptGenerator
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


        private TreeView _treeView;
        private TextBox _textBox;
        private Stack<(string name, bool isArray)> _stack;

        public JavaScriptGenerator(TreeView treeView, TextBox txtOut)
        {
            _treeView = treeView;
            _textBox = txtOut;

            // Asignar el evento NodeMouseClick después de cargar los nodos en el TreeView
            _treeView.NodeMouseClick += TreeView_NodeMouseClick;

            _stack = new Stack<(string name, bool isArray)>();
        }

        private void SetEnvironmentVariable(Stack<(string name, bool isArray)> stack, TextBox txtOut)
        {
            txtOut.Text = string.Empty;
            var JavaScriptCode = Cabecera;
            JavaScriptCode = JavaScriptCode.Replace("<VariablePath>", WriteVariablePathFromStackContent(stack));
            txtOut.Text = JavaScriptCode;
        }

        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Obtener el nodo seleccionado
            TreeNode selectedNode = e.Node;

            // Limpiamos la pila.
            _stack.Clear();

            // Cargamos la pila con el árbol padre del nodo seleccionado.
            _stack = LoadStack(selectedNode, _stack);

            // Volcamos el contenido de la pila en el TextBox.
            SetEnvironmentVariable(_stack, _textBox);
        }

        private static string WriteVariablePathFromStackContent(Stack<(string name, bool isArray)> stack)
        {
            var path = new StringBuilder();
            var previousItem = string.Empty;
            bool isFirstArrayElement = true;

            foreach(var item in stack)
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

        private static Stack<(string name, bool isArray)> LoadStack(TreeNode treeNode, Stack<(string name, bool isArray)> stack )
        {
            // Agregamos el contenido del nodo a la pila            
            stack.Push(new (treeNode.Text, (bool) treeNode.Tag));                

            // Verificar si el nodo tiene un nodo padre
            if (treeNode.Parent != null)
            {
                // Llamar recursivamente a la función para cargar la pila desde el nodo seleccionado hasta el nodo raiz.
                stack = LoadStack(treeNode.Parent, stack);
            }

            return stack;
        }
    }
}
