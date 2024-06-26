﻿using ExpedienteAcompanamiento.Models.Entity;
using ExpedienteAcompanamiento.Utils;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ExpedienteAcompanamiento.Models.Services
{
    public class PersonalesService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;
        private static readonly string _conPeopleSoftString = ConfigurationManager.ConnectionStrings["PeopleSoft"].ConnectionString;

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
                    comando.CommandText = @"SZ_BGS_EXPE.F_GET_DATOS_PERSONALES";
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
                                    Foto = lector["Foto"]?.ToString(),

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
        public static ResultObject ObtenerInformacionDatosAdministrativos(int pidm)
        {
            ResultObject result = new ResultObject();
            DatosAdministrativos datosAdministrativos = new DatosAdministrativos();
            try
            {

                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    cnx.Open();

                    OracleCommand comando = new OracleCommand();
                    comando.Connection = cnx;
                    comando.CommandText = @"SZ_BGS_EXPE.F_DATOS_ADMINISTRATIVOS";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.BindByName = true;

                    comando.Parameters.Add(new OracleParameter("salida", OracleDbType.Varchar2)
                    {
                        Size = 300,
                        Direction = ParameterDirection.ReturnValue
                    });

                    comando.Parameters.Add(new OracleParameter("PIDM", OracleDbType.Int32)
                    {
                        Value = pidm,
                        Size = 9
                    });

                    comando.Parameters.Add(new OracleParameter("c_becasPeriodo", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    });

                    comando.Parameters.Add(new OracleParameter("c_datos_administrativos", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    });

                    comando.Parameters.Add(new OracleParameter("c_documentosEntregados", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    });
                    comando.Parameters.Add(new OracleParameter("C_BLOQUEOS", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    });
                    
                    comando.ExecuteNonQuery();

                    // Revisamos si se pudo ejecutar la consulta
                    if (comando.Parameters["salida"].Value?.ToString() == "OP_EXITOSA")
                    {
                        OracleDataReader lector1 = ((OracleRefCursor)comando.Parameters["c_becasPeriodo"].Value).GetDataReader();
                        while (lector1.Read())
                        {
                          datosAdministrativos.becasPeriodos.Add(new BecasPeriodos()
                            {
                              BECA_PERIODO = lector1["BECA_PERIODO"]?.ToString(),
                              BECA_NOMBRE = lector1["BECA_NOMBRE"]?.ToString(),
                              BECA_PORCENTAJE = lector1["BECA_PORCENTAJE"]?.ToString(),
                              BECA_TIPO  = lector1["BECA_TIPO"]?.ToString()

                          });
                        }
                        OracleDataReader lector2 = ((OracleRefCursor)comando.Parameters["c_datos_administrativos"].Value).GetDataReader();
                        while (lector2.Read())
                        {
                            datosAdministrativos.datosAdministrativos.Add(new Administrativos()
                            {

                                SEGURO_UDEM = lector2["SGMM"]?.ToString(),
                                SEGURO_PART = lector2["SGM_PARTICULAR"]?.ToString(),
                                BLOQUEO = lector2["BLOQUEO"]?.ToString(),
                                TERMINOS_COND = lector2["TERMINOS_COND"]?.ToString(),
                                TERMINOS_FECHA = lector2["TERMINOS_FECHA"]?.ToString(),
                                FORMADOR_NOMBRE = lector2["FORMADOR_NOMBRE"]?.ToString(),
                                FORMADOR_CORREO = lector2["FORMADOR_CORREO"]?.ToString(),
                                FORMADOR_PIDM = lector2["FORMADOR_MAT"]?.ToString(),

                                FORMADOR_PUESTO = ConsultarFormadorDatos(lector2["FORMADOR_MAT"]?.ToString()),
                               
                            });
                        }
                        OracleDataReader lector3 = ((OracleRefCursor)comando.Parameters["c_documentosEntregados"].Value).GetDataReader();
                        while (lector3.Read())
                        {
                            datosAdministrativos.DocumentosEntregados.Add(new DocumentosEntregados()
                            {
                                CODIGO =    lector3["CODIGO"]?.ToString(),
                                DESCRIPCION = lector3["DESCRIPCION"]?.ToString(),
                                ORIGEN = lector3["ORIGEN"]?.ToString(),

                            });
                        }

                        OracleDataReader lector4 = ((OracleRefCursor)comando.Parameters["C_BLOQUEOS"].Value).GetDataReader();
                        while (lector4.Read())
                        {
                            datosAdministrativos.bloqueos.Add(new Bloqueos()
                            {
                                stvhldd_desc = (lector4.IsDBNull(0) ? "0" : lector4.GetString(0)),
                               
                              

                            });
                        }
                        result = new ResultObject()
                        {

                            Success = true,
                            Status = 200,
                            Value = datosAdministrativos
                        };
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

        public static ResultObject ObtenerInformacionDatosAcademicos(int pidm,string token)
        {

            ResultObject result = new ResultObject();
            DatosAcademicos datosAdministrativos = new DatosAcademicos();
            try
            {

                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                      cnx.Open();
                    var si = cnx.GetSessionInfo();
                    si.DateFormat = "DD/MM/RRRR HH24:MI:SS"; // for English or ARABIC for Arabic
                    si.TimeStampFormat = "DD/MM/RRRR HH24:MI:SSXFF";
                    si.TimeStampTZFormat = "DD/MM/RRRR HH24:MI:SSXFF TZR";
                    cnx.SetSessionInfo(si);

                  

                    OracleCommand comando = new OracleCommand();
                    comando.Connection = cnx;
                    comando.CommandText = @"SZ_BGS_EXPE.F_DATOS_ACADEMICOS";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.BindByName = true;

                    comando.Parameters.Add(new OracleParameter("salida", OracleDbType.Varchar2)
                    {
                        Size = 200,
                        Direction = ParameterDirection.ReturnValue
                    });

                    comando.Parameters.Add(new OracleParameter("PIDM", OracleDbType.Int32)
                    {
                        Value = pidm,
                        Size = 9
                    });

                    comando.Parameters.Add(new OracleParameter("c_datos_academicos", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    });

                  

                    comando.Parameters.Add(new OracleParameter("C_TOEFL", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    });

                    comando.Parameters.Add(new OracleParameter("C_PROM_HIST", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    });
                    comando.ExecuteNonQuery();

                    // Revisamos si se pudo ejecutar la consulta
                    if (comando.Parameters["salida"].Value?.ToString() == "OP_EXITOSA")
                    {
                        OracleDataReader lector1 = ((OracleRefCursor)comando.Parameters["c_datos_academicos"].Value).GetDataReader();
                        while (lector1.Read())
                        {
                            datosAdministrativos.datosAcademicos.Add(new Academicos()
                            {
                                CREDITOS_REQUERIDOS = lector1["CREDITOS_REQUERIDOS"]?.ToString(),
                                CREDITOS_APROBADOS = lector1["CREDITOS_APROBADOS"]?.ToString(),
                                CREDITOS_PORCENTAJE = lector1["CREDITOS_PORCENTAJE"]?.ToString(),
                                PROGRAMA = lector1["PROGRAMA"]?.ToString(),
                                TOEFL_IND = lector1["TOEFL_IND"]?.ToString(),
                                SERV_SOCIAL = lector1["SERV_SOCIAL"]?.ToString(),
                                MATERIAS_BAJA = lector1["MATERIAS_BAJA"]?.ToString(),
                                PROM_INTEGRADO = lector1["PROM_INTEGRADO"]?.ToString(),
                                ESTATUS = lector1["ESTATUS"]?.ToString(),
                                MATERIAS_REP_1ER_PARCIAL = Convert.ToInt32(lector1["MATERIAS_REP_1ER_PARCIAL"]?.ToString()),
                                MATERIAS_REP_2DO_PARCIAL = Convert.ToInt32(lector1["MATERIAS_REP_2DO_PARCIAL"]?.ToString()),
                                FALTAS_1ER_PARCIAL = Convert.ToInt32(lector1["FALTAS_1ER_PARCIAL"]?.ToString()),
                                FALTAS_2DO_PARCIAL = Convert.ToInt32(lector1["FALTAS_2DO_PARCIAL"]?.ToString()),
                                PORCEN_MAT_REP_1ER_PARCIAL = lector1["PORCEN_MAT_REP_1ER_PARCIAL"]?.ToString(),
                                PORCEN_MAT_REP_2DO_PARCIAL = lector1["PORCEN_MAT_REP_2DO_PARCIAL"]?.ToString(),


                            });
                        }
                    
                        OracleDataReader lector3 = ((OracleRefCursor)comando.Parameters["C_TOEFL"].Value).GetDataReader();
                        while (lector3.Read())
                        {

                            datosAdministrativos.toefl.Add(new TOEFL()
                            {
                                SORTEST_TESC_CODE = (lector3.IsDBNull(0) ? "" : lector3.GetString(0)),
                                STVTESC_DESC = (lector3.IsDBNull(1) ? "" : lector3.GetString(1)),
                                SORTEST_TEST_DATE = (lector3.IsDBNull(2) ? "" : lector3.GetString(2)),
                                SORTEST_TEST_SCORE = (lector3.IsDBNull(3) ? "" : lector3.GetString(3)),

                            });
                        }

                        OracleDataReader lector2 = ((OracleRefCursor)comando.Parameters["C_PROM_HIST"].Value).GetDataReader();
                        while (lector2.Read())
                        {

                            datosAdministrativos.historico.Add(new HISTORICO()
                            {
                                GPA = (lector2.IsDBNull(0) ? "" :Convert.ToString(lector2.GetDouble(0))),
                                PERIODO = (lector2.IsDBNull(1) ? "" : lector2.GetString(1)),
                              
                            });
                        }
                        datosAdministrativos.UrlHorarios = obtenerUrlHorarios(token);
                        result = new ResultObject()
                        {

                            Success = true,
                            Status = 200,
                            Value = datosAdministrativos
                        };
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
        public static string  obtenerUrlHorarios(string token)
        {
            string server = WebConfigurationManager.AppSettings["URLHorarios"];
            //string Url = server + "/Login/Message" + "?token=HO" + token;
            string Url = server + "/Login/Message" + "?token=HO" + "bWF0cmljdWxhPTAwMDUxOTU3MCZwaW49NjU1MTg3";

            return Url;

        }
        public static ResultObject ObtenerInformacionDatosAdmision(int pidm)
        {            
            ResultObject result = new ResultObject();
            try
            {

                using (OracleConnection cnx = new OracleConnection(_conString))
                {
                    cnx.Open();
                    
                    OracleCommand comando = new OracleCommand();
                    comando.Connection = cnx;
                    comando.CommandText = @"SZ_BGS_EXPE.F_DATOS_ADMISION";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.BindByName = true;

                    comando.Parameters.Add(new OracleParameter("salida", OracleDbType.Varchar2)
                    {
                        Size = 300,
                        Direction = ParameterDirection.ReturnValue
                    });

                    comando.Parameters.Add(new OracleParameter("PIDM", OracleDbType.Int32)
                    {
                        Value = pidm,
                        Size = 9
                    });

                    comando.Parameters.Add(new OracleParameter("c_datos_admision", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    });

                    comando.ExecuteNonQuery();

                    // Revisamos si se pudo ejecutar la consulta
                    if (comando.Parameters["salida"].Value?.ToString() == "OP_EXITOSA")
                    {
                        OracleDataReader lector = ((OracleRefCursor)comando.Parameters["c_datos_admision"].Value).GetDataReader();
                        while (lector.Read())
                        {
                            result = new ResultObject()
                            {
                                Success = true,
                                Status = 200,
                                Value = new Admisiones()
                                {
                                    Periodo_Inicio = lector["Periodo_Inicio"]?.ToString(),
                                    PAA = lector["PAA"]?.ToString(),
                                    Puntaje_Verbal = lector["Puntaje_Verbal"]?.ToString(),
                                    Puntaje_Matematicas = lector["Puntaje_Matematicas"]?.ToString(),
                                    Promedio_Ingreso_Prepa = lector["Promedio_Ingreso_Prepa"]?.ToString(),
                                    Admision_Condicionada = lector["Admision_Condicionada"]?.ToString(),

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
                    comando.CommandText = @"SZ_BGS_EXPE.F_GET_DATOS_CONTACTOS";
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

        public static String ConsultarFormadorDatos(String formador)
        {
            Dictionary<string, dynamic> user = new Dictionary<string, dynamic>();
            String PuestoFormador = "";
            using (var conn = new OracleConnection(_conPeopleSoftString))
            {
                var command = new OracleCommand("SELECT * FROM sysadm.PS_UDEM_RHPS_VW WHERE ALTER_EMPLID = '" + formador + "'", conn);
                //command.Parameters.Add("@Matricula", matricula);

                conn.Open();
                try
                {
                    command.ExecuteNonQuery();

                    var lector = command.ExecuteReader();

                    while (lector.Read())
                    {
                      
                        PuestoFormador = lector.IsDBNull(17) ? " " : lector.GetString(17);

                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return PuestoFormador;
        }

    }
}