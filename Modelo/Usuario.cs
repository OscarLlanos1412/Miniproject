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
        public bool UsuariosNuevos()
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

                //Confirmación de guardado
                Console.Write("Desea guardarlo? (Aceptar - Cancelar) : ");
                string aceptar = Console.ReadLine();
                if (aceptar == "Aceptar" || aceptar == "aceptar" || aceptar == "ACEPTAR")
                {
                    //Consulta para insertar en la Base de datos
                    string consulGuar = "INSERT INTO persona VALUES('" + Docu + "', '" + Nombre + "', '" + Apellido + "', '" + Correo + "', '" + Clave + "', '" + Tipo_user + "')";
                    //Comprobar si la consulta se ejecuto de la mejor manera
                    if (conn.guardarSQL(consulGuar))
                    {
                        Console.Clear();
                        Console.WriteLine("Guardado con exito");
                        InicioController usuario = new InicioController();
                        return true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("No guardo");
                        InicioController usuario = new InicioController();
                        return false;
                    }
                }
                else if(aceptar == "Cancelar" || aceptar == "cancelar" || aceptar == "CANCELAR")
                {
                    Console.Clear();
                    Console.WriteLine("Usted cancelo la inserción");
                    InicioController usuario = new InicioController();
                    return false;
                    
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Digite opciones correctas, por favor");
                    InicioController usuario = new InicioController();
                    return false;
                }


            }
            //Obtencion de excepcion 
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("No puede ir vacios los campos y tiene que ser letras o numeros, dependiendo el campo requerido");
                InicioController usuario = new InicioController();
                return false;
            }
            catch (SqlException)
            {
                Console.WriteLine("Documento ya existe, ingrese otro por favor");
                InicioController usuario = new InicioController();
                return false;
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
            //Atrapar la excepcion
            catch(FormatException e)
            {
                Console.Write("No pueden ir valores vacios");
                Console.WriteLine("Que opciones desea realizar:\n 1. Actualizar Datos personales\n 2. Actualizar Clave\n 3. Agregar preguntas\n 4. Mostrar preguntas\n 5. Cerrar Sesion");
                int opcionesActualizar = Convert.ToInt32(Console.ReadLine());
                OpcionAccion(opcionesActualizar, docu);
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
                    Console.Write("Desea actualizar (Actualizar - Cancelar)? : ");
                    string actual = Console.ReadLine();
                    if (actual == "Actualizar" || actual == "ACTUALIZAR" || actual == "actualizar")
                    {
                        //Consulta para actualizar los datos
                        string conActual = "UPDATE persona SET nombre = '" + Nombre + "', apellido = '" + Apellido + "', correo = '" + Correo + "' WHERE documento = '" + docu + "'";
                        if (conn.ActualizarSql(conActual))
                        {
                            Console.WriteLine("Actualizo con exito su información ");
                        }
                        else
                        {
                            Console.WriteLine("No actualizo con exito");
                        }
                    }
                    else if (actual == "Cancelar" || actual == "CANCELAR" || actual == "cancelar")
                    {
                        Console.WriteLine("Cancelo la actualización");
                    }
                    break;

                case 2:
                    //Actualización de la clave
                    Console.Write("Digite clave: ");
                    Clave = Console.ReadLine();

                    //Confirmar actualización de la clave
                    Console.Write("Desea actualizar (Actualizar - Cancelar)? : ");
                    actual = Console.ReadLine();
                    if (actual == "Actualizar" || actual == "ACTUALIZAR" || actual == "actualizar")
                    {
                        //Consulta para actualizar la clave
                        string conActual = "UPDATE persona SET clave = '" + Clave + "' WHERE documento = '" + docu + "'";
                        if (conn.ActualizarSql(conActual))
                        {
                            Console.Clear();
                            Console.WriteLine("Actualizo con exito su clave, vuelva a logearse");
                            InicioController usuario = new InicioController();
                        }
                        else
                        {
                            Console.WriteLine("No actualizo con exito la clave");
                        }
                    }
                    else if (actual == "Cancelar" || actual == "CANCELAR" || actual == "cancelar")
                    {
                        Console.WriteLine("Cancelo la actualización");
                    }
                    break;

                case 3:
                    //Insertar preguntas 
                    Console.Write("Digita la pregunta que desea hacer: ");
                    string pregunta = Console.ReadLine();

                    //Consulta para insertar la pregunta
                    string consuPregunta = "INSERT INTO preguntas VALUES('"+ docu +"', '"+ pregunta +"')";
                    if (conn.guardarSQL(consuPregunta))
                    {
                        Console.WriteLine("Su pregunta se guardo exitosamente!");
                    }
                    else
                    {
                        Console.WriteLine("No se guardo la pregunta");
                    }
                    break;

                case 4:
                    //Mostrar las preguntas que ha guardado
                    //Consulta para seleccionar todas las preguntas con el documento logeado
                    string consuPreguntas = $"SELECT pregunta FROM preguntas WHERE documento = {docu}";
                    if (conn.verPreguntas(consuPreguntas))
                    {
                        //Atrapar execepciones
                        try
                        {
                            //Validar si hay preguntas relacionadas con ese documento
                            DataRowCollection vble = conn.mostrarDatos(consuPreguntas).Rows;
                            //Si es 0 no hay preguntas
                            if (vble.Count == 0)
                            {
                                Console.WriteLine("No hay preguntas relacionadas... Agrega preguntas!");
                                return;
                            }

                            //Mostrar las preguntas si hay preguntas con el documento logeado
                            foreach (DataRow item in vble)
                            {
                                Console.WriteLine("Las preguntas que usted a guardado son: ");
                                Console.WriteLine($"Pregunta : {item["pregunta"].ToString()}");
                            }
                        }
                        //Atapar las excepciones
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine("No hay preguntas vinculadas con tu documento");
                        }


                    }
                    else
                    {
                        Console.WriteLine("No ejecuto la consulta");
                    }                   
                    break;
            }
        }

    }
}
