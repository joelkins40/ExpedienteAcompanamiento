using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpedienteAcompanamiento.Models.Entity;
using ExpedienteAcompanamiento.Utils;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace ExpedienteAcompanamiento.Models.Services
{

    public class ResidenciasAlertasService
    {

        private static readonly string _conString = ConfigurationManager.ConnectionStrings["AlertaResidencias"].ConnectionString;
        private static readonly string _conStringBanner = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;


        public static ResultObject obtenerInformacionAlertas(string matricula, string pidm)
        {
            ResultObject result = new ResultObject();
            try
            {
                var alertas = obtenerAlertas(matricula);
                var datosResidencias = obtenerInformacionDatosResidencias(pidm);

                result = new ResultObject()
                {
                    Success = true,
                    Status = 200,
                    Value = new
                    {
                        alertas,
                        datosResidencias
                    }
                };

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

        public static List<ResidenciasAlertas> obtenerAlertas(string matricula)
        {
            var residenciasAlertasList = new List<ResidenciasAlertas>();

            using (SqlConnection cnx = new SqlConnection(_conString))
            {
                cnx.Open();

                // 1.  create a command object identifying the stored procedure
                // SqlCommand cmd = new SqlCommand();
                //cmd.Connection = cnx;
                //cmd.CommandText = "prObtenerAlertasAvisos";
                //cmd.CommandType = CommandType.StoredProcedure;
                // 2. set the command object so it knows to execute a stored procedure

                SqlCommand cmd = new SqlCommand("prObtenerAlertasAvisos", cnx);
                cmd.CommandType = CommandType.StoredProcedure;



                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@Matricula", matricula));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        //Console.WriteLine("Product: {0,-35} Total: {1,2}", rdr["Matricula"], rdr["Total"]);
                        residenciasAlertasList.Add(new ResidenciasAlertas
                        {
                            Fecha_Salida = rdr["Fecha_Salida"]?.ToString(),
                            Fecha_Entrada = rdr["Fecha_Entrada"]?.ToString(),
                            Motivo = rdr["Motivo"]?.ToString(),
                            EstatusAviso = rdr["EstatusAviso"]?.ToString(),
                            Comentario = rdr["Comentario"]?.ToString(),
                            Fecha_Alta = rdr["Fecha_Alta"]?.ToString()

                        });


                    }
                }
                cnx.Close();

            }
            return residenciasAlertasList;
        }

        public static Residencias obtenerInformacionDatosResidencias(string pidm)
        {
            Residencias result = new Residencias();

            using (OracleConnection cnx = new OracleConnection(_conStringBanner))
            {
                List<EstadoCuenta> list = new List<EstadoCuenta>();
                List<ReporteConducta> listRC = new List<ReporteConducta>();
                List<Quejas> listQ = new List<Quejas>();
                cnx.Open();

                OracleCommand comando = new OracleCommand();
                comando.Connection = cnx;
                comando.CommandText = @"SZ_BGS_EXPE.F_DATOS_RESIDENCIAS";
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

                comando.Parameters.Add(new OracleParameter("c_datos_residencias", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                });

                comando.Parameters.Add(new OracleParameter("c_edocuenta", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                });

                comando.Parameters.Add(new OracleParameter("c_repoconducta", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                });

                comando.Parameters.Add(new OracleParameter("c_quejas", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                });

                comando.ExecuteNonQuery();

                // Revisamos si se pudo ejecutar la consulta
                if (comando.Parameters["salida"].Value?.ToString() == "OP_EXITOSA")
                {
                    OracleDataReader lectorQuejas = ((OracleRefCursor)comando.Parameters["c_quejas"].Value).GetDataReader();

                    while (lectorQuejas.Read())
                    {
                        listQ.Add(new Quejas()
                        {
                            Codigo = lectorQuejas.GetString(0),
                            Descripcion = lectorQuejas.GetString(1),
                        });
                    }

                    OracleDataReader lectorRpConducta = ((OracleRefCursor)comando.Parameters["c_repoconducta"].Value).GetDataReader();

                    while (lectorRpConducta.Read())
                    {
                        listRC.Add(new ReporteConducta()
                        {
                            Tipo = lectorRpConducta.GetString(0),
                            Folio = lectorRpConducta.GetInt32(1),
                            Fecha_Actividad = lectorRpConducta.GetDateTime(2),
                        });
                    }

                    OracleDataReader lectorEdoCuenta = ((OracleRefCursor)comando.Parameters["c_edocuenta"].Value).GetDataReader();
                    while (lectorEdoCuenta.Read())
                    {
                        list.Add(new EstadoCuenta()
                        {
                            Codigo = lectorEdoCuenta["CODIGO"]?.ToString(),
                            Descripcion = lectorEdoCuenta["DESCRIPCION"]?.ToString(),
                            Cantidad = lectorEdoCuenta.GetDouble(2),
                        });
                    }

                    OracleDataReader lector = ((OracleRefCursor)comando.Parameters["c_datos_residencias"].Value).GetDataReader();
                    while (lector.Read())
                    {
                        result = new Residencias()
                        {
                            Consecutivo_Periodo_Ingreso = lector["Consecutivo_Periodo"]?.ToString(),
                            Reservacion = lector["Reservacion"]?.ToString(),
                            Estancia = lector["Estancia"]?.ToString(),
                            Companero_Cuarto = lector["Compañero_Cuarto"]?.ToString(),
                            Habitacion_Asignada = lector["Habitacion"]?.ToString(),
                            Tiene_Carro = lector["Tiene_Carro"]?.ToString(),
                            Condicion_Suspencion = lector["Condicion_Suspencion"]?.ToString(),
                            Aviso_Ausencia = lector["Aviso_Ausencia"]?.ToString(),
                            Reporte_Conducta = lector["Reporte_Conducta"]?.ToString(),
                            Descuentos = lector["Descuentos"]?.ToString(),
                            Esquema_Pagos = lector["Esquema_Pagos"]?.ToString(),
                            EstadoCuentas = list,
                            ReportesConducta = listRC,
                            Quejas = listQ,
                        };
                    }
                }
                cnx.Close();
            }
            return result;
        }
    }
}

