using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Miniproyecto.Conexion
{
    public class ConexionBD
    {
        //Cadena de conexion
        private string CadenaConex = @"Data Source=DESKTOP-6ABCUND\OSCAR;Initial Catalog=inicio_sesion;User ID=sa; password= oscar1005753852; Integrated Security=True";
        private SqlConnection conn;
        private SqlCommand comd; //Representa un procesamiento almacenado o una instruccion sql y devuelve una respuesta

        //Listar tablas
        private SqlDataAdapter sda;
        private DataTable dtt;
        private DataSet dts; //-> Es una coleccion de datos que extrae de una tabla 

        private void Conectar()
        {
            conn = new SqlConnection(CadenaConex);
        }

        public ConexionBD()
        {
            //Esto lo que hace es que inicie siempre la funcion conectar
            Conectar();
        }

        //Inserta datos
        public bool guardarSQL(string sql)
        {
            try
            {
                comd = new SqlCommand(sql, conn);
                conn.Open();
                if (comd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        
        //Muestra los datos
        public DataTable mostrarDatos(string tabla)
        {
            try
            {
                string sql = tabla;
                conn.Open();
                sda = new SqlDataAdapter(sql, conn);
                dts = new DataSet();
                sda.Fill(dts, tabla);
                dtt = dts.Tables[tabla];
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return dtt;
        }

        //Selecciona datos
        public bool SeleccionSql(string sql)
        {
            try
            {
                comd = new SqlCommand(sql, conn);
                conn.Open();
                if (comd.ExecuteNonQuery() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        
        //Muestra las preguntas 
        public bool verPreguntas(string sql)
        {
            try
            {
                comd = new SqlCommand(sql, conn);
                conn.Open();
                if (comd.ExecuteNonQuery() > 0)
                {
                    
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        //Actualizar datos
        public bool ActualizarSql(string conActualizar)
        {
            try
            {
                comd = new SqlCommand(conActualizar, conn);
                conn.Open();
                if (comd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        //Eliminar datos
        public bool EliminarSql(string conEliminar)
        {
            try
            {
                comd = new SqlCommand(conEliminar, conn);
                conn.Open();
                if (comd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
