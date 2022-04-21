using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miniproyecto.Conexion;
using Miniproyecto.Controlador;

namespace Miniproyecto.Modelo
{
    public class Usuario : Persona
    {
        //Conexion de la Base de datos
        ConexionBD conn = new ConexionBD();

        //Metodo para agregar usuarios nuevos
        public void UsuariosNuevos()
        {
            //Manejo de excepciones 
            try
            {
                //Obtención de datos para agregar usuario
                Console.Write("Digite documento: ");
                Docu = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite nombre: ");
                Nombre = Console.ReadLine();
                Console.Write("Digite apellido: ");
                Apellido = Console.ReadLine();
                Console.Write("Digite correo: ");
                Correo = Console.ReadLine();
                Console.Write("Digite clave: ");
                Clave = Console.ReadLine();
                Tipo_user = 2;

                Consultas inserUsers = new Consultas();
                inserUsers.InsertarUsers("persona", Docu, Nombre, Apellido, Correo, Clave, Tipo_user);

            }
            //Obtencion de excepcion 
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("No puede ir vacios los campos y tiene que ser letras o numeros, dependiendo el campo requerido");
                InicioController usuario = new InicioController();
            }
            catch (SqlException)
            {
                Console.WriteLine("Documento ya existe, ingrese otro por favor");
                InicioController usuario = new InicioController();
            }
        }

        //Metodo de opciones para usuarios
        public void OpcionesUser(int docu)
        {
            //Captura de excepciones
            try 
            {
                //Opciones que puede realizar el usuarios
                Console.WriteLine("Que opciones desea realizar:\n 1. Actualizar Datos personales\n 2. Actualizar Clave\n 3. Agregar preguntas\n 4. Mostrar preguntas\n 5. Cerrar Sesion");
                int opcionesActualizar = Convert.ToInt32(Console.ReadLine());
                //Llamado de metodo para la acción de cada opción 
                OpcionAccion(opcionesActualizar, docu);
                //Ciclo para que se repitan las opciones hasta que sea igual a la opcion de cerrar sesion
                while (opcionesActualizar != 5)
                {
                    Console.WriteLine("Que opciones desea realizar:\n 1. Actualizar Datos personales\n 2. Actualizar Clave\n 3. Agregar preguntas\n 4. Mostrar preguntas\n 5. Cerrar Sesion");
                    opcionesActualizar = Convert.ToInt32(Console.ReadLine());
                    OpcionAccion(opcionesActualizar, docu);
                    if (opcionesActualizar == 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Sesion cerrada");
                        InicioController usuario = new InicioController();
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                //Si el usuario inicia sesion y al mismo instante cierra sesion
                if (opcionesActualizar == 5)
                {
                    Console.Clear();
                    Console.WriteLine("Sesion cerrada");
                    InicioController usuario = new InicioController();
                }

            }
            catch(Exception)
            {
                Console.WriteLine("No pueden ir valores vacios.".ToUpper());
                Console.WriteLine("Que opciones desea realizar:\n 1. Actualizar Datos personales\n 2. Actualizar Clave\n 3. Agregar preguntas\n 4. Mostrar preguntas\n 5. Cerrar Sesion");
                int opcionesActualizar = Convert.ToInt32(Console.ReadLine());
                OpcionAccion(opcionesActualizar, docu);
                while (opcionesActualizar != 5)
                {
                    Console.WriteLine("Que opciones desea realizar:\n 1. Actualizar Datos personales\n 2. Actualizar Clave\n 3. Agregar preguntas\n 4. Mostrar preguntas\n 5. Cerrar Sesion");
                    opcionesActualizar = Convert.ToInt32(Console.ReadLine());
                    OpcionAccion(opcionesActualizar, docu);
                    
                }
                if (opcionesActualizar == 5)
                {
                    Console.Clear();
                    Console.WriteLine("Sesion cerrada");
                    InicioController usuario = new InicioController();
                }
            }
        }

        //Metodo para la acción de las opciones
        public void OpcionAccion(int accion, int docu)
        {
            //Hacemos uso del switch para obtener la opción del usuario
            switch (accion)
            {
                case 1:
                    //Actualizar datos personales
                    Console.Write("Digite nombre: ");
                    Nombre = Console.ReadLine();
                    Console.Write("Digite apellido: ");
                    Apellido = Console.ReadLine();
                    Console.Write("Digite correo: ");
                    Correo = Console.ReadLine();

                    //Confirmación para actualizar 
                    Consultas UpdateUser = new Consultas();
                    UpdateUser.UpdateUsers(docu, Nombre, Apellido, Correo);
                    break;

                case 2:
                    //Actualización de la clave
                    Console.Write("Digite clave: ");
                    Clave = Console.ReadLine();

                    //Confirmar actualización de la clave
                    Consultas UpdaUsersPass = new Consultas();
                    UpdaUsersPass.UpdateUsers(docu, "", "", "", Clave);
                    break;

                case 3:
                    //Insertar preguntas 
                    Console.Write("Digita la pregunta que desea hacer: ");
                    string pregunta = Console.ReadLine();
                    Consultas InserQuestion = new Consultas();
                    InserQuestion.InsertarUsers("preguntas", docu, "", "", "", "", 0, pregunta);
                    //Consulta para insertar la pregunta
                    
                    break;

                case 4:
                    //Mostrar las preguntas que ha guardado
                    //Consulta para seleccionar todas las preguntas con el documento logeado
                    Consultas Questions = new Consultas();
                    Questions.SeleccionInner("preguntas", "", 0, docu);
                    break;
            }
        }

    }
}
