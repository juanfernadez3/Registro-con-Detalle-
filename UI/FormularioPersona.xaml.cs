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

namespace RegistroConDetalle1.UI
{
    /// <summary>
    /// Lógica de interacción para FormularioPersona.xaml
    /// </summary>
    public partial class FormularioPersona : Window
    {
        public List<TelefonosDetalle> Detalles {get; set;}
        public FormularioPersona()
        {
            InitializeComponent();
            Detalles = new List<TelefonosDetalle>();
        }
        private void Limpiar()
        {
            PersoaIdTextBox.Text = "0";
            NombreTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            CedulaTextBox.Text = string.Empty;
            FechaNacimientoDateTimePicker.SelectedDate = DateTime.Now;

            Detalles = new List<TelefonosDetalle>();
            CargarGrid();
        }

        private Personas LlenaClase()
        {
            Personas persona = new Personas();

            persona.PersonaId = Convert.ToInt32(PersoaIdTextBox.Text);
            persona.Nombre = NombreTextBox.Text;
            persona.Direcion = DireccionTextBox.Text;
            persona.Cedula = CedulaTextBox.Text;
            persona.FechaNacimineto = (DateTime)FechaNacimientoDateTimePicker.SelectedDate;
            persona.Telefonos = Detalles;

            return persona;
        }

        private void LlenaCampo(Personas persona)
        {
            PersoaIdTextBox.Text = Convert.ToString(persona.PersonaId);
            NombreTextBox.Text = persona.Nombre;
            DireccionTextBox.Text = persona.Direcion;
            CedulaTextBox.Text = persona.Cedula;
            FechaNacimientoDateTimePicker.SelectedDate = persona.FechaNacimineto;
            Detalles = persona.Telefonos;
            CargarGridBudqueda(Convert.ToInt32(PersoaIdTextBox.Text));
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrEmpty(NombreTextBox.Text))
            {
                MessageBox.Show("Debe Ingresar Nombre", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                NombreTextBox.Focus();
                paso = false;
            }
            if (string.IsNullOrEmpty(DireccionTextBox.Text))
            {
                MessageBox.Show("Debe Ingresar Direccion", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                DireccionTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrEmpty(CedulaTextBox.Text))
            {
                MessageBox.Show("Debe Ingresar Cedula", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                CedulaTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrEmpty(FechaNacimientoDateTimePicker.Text))
            {
                MessageBox.Show("Debe Ingresar Fecha de Nacimiento", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                FechaNacimientoDateTimePicker.Focus();
                paso = false;
            }
            if (Detalles.Count == 0)
            {
                MessageBox.Show("Debe agregar almenos una Telefono");
                TelefonoTextBox.Focus();
                paso = false;

            }
            return paso;
        }
        private void CargarGridBudqueda(int id)
        {
            Personas persona = new Personas();
            persona = PersonasBLL.Buscar(id);
            if (persona != null)
            {
                Detalles = persona.Telefonos;
                TelefonoDataGrid.ItemsSource = null;
                TelefonoDataGrid.ItemsSource = Detalles;
            }
            else
            {
                Console.WriteLine("No existe persona");
            }
        }
        private bool ExisteEnBaseDeDatos()
        {
            Personas persona = PersonasBLL.Buscar(Convert.ToInt32(PersoaIdTextBox.Text));
            return (persona != null);
        }
        private void CargarGrid()
        {
            //this.TelefonoDataGrid.AutoGenerateColumns = false;
            TelefonoDataGrid.ItemsSource = null;
            TelefonoDataGrid.ItemsSource = Detalles;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Personas persona = new Personas();
            int.TryParse(PersoaIdTextBox.Text, out id);
            Limpiar();

            persona = PersonasBLL.Buscar(id);


            if (persona != null)
            {
                LlenaCampo(persona);
            }
            else
            {
                MessageBox.Show("No se puedo encontrar");
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (TelefonoDataGrid.DataContext != null)
                Detalles = (List<TelefonosDetalle>)TelefonoDataGrid.DataContext;

            Detalles.Add(new TelefonosDetalle
            {
                //TipoTelefono = TipoTextBox.Text,
                Telefono = TelefonoTextBox.Text
            });
            CargarGrid();
            TelefonoTextBox.Focus();
            TipoTextBox.Text = string.Empty;
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if (TelefonoDataGrid.Items.Count > 0 && TelefonoDataGrid.SelectedItem != null)
            {
                Detalles.RemoveAt(TelefonoDataGrid.SelectedIndex);
                CargarGrid();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Personas persona;
            bool paso = false;

            if (!Validar())
                return;

            persona = LlenaClase();

            if (PersoaIdTextBox.Text == "0")
                paso = PersonasBLL.Gurardar(persona);
            else
            {
                if (!ExisteEnBaseDeDatos())
                {
                    MessageBox.Show("Persona no existente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = PersonasBLL.Modificar(persona);
            }

            if (paso)
            {
                MessageBox.Show("Guardado con Exito");
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error, No Guardado");
            }
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(PersoaIdTextBox.Text, out id);
            Limpiar();
            if (PersonasBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado con Exito", "Eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No Eliminado ", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }
    }
}
