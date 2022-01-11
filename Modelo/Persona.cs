using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miniproyecto.Conexion;

namespace Miniproyecto.Modelo
{
    //Clase molde
    public class Persona
    {
        private int docu;
        private string nombre;
        private string apellido;
        private string correo;
        private string clave;
        private int tipo_user;

        public int Docu { get => docu; set => docu = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Clave { get => clave; set => clave = value; }
        public int Tipo_user { get => tipo_user; set => tipo_user = value; }

        public Persona()
        {
            
        }
        public Persona(int docu, string nombre, string apellido, string correo, string clave, int tipo_user)
        {
            this.Docu = docu;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Correo = correo;
            this.Clave = clave;
            this.Tipo_user = tipo_user;
        }

    }
}
