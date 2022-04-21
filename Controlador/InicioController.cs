using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miniproyecto.Modelo;
using Miniproyecto.Conexion;
using System.Data;

namespace Miniproyecto.Controlador
{
    public class InicioController
    {
        //Conexion a la Base de datos
        ConexionBD conn = new ConexionBD();
        
        //Metodo constructor para que inicie la app
        public InicioController()
        {
            Iniciar();
        }

        //Metodo donde tiene la interfaz primaria 
        private void Iniciar()
        {
            //Cambiar el color de la consola
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            
            //Bienvenida a la app
            Console.WriteLine("BIENVENIDO AL SISTEMA DE INICIO SESION POR CONSOLA \n Ya tienes cuenta? Si - No (Digita 'Salir' para cerrar la app): ");
            string respues = Console.ReadLine();

            if (respues == "Si" || respues == "SI" || respues == "si")
            {
                //Obtención del correo y clave
                Console.Write("Digita el correo: ");
                string cor = Console.ReadLine();
                Console.Write("Digita la clave: ");
                string clav = Console.ReadLine();
                //Validacion si los campos estan vacios
                if (cor == System.String.Empty && clav == System.String.Empty)
                {
                    Console.Clear();
                    Console.WriteLine("No pueden ir vacios los campos");
                    InicioController iniciar = new InicioController();
                }
                
                //Consulta para mostrar datos 
                string consultica = "SELECT * FROM persona WHERE correo = '" + cor + "' AND clave = '" + clav + "'";
                if (conn.SeleccionSql(consultica))
                {
                    Console.Clear();
                    foreach (DataRow item in conn.mostrarDatos(consultica).Rows)
                    {
                        //Validar que tipo de usuario es
                        if (Convert.ToInt32(item["id_tip_user"].ToString()) == 1)
                        {
                            Console.WriteLine("Bienvenido Administrador " + item["nombre"].ToString().ToUpper() + " " + item["apellido"].ToString().ToUpper());
                            DateTime initLogin = DateTime.Now;
                            Console.WriteLine("Ingreso a la app: " + initLogin);
                            Administrador admin = new Administrador();
                            admin.Acciones(Convert.ToInt32(item["documento"].ToString()));
                        }
                        else if (Convert.ToInt32(item["id_tip_user"].ToString()) == 2)
                        {
                            Console.WriteLine("Bienvenido Usuario " + item["nombre"].ToString().ToUpper() + " " + item["apellido"].ToString().ToUpper());
                            DateTime initLogin = DateTime.Now;
                            Console.WriteLine("Ingreso a la app: " + initLogin);
                            Usuario user = new Usuario();
                            string doc = item["documento"].ToString();
                            int docu2 = Convert.ToInt32(doc);
                            user.OpcionesUser(docu2);
                        }
                        else
                        {
                            Console.WriteLine("No se encuentra el usuario");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("No se ejecuto la consulta");
                }

            }
            else if (respues == "No" || respues == "NO" || respues == "no")
            {
                Console.Write("Desea Crear una cuenta? Si - No: ");
                string respues2 = Console.ReadLine();
                if (respues2 == "Si" || respues2 == "SI" || respues2 == "si")
                {
                    Usuario person = new Usuario();
                    person.UsuariosNuevos();
                }
                else if (respues2 == "No" || respues2 == "NO" || respues2 == "no")
                {
                    Console.Write("Digite 'Salir' para acabar con la app o 'Seguir' para reiniciar la app: ");
                    string salida = Console.ReadLine();
                    if (salida == "Salir" || salida == "SALIR" || salida == "salir")
                    {
                        Console.Clear();
                        Console.WriteLine("Gracias por utilzar nuestra app ");
                    }
                    else
                    {
                        Console.Clear();
                        //Reiniciar el programa
                        InicioController iniciar = new InicioController();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Digite una opcion correcta, por favor");
                    //Reiniciar el programa
                    InicioController iniciar = new InicioController();
                }
            }
            else if (respues == "Salir" || respues == "SALIR" || respues == "salir")
            {
                Console.Clear();
                Console.WriteLine("Gracias por utilzar nuestra app ");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Digite una opción por favor");
                //Reiniciar el programa
                InicioController iniciar = new InicioController();
            }
            
        }

    }
}
