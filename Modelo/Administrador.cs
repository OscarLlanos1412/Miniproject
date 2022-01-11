using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miniproyecto.Conexion;
using Miniproyecto.Controlador;
using System.Data;
using System.Data.SqlClient;

namespace Miniproyecto.Modelo
{
    public class Administrador : Persona
    {
        //Conexion a la Base de datos
        ConexionBD conexion = new ConexionBD();

        //Metodo para digitar opciones para el administrador
        public void Acciones(int docu)
        {
            Console.WriteLine("Que opciones desea realizar:\n 1. Mostrar Usuarios\n 2. Eliminar usuario\n 3. Buscar Usuario\n 4. Mostrar Administradores\n 5. Agregar administrador\n 6. Eliminar Administrador\n 7. Actualizar datos personales\n 8. Actualizar Clave\n 9. Buscar Administrador\n 10. Cerrar Sesion: ");
            int op = Convert.ToInt32(Console.ReadLine());
            Opciones(op, docu);

            //Ciclo para que se repita las opciones hasta que sea igual a 10
            while (op != 10)
            {
                Console.WriteLine("Que opciones desea realizar:\n 1. Mostrar Usuarios\n 2. Eliminar usuario\n 3. Buscar Usuario\n 4. Mostrar Administradores\n 5. Agregar administrador\n 6. Eliminar Administrador\n 7. Actualizar datos personales\n 8. Actualizar Clave\n 9. Buscar Administrador\n 10. Cerrar Sesion: ");
                op = Convert.ToInt32(Console.ReadLine());
                Opciones(op, docu);
                if (op == 10)
                {
                    Console.Clear();
                    Console.WriteLine("Sesion cerrada");
                    InicioController admin = new InicioController();
                    break;
                }
                else
                {
                    continue;
                }
            }

            //Si el usuario inicia sesion y al instante cierra sesion
            if (op == 10)
            {
                Console.Clear();
                Console.WriteLine("Sesion cerrada");
                InicioController admin = new InicioController();
            }

        }

        //Metodo para obtener la opcion que digite el usuarios
        public void Opciones(int op, int docu)
        {
            //Hacemos un try para atrapar errores dentro de la ejecucion del codigo
            try
            {
                //Hacemos uso del swicth para obtener la opcion que ha digitado el usuario y haga el proceso de acuerdo a la opción
                switch (op)
                {
                    case 1:
                        //Mostrar todos los usuarios que sean usuarios comunes o clientes
                        string con = "SELECT nombre, apellido, correo, nom_tip_user FROM persona INNER JOIN tipo_user ON persona.id_tip_user = tipo_user.id_tip_user WHERE persona.id_tip_user = 2";
                        //Comprobar si la consulta se ejecuta 
                        if (conexion.SeleccionSql(con))
                        {
                            //Recorrer la tabla y mostrar todos los datos
                            foreach (DataRow item in conexion.mostrarDatos(con).Rows)
                            {
                                Console.WriteLine("Nombre: " + item["nombre"].ToString() + " Apellido: " + item["apellido"].ToString() + " Correo: " + item["correo"].ToString() + " Tipo de usuario: " + item["nom_tip_user"].ToString());
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("No se ejecuto la consulta");
                        }
                        break;

                    case 2:
                        //Eliminar un usuario común en especifico 
                        Console.Write("Digite el documento el cual desea eliminar: ");
                        Docu = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Esta seguro de eliminarlo (Eliminar - Cancelar): ");
                        string resEliminarUser = Console.ReadLine();
                        //Mensaje de comprobación para eliminar
                        if (resEliminarUser == "Eliminar" || resEliminarUser == "ELIMINAR" || resEliminarUser == "eliminar")
                        {
                            //Consulta y comprobación de que se ejecuto la consulta
                            string consulEliminar = "DELETE FROM persona WHERE documento = '" + Docu + "'";
                            if (conexion.EliminarSql(consulEliminar))
                            {
                                Console.WriteLine("Eliminado con exito!");
                            }
                            else
                            {
                                Console.WriteLine("No se elimino");
                            }
                            
                        }
                        //Si cancela la elimanción
                        else if (resEliminarUser == "Cancelar" || resEliminarUser == "CANCELAR" || resEliminarUser == "cancelar")
                        {
                            Console.WriteLine("Cancelaste la eliminación");
                        }
                        break;

                    case 3:
                        //Buscar un usuario común
                        Console.Write("Digite el documento el cual quiere ver: ");
                        Docu = int.Parse(Console.ReadLine());
                        string consulMostrar = "SELECT nombre, apellido, correo, nom_tip_user FROM persona INNER JOIN tipo_user ON persona.id_tip_user = tipo_user.id_tip_user WHERE documento = '"+ Docu +"'" ;
                        if (conexion.SeleccionSql(consulMostrar))
                        {
                            foreach (DataRow item in conexion.mostrarDatos(consulMostrar).Rows)
                            {
                                Console.WriteLine("Nombre: " + item["nombre"].ToString() + " Apellido: " + item["apellido"].ToString() + " Correo: " + item["correo"].ToString() + " Tipo de usuario: " + item["nom_tip_user"].ToString());
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("No se encontro el usuario");
                        }
                        break;

                    case 4:
                        //Mostrar los administradores
                        //Consulta usando inner join
                        string consMostrarAdmin = "SELECT nombre, apellido, correo, nom_tip_user FROM persona p INNER JOIN tipo_user t_u ON p.id_tip_user = t_u.id_tip_user WHERE p.id_tip_user = 1";
                        //Comprobar si se ejecuto con exito
                        if (conexion.SeleccionSql(consMostrarAdmin))
                        {
                            //Recorrer la tabal y mostrar datos
                            foreach (DataRow item in conexion.mostrarDatos(consMostrarAdmin).Rows)
                            {
                                Console.WriteLine("Nombre: " + item["nombre"].ToString() + " Apellido: " + item["apellido"].ToString() + " Correo: " + item["correo"].ToString() + " Tipo de usuario: " + item["nom_tip_user"].ToString());
                            }
                            Console.WriteLine();

                        }
                        else
                        {
                            Console.WriteLine("No se ejecuto la consulta");
                        }
                        break;

                    case 5:
                        //Registrar un nuevo administrador
                        //Obtencion de datos del nuevo administrador
                        Console.WriteLine("Podra registrar el administrador nuevo a continuación");
                        Console.Write("Digite documento del administrador: ");
                        Docu = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Digite nombre del administrador: ");
                        Nombre = Console.ReadLine();
                        Console.Write("Digite apellido del administrador: ");
                        Apellido = Console.ReadLine();
                        Console.Write("Digite correo del administrador: ");
                        Correo = Console.ReadLine();
                        Console.Write("Digite clave del administrador: ");
                        Clave = Console.ReadLine();
                        Tipo_user = 1;

                        //Confirmar si esta seguro de guardar
                        Console.Write("Desea guardar el nuevo administrador (Aceptar - Cancelar)? : ");
                        string aceptar = Console.ReadLine();
                        //Comprobar si acepto la inserción
                        if (aceptar == "Aceptar" || aceptar == "aceptar" || aceptar == "ACEPTAR")
                        {
                            //Consulta para insertar en la tabla con valores obtenidos
                            string consulGuar = "INSERT INTO persona VALUES('" + Docu + "', '" + Nombre + "', '" + Apellido + "', '" + Correo + "', '" + Clave + "', '" + Tipo_user + "')";
                            //Comprobar si se ejecuto la cosulta correctamente
                            if (conexion.guardarSQL(consulGuar))
                            {
                                Console.WriteLine("Se guardo con exito el administrador");
                            }
                            else
                            {
                                Console.WriteLine("No guardo el nuevo administrador");
                            }
                        }
                        //Si cancela la inserción
                        else if (aceptar == "Cancelar" || aceptar == "cancelar" || aceptar == "CANCELAR")
                        {
                            Console.WriteLine("Cancelaste la insercion del nuevo administrador");
                        }
                        else
                        {
                            Console.WriteLine("Digite valores correctos, por favor");
                        }
                        break;

                    case 6:
                        //Eliminar un administrador en concreto
                        Console.Write("Digite el documento del administrador el cual desea eliminar: ");
                        int documenAdmin = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Esta seguro de eliminarlo (Eliminar - Cancelar): ");
                        string resEliminar = Console.ReadLine();
                        //Comprobar si esta seguro de eliminarlo
                        if (resEliminar == "Eliminar" || resEliminar == "ELIMINAR" || resEliminar == "eliminar")
                        {
                            //Consulta de eliminación
                            string consulEliminarAdminis = "DELETE FROM persona WHERE documento = '" + documenAdmin + "'";
                            //Comprobar si la consulta se realizo exitosamente
                            if (conexion.EliminarSql(consulEliminarAdminis))
                            {
                                Console.WriteLine("Se elimino el administrador con exito!");
                            }
                            else
                            {
                                Console.WriteLine("No se elimino el administrador");
                            }
                        }
                        else if (resEliminar == "Cancelar" || resEliminar == "CANCELAR" || resEliminar == "cancelar")
                        {
                            Console.WriteLine("Usted cancelo la eliminación del administrador");
                        }
                        else
                        {
                            Console.WriteLine("No digito una opción valida");
                        }
                        
                        break;

                    case 7:
                        //Actualizacion del mismo administrador
                        Console.Write("Digite nombre administrador: ");
                        Nombre = Console.ReadLine();
                        Console.Write("Digite apellido administrador: ");
                        Apellido = Console.ReadLine();
                        Console.Write("Digite correo administrador: ");
                        Correo = Console.ReadLine();
                        Console.Write("Desea actualizar (Actualizar - Cancelar)? : ");
                        string actual = Console.ReadLine();
                        //Comprobar que este seguro de actualizar
                        if (actual == "Actualizar" || actual == "ACTUALIZAR" || actual == "actualizar")
                        {
                            //Consulta de actualización
                            string conActualAdminis = "UPDATE persona SET nombre = '" + Nombre + "', apellido = '" + Apellido + "', correo = '" + Correo + "' WHERE documento = '" + docu + "'";
                            //Comprobar si la consulta de actualización se ejecute correctamente
                            if (conexion.ActualizarSql(conActualAdminis))
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

                    case 8:
                        //Actualizar clave del administrador
                        Console.Write("Digite clave: ");
                        Clave = Console.ReadLine();
                        Console.Write("Desea actualizar (Actualizar - Cancelar)? : ");
                        actual = Console.ReadLine();
                        //Comprobar si esta seguro de actualizarlo
                        if (actual == "Actualizar" || actual == "ACTUALIZAR" || actual == "actualizar")
                        {
                            //Consulta para la actualización de la clave
                            string conActualAdmin = "UPDATE persona SET clave = '" + Clave + "' WHERE documento = '" + docu + "'";
                            //Confirmar si la consulta se ejecuto bien
                            if (conexion.ActualizarSql(conActualAdmin))
                            {
                                //Cerrar sesion porque si cambio la clave
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

                    case 9:
                        //Consultar un administrador en especifico
                        Console.Write("Digite el documento del administrador el cual quiere ver: ");
                        Docu = int.Parse(Console.ReadLine());
                        //Consulta para seleccionar el administrador
                        string consulMostrarAdmin = "SELECT nombre, apellido, correo, nom_tip_user FROM persona p INNER JOIN tipo_user t_u ON p.id_tip_user = t_u.id_tip_user WHERE p.documento = '" + Docu + "'";
                        //Efectuar la ejecución de la consulta
                        if (conexion.SeleccionSql(consulMostrarAdmin))
                        {
                            //Mostrar datos del administrador en especifico
                            foreach (DataRow item in conexion.mostrarDatos(consulMostrarAdmin).Rows)
                            {
                                Console.WriteLine("Nombre: " + item["nombre"].ToString() + " Apellido: " + item["apellido"].ToString() + " Correo: " + item["correo"].ToString() + " Tipo de usuario: " + item["nom_tip_user"].ToString());
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("No se encontro el administrador");
                        }
                        break;
                }
            }
            //Atrapar excepciones que se pueden encontrar 
            catch (SqlException e)
            {
                Console.WriteLine("Se produjo un error en la BD " + e);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Hay un error en la consulta " + e);
            }
            
        }
    }
}
