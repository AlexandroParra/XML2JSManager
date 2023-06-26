using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace XML2JSManager
{
    public partial class frmMain : Form
    {
        private JavaScriptGenerator _jsGenerator;

        private XMLManager _xmlManager;

        private XmlDocument _xmlDocument;

        public frmMain()
        {
            InitializeComponent();
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

            // Calcula el nuevo valor de desplazamiento basado en la direcci�n de la rueda del rat�n
            int newScrollPosition = e.Delta > 0 ? scrollPosition - 1 : scrollPosition + 1;

            // Establece el nuevo valor de desplazamiento vertical
            NativeMethods.SetScrollPos(txtXML.Handle, NativeMethods.SB_VERT, newScrollPosition, true);

            // Env�a un mensaje de desplazamiento vertical al control
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

                try
                {                
                    // Cargar el XML desde el contenido del TextBox
                    _xmlManager = new XMLManager(xmlContent);                    

                    // Aplicamos el formato
                    txtXML.Text = XMLManager.GetFormattedXML(_xmlManager.Document);

                    // Limpiar el TreeView antes de cargar nuevos nodos
                    treeView.Nodes.Clear();

                    // Llamar a un m�todo recursivo para agregar los nodos al TreeView
                    XmlToTreeView.AddXmlNodesToTreeView(_xmlManager.Document, treeView.Nodes, _xmlManager);

                    // Crear una instancia de JavaScriptGenerator y pasarle el TreeView
                    _jsGenerator = new JavaScriptGenerator(treeView, txtJavaScript);

                    // Expandimos los nodos del �rbol.
                    treeView.ExpandAll();
                }
                catch (XmlException ex)
                {
                    // Se produjo una excepci�n al cargar el XML, lo que indica que no est� bien formado
                    MessageBox.Show("El XML no est� bien formado. Error: " + ex.Message);
                }
            }
        }

    }
}