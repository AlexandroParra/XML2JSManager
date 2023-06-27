using System;
using System.Windows.Forms;
using System.Xml;

namespace XML2JSManager
{
    public static class XmlToTreeView
    {

        // Método recursivo para agregar los nodos de un documento XML a un objeto TreeView
        public static void AddXmlNodesToTreeView(XmlNode xmlNode, TreeNodeCollection treeNodes, XMLManager xmlManager)
        {
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                TreeNode newNode;

                if (childNode.NodeType == XmlNodeType.Element)
                {
                    // Si el nodo es un elemento XML, crea un nuevo TreeNode con el nombre del nodo y el InnerText
                    newNode = new TreeNode(childNode.Name);
                }
                else if (childNode.NodeType == XmlNodeType.Text)
                {
                    // Si el nodo es un nodo de texto, crea un nuevo TreeNode con el valor del texto
                    newNode = new TreeNode(childNode.InnerText);
                }
                else
                {
                    // Si el nodo no es un elemento ni un nodo de texto, crea un nuevo TreeNode con el nombre del nodo
                    newNode = new TreeNode(childNode.Name);
                }

                // Guardamos si es un array.
                newNode.Tag = xmlManager.IsArrayElement(childNode.Name);

                treeNodes.Add(newNode);

                // Llama recursivamente al método para agregar los nodos hijos del nodo actual
                AddXmlNodesToTreeView(childNode, newNode.Nodes, xmlManager);
            }
        }
    }

}