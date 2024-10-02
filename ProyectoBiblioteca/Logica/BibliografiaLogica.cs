using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ProyectoBiblioteca.Logica
{
    public class BibliografiaLogica
    {
        private static BibliografiaLogica instancia = null;

        public BibliografiaLogica()
        {

        }

        public static BibliografiaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new BibliografiaLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Bibliografia oBibliografia)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarBibliografia", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", oBibliografia.Descripcion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Modificar(Bibliografia oBibliografia)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarBibliografia", oConexion);
                    cmd.Parameters.AddWithValue("IdBibliografia", oBibliografia.IdBibliografia);
                    cmd.Parameters.AddWithValue("Descripcion", oBibliografia.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", oBibliografia.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }


        public List<Bibliografia> Listar()
        {
            List<Bibliografia> Lista = new List<Bibliografia>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdBibliografia,Descripcion,Estado from BIBLIOGRAFIA", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Bibliografia()
                            {
                                IdBibliografia = Convert.ToInt32(dr["IdBibliografia"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Bibliografia>();
                }
            }
            return Lista;
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("delete from BIBLIOGRAFIA where IdBibliografia = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

    }
}