using MvcPostInsigths.Helpers;
using MvcPostInsigths.Models;
using System.Xml.Linq;

namespace MvcPostInsigths.Repositories
{
    public class RepositoryJugadores
    {
        private HelperPathProvider helper;
        private XDocument documentJugadores;
        private string pathJugadores;

        public RepositoryJugadores(HelperPathProvider helper)
        {
            this.helper = helper;
            this.pathJugadores = this.helper.MapPath("jugadores.xml", Folders.Documents);
            documentJugadores = XDocument.Load(this.pathJugadores);
        }

        public Jugador FindJugador(string nombre, string contrasenia)
        {
            var consulta = from datos in this.documentJugadores.Descendants("jugador")
                           where datos.Element("nombre").Value == nombre && 
                           datos.Element("contrasenia").Value == contrasenia
                           select datos;

            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                XElement tag = consulta.FirstOrDefault();
                Jugador jug = new Jugador
                {
                    Id = int.Parse(tag.Element("id").Value),
                    Nombre = tag.Element("nombre").Value,
                    Contrasenia = tag.Element("contrasenia").Value,
                    Dorsal = int.Parse(tag.Element("dorsal").Value),
                    Edad = int.Parse(tag.Element("edad").Value),
                    Peso = int.Parse(tag.Element("peso").Value),
                    Altura = int.Parse(tag.Element("altura").Value)
                };

                return jug;
            }
        }
    }
}
