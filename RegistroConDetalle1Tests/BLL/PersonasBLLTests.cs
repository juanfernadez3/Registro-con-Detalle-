using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroConDetalle.BLL;
using System;
using System.Collections.Generic;
using System.Text;
using RegistroConDetalle.Entidades;

namespace RegistroConDetalle.BLL.Tests
{
    [TestClass()]
    public class PersonasBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Personas personas = new Personas();
            bool paso = false;
            personas.PersonaId = 0;
            personas.Nombre = "prueba 1 Guardar";
            personas.Direcion = "Base de datos";
            personas.FechaNacimineto = DateTime.Now;

            personas.Telefonos.Add(new TelefonosDetalle
            {
                //TipoTelefono = "universidad",
                Telefono = "809-588-3505"
            });
            paso = PersonasBLL.Gurardar(personas);

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Personas personas = new Personas();
            bool paso = false;

            personas.PersonaId = 4;
            personas.Nombre = "prueba 2";
            personas.Direcion = "base de datos";
            personas.FechaNacimineto = DateTime.Now;
            paso = PersonasBLL.Modificar(personas);

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Personas personas = new Personas();
            bool paso = false;
            personas.PersonaId = 1;

            int id = Convert.ToInt32(personas.PersonaId);

            paso = PersonasBLL.Eliminar(id);

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.Fail();
        }
    }
}
