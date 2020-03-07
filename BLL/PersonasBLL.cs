using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistroConDetalle.Entidades;
using RegistroConDetalle.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroConDetalle.BLL
{
    public class PersonasBLL
    {
        public static bool Gurardar(Personas personas)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if(db.Personas.Add(personas) != null)
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Personas personas)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var Anterior = Buscar(personas.PersonaId);
                foreach (var item in Anterior.Telefonos)
                {
                    if(!personas.Telefonos.Exists(s=> s.TelId == item.TelId))
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach(var item in personas.Telefonos)
                {
                    var Estado = item.TelId>0? EntityState.Modified : EntityState.Added;
                    db.Entry(item).State = Estado;
                }

                db.Entry(personas).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Personas.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Personas Buscar(int id)
        {
            Personas persona = new Personas();
            Contexto db = new Contexto();

            try
            {
                persona = db.Personas.Where(s => s.PersonaId == id).
                Include(a => a.Telefonos).SingleOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return persona;
        }

        public static List<Personas> GetList(Expression<Func<Personas,bool>> persona)
        {
            List<Personas> lista = new List<Personas>();
            Contexto db = new Contexto();

            try
            {
                lista = db.Personas.Where(persona).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return lista;
        }
    }
}
