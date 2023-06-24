﻿using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XML2JSManager
{
    public class JavaScriptGenerator
    {
        private TreeView _treeView;
        private TextBox _textBox;
        private XDocument _xmlDocument;
        private Stack<KeyValuePair<string, string>> _stack;

        public JavaScriptGenerator(TreeView treeView, string xmlContent, TextBox txtOut)
        {
            _treeView = treeView;
            _textBox = txtOut;

            // Asignar el evento NodeMouseClick después de cargar los nodos en el TreeView
            _treeView.NodeMouseClick += TreeView_NodeMouseClick;

            _stack = new Stack<KeyValuePair<string, string>>();
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
            WriteStackContent(_stack, _textBox);
        }

        private static void WriteStackContent(Stack<KeyValuePair<string, string>> stack, TextBox txtOut)
        {
            foreach(KeyValuePair<string, string> item in stack)
            {
                txtOut.Text += "Nombre : " + item.Key + " Valor: " + item.Value + "\r\n";
            }
        }

        private static Stack<KeyValuePair<string, string>> LoadStack(TreeNode treeNode, Stack<KeyValuePair<string, string>> stack )
        {
            // Agregamos el contenido del nodo a la pila
            var nodeValues = new KeyValuePair<string, string>(treeNode.toString(), treeNode.toString());
            stack.Push(nodeValues);                

            // Verificar si el nodo tiene un nodo padre
            if (treeNode.Parent != null)
            {
                // Llamar recursivamente a la función para cargar la pila desde el nodo seleccionado hasta el nodo raiz.
                stack = LoadStack(treeNode.Parent, stack);
            }

            return stack;
        }


        private string GetJavaScriptValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "''"; // Si el valor está vacío o contiene solo espacios en blanco, se considera una cadena vacía en JavaScript
            }

            // Verificar si el valor contiene caracteres especiales que deben ser escapados en JavaScript
            if (value.Contains("'"))
            {
                value = value.Replace("'", "\\'"); // Escapar las comillas simples en el valor
            }

            return "'" + value + "'";
        }
    }
}
