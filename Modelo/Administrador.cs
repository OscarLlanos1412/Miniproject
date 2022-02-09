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
                        //Mostrar usuarios
                        Consultas consul = new Consultas();
                        consul.SeleccionInner("persona", "tipo_user", 2);
                        break;

                    case 2:
                        //Eliminar un usuario común en especifico 
                        Console.Write("Digite el documento el cual desea eliminar: ");
                        Docu = Convert.ToInt32(Console.ReadLine());
                        Consultas eliminar = new Consultas();
                        eliminar.EliminarUsers(0, Docu);
                        break;

                    case 3:
                        //Buscar un usuario común
                        Console.Write("Digite el documento el cual quiere ver: ");
                        Docu = int.Parse(Console.ReadLine());
                        Consultas consDocu = new Consultas();
                        consDocu.SeleccionInner("persona", "tipo_user", 0, Docu);
                        break;

                    case 4:
                        //Mostrar los administradores
                        Consultas mosAdmins = new Consultas();
                        mosAdmins.SeleccionInner("persona", "tipo_user", 1);
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

                        Consultas inserAdmin = new Consultas();
                        inserAdmin.InsertarUsers("persona", Docu, Nombre, Apellido, Correo, Clave, Tipo_user);
                        break;

                    case 6:
                        //Eliminar un administrador en concreto
                        Console.Write("Digite el documento del administrador el cual desea eliminar: ");
                        int documenAdmin = Convert.ToInt32(Console.ReadLine());
                        Consultas eliminAdmin = new Consultas();
                        eliminAdmin.EliminarUsers(documenAdmin);
                        break;

                    case 7:
                        //Actualizacion del mismo administrador
                        Console.Write("Digite nombre administrador: ");
                        Nombre = Console.ReadLine();
                        Console.Write("Digite apellido administrador: ");
                        Apellido = Console.ReadLine();
                        Console.Write("Digite correo administrador: ");
                        Correo = Console.ReadLine();
                        Consultas UpdaAdmon = new Consultas();
                        UpdaAdmon.UpdateUsers(docu, Nombre, Apellido, Correo);
                        break;

                    case 8:
                        //Actualizar clave del administrador
                        Console.Write("Digite clave: ");
                        Clave = Console.ReadLine();
                        Consultas UpdaAdmonPass = new Consultas();
                        UpdaAdmonPass.UpdateUsers(docu, "", "", "", Clave);
                        break;

                    case 9:
                        //Consultar un administrador en especifico
                        Console.Write("Digite el documento del administrador el cual quiere ver: ");
                        Docu = int.Parse(Console.ReadLine());
                        //Consulta para seleccionar el administrador
                        Consultas ConsDocuAdmons = new Consultas();
                        ConsDocuAdmons.SeleccionInner("persona", "tipo_user", 0, Docu);
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
