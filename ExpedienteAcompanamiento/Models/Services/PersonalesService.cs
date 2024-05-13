using ExpedienteAcompanamiento.Models.Entity;
using ExpedienteAcompanamiento.Utils;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Services
{
    public class PersonalesService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

        public static ResultObject ObtenerInformacionPersonal(int pidm)
        {
            ResultObject result = new ResultObject();
            try
            {

                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    cnx.Open();

                    OracleCommand comando = new OracleCommand();
                    comando.Connection = cnx;
                    comando.CommandText = @"GZ_BGSEXPE.F_GET_DATOS_PERSONALES";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.BindByName = true;

                    comando.Parameters.Add(new OracleParameter("salida", OracleDbType.Varchar2)
                    {
                        Size = 200,
                        Direction = ParameterDirection.ReturnValue
                    });

                    comando.Parameters.Add(new OracleParameter("PIDM", OracleDbType.Int16)
                    {
                        Value = pidm,
                        Size = 9
                    });

                    comando.Parameters.Add(new OracleParameter("c_datos_personales", OracleDbType.RefCursor)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    });

                    comando.ExecuteNonQuery();

                    // Revisamos si se pudo ejecutar la consulta
                    if (comando.Parameters["salida"].Value?.ToString() == "OP_EXITOSA")
                    {
                        OracleDataReader lector = ((OracleRefCursor)comando.Parameters["c_datos_personales"].Value).GetDataReader();
                        while (lector.Read())
                        {
                            result = new ResultObject()
                            {
                                Success = true,
                                Status = 200,
                                Value = new DatosInicio()
                                {
                                    matricula = lector["Matricula"]?.ToString(),
                                    nombreCompleto = lector["Nombre_Completo"]?.ToString(),
                                    fechaNacimiento = lector["Fecha_Nacimiento"]?.ToString(),
                                    ciudadOrigen = lector["Ciudad_Origen"]?.ToString(),
                                    estadoOrigen = lector["Estado_Origen"]?.ToString(),
                                    nivel = lector["Nivel"]?.ToString(),
                                    carrera = lector["Carrera"]?.ToString(),
                                    carreraAnterior = lector["Carrera_Anterior"]?.ToString(),
                                    dobleTitulacion = lector["Doble_Titulacion"]?.ToString(),
                                    etnia = lector["Etnia"]?.ToString(),
                                    genero = lector["Genero"]?.ToString(),
                                    periodoActual = lector["Periodo_Actual"]?.ToString(),                                    
                                    semestre = lector["Semestre"]?.ToString(),
                                    preparatoriaProcedencia = lector["Preparatoria_Procedencia"]?.ToString(),
                                    nacionalidad = lector["Nacionalidad"]?.ToString(),
                                }
                            };
                        }
                    }
                    else
                    {
                        result = new ResultObject()
                        {
                            Success = false,
                            Status = 700,
                            Message = comando.Parameters["salida"].Value?.ToString(),
                            uiMessage = "Ocurrio un error inesperado!"
                        };
                    }
                    cnx.Close();
                }
                return result;

            }
            catch (Exception ex)
            {
                return new ResultObject()
                {
                    Success = false,
                    Status = 500,
                    Message = ex.Message,
                    uiMessage = "Ocurrio un error inesperado!"
                };
            }
        }

        public static ResultObject ObtenerInformacionContactos(int pidm)
        {            
            ResultObject result = new ResultObject();
            try
            {

                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    cnx.Open();
                    
                    OracleCommand comando = new OracleCommand();
                    comando.Connection = cnx;
                    comando.CommandText = @"GZ_BGSEXPE.F_GET_DATOS_CONTACTOS";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.BindByName = true;

                    comando.Parameters.Add(new OracleParameter("salida", OracleDbType.Varchar2)
                    {
                        Size = 200,
                        Direction = ParameterDirection.ReturnValue
                    });

                    comando.Parameters.Add(new OracleParameter("PIDM", OracleDbType.Int16)
                    {
                        Value = pidm,
                        Size = 9
                    });

                    comando.Parameters.Add(new OracleParameter("c_datos_contactos", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    });

                    comando.ExecuteNonQuery();

                    // Revisamos si se pudo ejecutar la consulta
                    if (comando.Parameters["salida"].Value?.ToString() == "OP_EXITOSA")
                    {
                        OracleDataReader lector = ((OracleRefCursor)comando.Parameters["c_datos_contactos"].Value).GetDataReader();
                        while (lector.Read())
                        {
                            result = new ResultObject()
                            {
                                Success = true,
                                Status = 200,
                                Value = new InformacionGeneral()
                                {
                                    Direccion = lector["Direccion"]?.ToString(),
                                    Telefono = lector["Telefono"]?.ToString(),
                                    Direccion_Metropolitana = lector["Direccion_Metropolitana"]?.ToString(),
                                    Correo_Preferencia = lector["Correo_Preferencia"]?.ToString(),
                                    Correo_Personal = lector["Correo_Personal"]?.ToString(),
                                    Correo_UDEM = lector["Correo_UDEM"]?.ToString(),
                                    SGMM_UDEM = lector["SGMM_UDEM"]?.ToString(),
                                    SGMM_Particular = lector["SGMM_Particular"]?.ToString(),
                                    Nombre_Del_Padre = lector["Nombre_Del_Padre"]?.ToString(),
                                    Nombre_De_Madre = lector["Nombre_De_Madre"]?.ToString(),
                                    Nombre_Del_Tutor = lector["Nombre_Del_Tutor"]?.ToString(),
                                    Correo_Del_Padre = lector["Correo_Del_Padre"]?.ToString(),
                                    Correo_De_Madre = lector["Correo_De_Madre"]?.ToString(),
                                    Correo_De_Tutor = lector["Correo_De_Tutor"]?.ToString(),
                                    Tel_Del_Padre = lector["Tel_Del_Padre"]?.ToString(),
                                    Tel_De_Madre = lector["Tel_De_Madre"]?.ToString(),
                                    Tel_De_Tutor = lector["Tel_De_Tutor"]?.ToString(),
                                    Contacto_Emergencia = lector["Contacto_Emergencia"]?.ToString(),
                                    Tel_Emergencia = lector["Tel_Emergencia"]?.ToString(),
                                    Tel_DomicilioPermanente = lector["Tel_DomicilioPermanente"]?.ToString(),
                                }
                            };
                        }
                    }
                    else
                    {
                        result = new ResultObject()
                        {
                            Success = false,
                            Status = 700,
                            Message = comando.Parameters["salida"].Value?.ToString(),
                            uiMessage = "Ocurrio un error inesperado!"
                        };
                    }
                    cnx.Close();
                }
                return result;

            }catch(Exception ex)
            {
                return new ResultObject()
                {
                    Success = false,
                    Status = 500,
                    Message = ex.Message,
                    uiMessage = "Ocurrio un error inesperado!"
                };
            }
        }
    }
}