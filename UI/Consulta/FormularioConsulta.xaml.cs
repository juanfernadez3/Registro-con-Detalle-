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
using System.Windows.Shapes;
using RegistroConDetalle.Entidades;
using RegistroConDetalle.BLL;

namespace RegistroConDetalle.UI.Consulta
{
    /// <summary>
    /// Lógica de interacción para FormularioConsulta.xaml
    /// </summary>
    public partial class FormularioConsulta : Window
    {
        public FormularioConsulta()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click_1(object sender, RoutedEventArgs e)
        {
            var listado = new List<Personas>();
            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltrarComboBox.SelectedIndex)
                {
                    case 0:
                        listado = PersonasBLL.GetList(p => true);
                        break;

                    case 1:
                        int id;
                        id = Convert.ToInt32(CriterioTextBox.Text);
                        listado = PersonasBLL.GetList(p => p.PersonaId == id);

                        break;

                    case 2:
                        listado = PersonasBLL.GetList(p => p.Nombre == CriterioTextBox.Text);
                        break;
                }
            }
            else
            {
                listado = PersonasBLL.GetList(p => true);
            }
            ConsultaPersonasDataGrid.ItemsSource = null;
            ConsultaPersonasDataGrid.ItemsSource = listado;
        }
    }
    
}
