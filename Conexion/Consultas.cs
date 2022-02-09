using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miniproyecto.Controlador;

namespace Miniproyecto.Conexion
{
    class Consultas
    {
        ConexionBD conex = new ConexionBD();
        public void SeleccionInner(string tabla, string tabla2 = "", int tip_user = 0, int docu = 0)
        {
            if (tip_user != 0)
            {
                string con = $"SELECT nombre, apellido, correo, nom_tip_user FROM {tabla} " +
                             $"INNER JOIN {tabla2} ON {tabla}.id_tip_user = {tabla2}.id_tip_user WHERE {tabla}.id_tip_user = {tip_user}";
                //Comprobar si la consulta se ejecuta 
                if (conex.SeleccionSql(con))
                {
                    //Recorrer la tabla y mostrar todos los datos
                    foreach (DataRow item in conex.mostrarDatos(con).Rows)
                    {
                        Console.WriteLine("Nombre: " + item["nombre"].ToString() + " Apellido: " + item["apellido"].ToString() + " Correo: " + item["correo"].ToString() + " Tipo de usuario: " + item["nom_tip_user"].ToString());
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("No se ejecuto la consulta");
                }
            }
            else if(tabla2 != "")
            {
                string consulMostrar = $"SELECT nombre, apellido, correo, nom_tip_user FROM {tabla} INNER JOIN " +
                    $"{tabla2} ON {tabla}.id_tip_user = {tabla2}.id_tip_user WHERE documento = {docu}";
                if (conex.SeleccionSql(consulMostrar))
                {
                    foreach (DataRow item in conex.mostrarDatos(consulMostrar).Rows)
                    {
                        Console.WriteLine("Nombre: " + item["nombre"].ToString() + " Apellido: " + item["apellido"].ToString() + " Correo: " + item["correo"].ToString() + " Tipo de usuario: " + item["nom_tip_user"].ToString());
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("No se encontro el usuario");
                }
                
            }
            else if (tabla2 == "")
            {
                string consuPreguntas = $"SELECT pregunta FROM {tabla} WHERE documento = {docu}";
                if (conex.verPreguntas(consuPreguntas))
                {
                    //Atrapar execepciones
                    try
                    {
                        //Validar si hay preguntas relacionadas con ese documento
                        DataRowCollection vble = conex.mostrarDatos(consuPreguntas).Rows;
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
            }
            
        }
        
        public void EliminarUsers(int docuAdmin = 0, int docUser = 0)
        {
            if (docUser != 0)
            {
                Console.WriteLine("Esta seguro de eliminarlo (Eliminar - Cancelar): ");
                string resEliminarUser = Console.ReadLine().ToUpper();
                //Mensaje de comprobación para eliminar
                if (resEliminarUser == "ELIMINAR")
                {
                    //Consulta y comprobación de que se ejecuto la consulta
                    string consulEliminar = $"DELETE FROM persona WHERE documento = {docUser}";
                    if (conex.EliminarSql(consulEliminar))
                    {
                        Console.WriteLine("Eliminado con exito!");
                    }
                    else
                    {
                        Console.WriteLine("No se elimino");
                    }

                }
                //Si cancela la elimanción
                else if (resEliminarUser == "CANCELAR")
                {
                    Console.WriteLine("Cancelaste la eliminación");
                }
                else
                {
                    Console.WriteLine("No digito una opción valida");
                }
            }
            else if(docuAdmin != 0)
            {
                Console.WriteLine("Esta seguro de eliminarlo (Eliminar - Cancelar): ");
                string resEliminar = Console.ReadLine().ToUpper();
                //Comprobar si esta seguro de eliminarlo
                if (resEliminar == "ELIMINAR")
                {
                    //Consulta de eliminación
                    string consulEliminarAdminis = $"DELETE FROM persona WHERE documento = {docuAdmin}";
                    //Comprobar si la consulta se realizo exitosamente
                    if (conex.EliminarSql(consulEliminarAdminis))
                    {
                        Console.WriteLine("Se elimino el administrador con exito!");
                    }
                    else
                    {
                        Console.WriteLine("No se elimino el administrador");
                    }
                }
                else if (resEliminar == "CANCELAR")
                {
                    Console.WriteLine("Usted cancelo la eliminación del administrador");
                }
                else
                {
                    Console.WriteLine("No digito una opción valida");
                }

            }

        }
        
        public void InsertarUsers(string tabla, int docu, string name = "", string lastname = "", string email = "", string pass = "", int tip_user = 0, string questions = "")
        {
            if (tip_user == 1)
            {
                Console.Write("Desea guardar el nuevo administrador (Aceptar - Cancelar)? : ");
                string aceptar = Console.ReadLine().ToUpper();
                //Comprobar si acepto la inserción
                if (aceptar == "ACEPTAR")
                {
                    //Consulta para insertar en la tabla con valores obtenidos
                    string consulGuar = $"INSERT INTO {tabla} VALUES('{docu}', '{name}', '{lastname}', '{email}', '{pass}', '{tip_user}')";
                    //Comprobar si se ejecuto la cosulta correctamente
                    if (conex.guardarSQL(consulGuar))
                    {
                        Console.WriteLine("Se guardo con exito el administrador");
                    }
                    else
                    {
                        Console.WriteLine("No guardo el nuevo administrador");
                    }
                }
                //Si cancela la inserción
                else if (aceptar == "CANCELAR")
                {
                    Console.WriteLine("Cancelaste la insercion del nuevo administrador");
                }
                else
                {
                    Console.WriteLine("Digite valores correctos, por favor");
                }

            }
            else if (tip_user == 2)
            {
                Console.Write("Desea guardarlo? (Aceptar - Cancelar) : ");
                string aceptar = Console.ReadLine();
                if (aceptar == "Aceptar" || aceptar == "aceptar" || aceptar == "ACEPTAR")
                {
                    //Consulta para insertar en la Base de datos
                    string consulGuar = $"INSERT INTO {tabla} VALUES('{docu}', '{name}', '{lastname}', '{email}', '{pass}', '{tip_user}')";
                    //Comprobar si la consulta se ejecuto de la mejor manera
                    if (conex.guardarSQL(consulGuar))
                    {
                        Console.Clear();
                        Console.WriteLine("Guardado con exito");
                        InicioController usuario = new InicioController();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("No guardo");
                        InicioController usuario = new InicioController();
                    }
                }
                else if (aceptar == "Cancelar" || aceptar == "cancelar" || aceptar == "CANCELAR")
                {
                    Console.Clear();
                    Console.WriteLine("Usted cancelo la inserción");
                    InicioController usuario = new InicioController();

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Digite opciones correctas, por favor");
                    InicioController usuario = new InicioController();
                }
            }
            else if (questions != "")
            {
                string consuPregunta = $"INSERT INTO {tabla} VALUES('{docu}', '{questions}')";
                if (conex.guardarSQL(consuPregunta))
                {
                    Console.WriteLine("Su pregunta se guardo exitosamente!");
                }
                else
                {
                    Console.WriteLine("No se guardo la pregunta");
                }
            }

        }
    
        public void UpdateUsers(int docu, string name = "", string lastname = "", string email = "", string pass = "")
        {
            if (pass == "")
            {
                Console.Write("Desea actualizar (Actualizar - Cancelar)? : ");
                string actual = Console.ReadLine().ToUpper();
                //Comprobar que este seguro de actualizar
                if (actual == "ACTUALIZAR")
                {
                    //Consulta de actualización
                    string conActualAdminis = $"UPDATE persona SET nombre = '{name}', apellido = '{lastname}', " +
                        $"correo = '{email}' WHERE documento = {docu}";
                    //Comprobar si la consulta de actualización se ejecute correctamente
                    if (conex.ActualizarSql(conActualAdminis))
                    {
                        Console.WriteLine("Actualizo con exito su información ");
                    }
                    else
                    {
                        Console.WriteLine("No actualizo con exito");
                    }
                }
                else if (actual == "CANCELAR")
                {
                    Console.WriteLine("Cancelo la actualización");
                }
            }
            else if(pass != "")
            {
                Console.Write("Desea actualizar (Actualizar - Cancelar)? : ");
                string actual = Console.ReadLine().ToUpper();
                //Comprobar si esta seguro de actualizarlo
                if (actual == "ACTUALIZAR")
                {
                    //Consulta para la actualización de la clave
                    string conActualAdmin = $"UPDATE persona SET clave = '{pass}' WHERE documento = {docu}";
                    //Confirmar si la consulta se ejecuto bien
                    if (conex.ActualizarSql(conActualAdmin))
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
            }
            
        }
    }
}
