using System;
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

        public JavaScriptGenerator(TreeView treeView, string xmlContent, TextBox txtOut)
        {
            _treeView = treeView;
            _textBox = txtOut;
            _xmlDocument = new XDocument();

            // Cargar el XML en el documento
            _xmlDocument = XDocument.Parse(xmlContent);

            // Agregar los nodos al TreeView
            LoadTreeViewFromXml();

            // Asignar el evento NodeMouseClick después de cargar los nodos en el TreeView
            _treeView.NodeMouseClick += TreeView_NodeMouseClick;
        }

        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Obtener el nodo seleccionado
            TreeNode selectedNode = e.Node;

            // Generar el código JavaScript para mostrar el contenido del nodo seleccionado
            GenerateJavaScriptCode(selectedNode, _textBox);
        }

        private void LoadTreeViewFromXml()
        {
            // Limpiar el TreeView antes de cargar nuevos nodos
            _treeView.Nodes.Clear();

            // Agregar el nodo raíz al TreeView
            _treeView.Nodes.Add(new TreeNode(_xmlDocument.Root.Name.LocalName) { Tag = _xmlDocument.Root });

            // Llamar al método recursivo para agregar los nodos hijos
            AddXmlNodesToTreeView(_xmlDocument.Root, _treeView.Nodes[0].Nodes);
        }

        private void AddXmlNodesToTreeView(XElement xmlNode, TreeNodeCollection treeNodes)
        {
            foreach (XElement childNode in xmlNode.Elements())
            {
                // Crear un nuevo TreeNode para el nodo hijo
                TreeNode newNode = new TreeNode(childNode.Name.LocalName) { Tag = childNode };

                // Agregar el nuevo TreeNode a la colección de nodos del TreeView
                treeNodes.Add(newNode);

                // Llamar recursivamente al método para agregar los nodos hijos del nodo actual
                AddXmlNodesToTreeView(childNode, newNode.Nodes);
            }
        }

        private void GenerateJavaScriptCode(TreeNode treeNode, TextBox textBox)
        {
            // Obtener el nodo XML asociado al TreeNode actual
            XmlNode xmlNode = (XmlNode)treeNode.Tag;

            // Verificar si el nodo no es nulo, es un elemento (tag) y tiene texto
            if (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element && !string.IsNullOrWhiteSpace(xmlNode.InnerText))
            {
                // Generar el código JavaScript para mostrar el valor del nodo y su nombre
                string jsCode = $"textBox.Text += '{xmlNode.Name}: ' + {GetJavaScriptValue(xmlNode.InnerText)} + Environment.NewLine;";
                textBox.Text += jsCode;
            }

            // Verificar si el nodo tiene un nodo padre
            if (treeNode.Parent != null)
            {
                // Llamar recursivamente a la función para generar el código JavaScript para el nodo padre
                GenerateJavaScriptCode(treeNode.Parent, textBox);
            }
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
