using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RegistroConDetalle1.UI;
using RegistroConDetalle.UI.Consulta;
using RegistroConDetalle.Entidades;

namespace RegistroConDetalle1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PersonAdderItem_Click(object sender, RoutedEventArgs e)
        {
            FormularioPersona fp = new FormularioPersona();
            fp.Show();
        }

        private void ConsultarPersonas_Click(object sender, RoutedEventArgs e)
        {
            FormularioConsulta fc = new FormularioConsulta();
            fc.Show();
        }
    }
}
