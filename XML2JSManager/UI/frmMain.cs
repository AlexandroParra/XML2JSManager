using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace XML2JSManager
{
    public partial class frmMain : Form
    {
        private XMLManager _xmlManager;

        private XmlDocument _xmlDocument;

        public frmMain()
        {
            InitializeComponent();
            txtXML.MaxLength = 1000000;
            txtXML.MouseWheel += richTextBox_MouseWheel;
            txtXML.ScrollBars = ScrollBars.Both;
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

                try
                {
                    // Cargar el XML desde el contenido del TextBox
                    _xmlManager = new XMLManager(xmlContent);

                    // Aplicamos el formato
                    txtXML.Text = XMLManager.GetFormattedXML(_xmlManager.Document);

                    // Limpiar el TreeView antes de cargar nuevos nodos
                    treeView.Nodes.Clear();

                    // Llamar a un método recursivo para agregar los nodos al TreeView
                    XmlToTreeView.AddXmlNodesToTreeView(_xmlManager.Document, treeView.Nodes, _xmlManager);

                    // Expandimos los nodos del árbol.
                    treeView.ExpandAll();
                }
                catch (XmlException ex)
                {
                    // Se produjo una excepción al cargar el XML, lo que indica que no está bien formado
                    MessageBox.Show("El XML no está bien formado. Error: " + ex.Message);
                }
            }
        }

        private void btnEnvVariables_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
                txtJavaScript.Text = JavaScriptGenerator.GetEnvironmentVariable(treeView.SelectedNode);
        }

        private void btnContent_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
                txtJavaScript.Text = JavaScriptGenerator.GetVariableContent(treeView.SelectedNode);
        }
    }
}