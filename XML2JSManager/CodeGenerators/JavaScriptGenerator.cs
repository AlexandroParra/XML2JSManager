using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XML2JSManager
{
    public static class JavaScriptGenerator
    { 

        public static string GetEnvironmentVariable(TreeNode selectedNode)
        {
            var stack = new Stack<(string name, bool isArray)>();

            // Cargamos la pila con el árbol padre del nodo seleccionado.
            stack = LoadStack(selectedNode, stack);

            return SetterEnvironmentVariable.SetEnvironmentVariable(stack);
        }

        public static string GetVariableContent(TreeNode selectedNode)
        {
            var stack = new Stack<(string name, bool isArray)>();

            // Cargamos la pila con el árbol padre del nodo seleccionado.
            stack = LoadStack(selectedNode, stack);

            return GetterVariableContent.GetVariableContent(stack);
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
