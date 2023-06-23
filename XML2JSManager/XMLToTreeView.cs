using System;
using System.Windows.Forms;
using System.Xml;

public class XmlToTreeView
{
    public static void LoadXmlToTreeView(string xmlFilePath, TreeView treeView)
    {
        try
        {
            // Crea un nuevo documento XML
            XmlDocument xmlDoc = new XmlDocument();
            // Carga el XML desde el archivo
            xmlDoc.Load(xmlFilePath);

            // Borra todos los nodos existentes en el TreeView
            treeView.Nodes.Clear();

            // Agrega el nodo raíz al TreeView
            TreeNode rootNode = new TreeNode(xmlDoc.DocumentElement.Name);
            treeView.Nodes.Add(rootNode);

            // Recorre los elementos hijos del nodo raíz
            foreach (XmlNode childNode in xmlDoc.DocumentElement.ChildNodes)
            {
                // Agrega los nodos hijos al nodo raíz del TreeView
                AddNodeToTreeView(childNode, rootNode);
            }

            // Expande el nodo raíz del TreeView
            rootNode.Expand();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al cargar el XML: " + ex.Message);
        }
    }

    private static void AddNodeToTreeView(XmlNode xmlNode, TreeNode treeNode)
    {
        // Crea un nuevo nodo en el TreeView para el nodo XML actual
        TreeNode newNode = new TreeNode(xmlNode.Name);
        treeNode.Nodes.Add(newNode);

        // Agrega los atributos del nodo XML como nodos secundarios
        if (xmlNode.Attributes != null)
        {
            foreach (XmlAttribute attribute in xmlNode.Attributes)
            {
                TreeNode attributeNode = new TreeNode(attribute.Name + ": " + attribute.Value);
                newNode.Nodes.Add(attributeNode);
            }
        }

        // Recorre los elementos hijos del nodo XML actual
        foreach (XmlNode childNode in xmlNode.ChildNodes)
        {
            // Agrega los nodos hijos al nodo actual del TreeView
            AddNodeToTreeView(childNode, newNode);
        }
    }
}

