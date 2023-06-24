using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace XML2JSManager
{
    public partial class frmMain : Form
    {
        private JavaScriptGenerator _jsGenerator;

        public frmMain()
        {
            InitializeComponent();
            txtXML.MouseWheel += richTextBox_MouseWheel;
        }


        private void richTextBox_MouseWheel(object sender, MouseEventArgs e)
        {
            const int WM_VSCROLL = 0x115;
            const int SB_LINEUP = 0;
            const int SB_LINEDOWN = 1;

            // Obtiene el valor actual de desplazamiento vertical
            int scrollPosition = NativeMethods.GetScrollPos(txtXML.Handle, NativeMethods.SB_VERT);

            // Calcula el nuevo valor de desplazamiento basado en la dirección de la rueda del ratón
            int newScrollPosition = e.Delta > 0 ? scrollPosition - 1 : scrollPosition + 1;

            // Establece el nuevo valor de desplazamiento vertical
            NativeMethods.SetScrollPos(txtXML.Handle, NativeMethods.SB_VERT, newScrollPosition, true);

            // Envía un mensaje de desplazamiento vertical al control
            NativeMethods.PostMessage(txtXML.Handle, WM_VSCROLL, e.Delta > 0 ? SB_LINEUP : SB_LINEDOWN, 0);
        }

        private static class NativeMethods
        {
            public const int SB_VERT = 1;

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern int GetScrollPos(IntPtr hWnd, int nBar);

            [DllImport("user32.dll")]
            public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

            [DllImport("user32.dll")]
            public static extern int PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        }

        private void txtXML_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLoadTreeView_Click(object sender, EventArgs e)
        {
            LoadTreeViewFromXMLTextBox();
        }

        private void LoadTreeViewFromXMLTextBox()
        {
            if (txtXML.Text != string.Empty)
            {
                // Obtener el contenido XML del TextBox
                string xmlContent = txtXML.Text;

                // Crear un nuevo documento XmlDocument
                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    // Cargar el XML desde el contenido del TextBox
                    xmlDoc.LoadXml(xmlContent);

                    // Crear una instancia de JavaScriptGenerator y pasarle el TreeView
                    _jsGenerator = new JavaScriptGenerator(treeView, txtXML.Text, txtJavaScript);


                    // Aplicamos el formato
                    txtXML.Text = GetFormattedXML(xmlDoc);

                    // Limpiar el TreeView antes de cargar nuevos nodos
                    treeView.Nodes.Clear();

                    // Llamar a un método recursivo para agregar los nodos al TreeView
                    AddXmlNodesToTreeView(xmlDoc.DocumentElement, treeView.Nodes);

                    // Expandimos los nodos del árbol.
                    treeView.ExpandAll();
                }
                catch (XmlException ex)
                {
                    // Se produjo una excepción al cargar el XML, lo que indica que no está bien formado
                    Console.WriteLine("El XML no está bien formado. Error: " + ex.Message);
                }
            }
        }

        // Método recursivo para agregar los nodos al TreeView
        private void AddXmlNodesToTreeView(XmlNode xmlNode, TreeNodeCollection treeNodes)
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

                treeNodes.Add(newNode);

                // Llama recursivamente al método para agregar los nodos hijos del nodo actual
                AddXmlNodesToTreeView(childNode, newNode.Nodes);
            }
        }

        private string GetFormattedXML(XmlDocument xmlDoc)
        {
            // Crear los ajustes de formato para el XML
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true; // Habilitar la indentación
            settings.IndentChars = "  "; // Establecer el carácter de indentación (puedes utilizar espacios, tabulaciones, etc.)
            settings.NewLineChars = "\r\n"; // Establecer los caracteres de nueva línea (puedes ajustarlos según tus preferencias)

            // Crear un StringWriter para almacenar el XML formateado
            using (StringWriter sw = new StringWriter())
            {
                // Crear un XmlWriter utilizando los ajustes de formato y el StringWriter
                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    // Escribir el documento XML formateado en el XmlWriter
                    xmlDoc.WriteTo(writer);
                }

                // Obtener el XML formateado como una cadena
                return sw.ToString();
            }
        }
    }
}