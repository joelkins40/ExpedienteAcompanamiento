using ExpedienteAcompanamiento.Models.Entity;
using ExpedienteAcompanamiento.Models.Entity.SIAT;
using ExpedienteAcompanamiento.Utils;
using Microsoft.Ajax.Utilities;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Services
{
    public class ReportesService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["SIAT"].ConnectionString;
        private static readonly string _conStringBanner = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

        public static ResultObject ObtenerAlertas(int pidm, string term)
        {
            try
            {
                pidm = 778111;
                var alerts = ObtenerReportes(pidm);

                var becas = ObtenerBecas(pidm, term);
                var becasLinde = ObtenerBecasLinde(pidm, term);

                // TODO: Becas
                foreach (var alert in alerts)
                {
                    alert.Beca = becas.Count > 0 ? "Si" : "No";
                    alert.Canalizacion = ObtenerAsignacion(alert.IdReporte);
                    alert.GeneroReporte = ObtenerTipoRegistro(alert.TipoRegistro).RegistroDesc;
                    alert.Estatus = ObtenerEstatus(alert.IdReporte);
                    alert.Comentarios = ObtenerComentarios(alert.IdReporte);
                    alert.ApoyoLINDE = becasLinde != null && becasLinde.ToUpper().Contains("LINDE") ? "Y" : "N";
                }

                return new ResultObject() { Success = true, Value = alerts };
            } 
            catch(Exception ex)
            {
                return new ResultObject()
                {
                    Success = false,
                    Status = 500,
                    Message = ex.Message,
                    uiMessage = "Ocurrio un error inesperado al obtener las alertas!"
                };
            }
        }

        public static List<ReportResult> ObtenerReportes(int pidm)
        {
            string query = "SELECT IDENTIFICADOR, IDBN_ALUMNO, NOMBRE_ALUMNO, " +
                "CARRERA_ALUMNO, ESCUELA_ALUMNO, CORREO_ALUMNO, CELULAR_ALUMNO, " +
                "SEMESTRE_ALUMNO, FECHA_REGISTRO, CRN, MATERIA, IDBN_PROF, " +
                "NOMBRE_PROF, CORREO_PROF, PREGUNTA3, PREGUNTA2, CORREO_DPA, TIPO_REGISTRO " +
                "PREGUNTA1_RESP1, PREGUNTA1_RESP2, PREGUNTA1_RESP3, PREGUNTA1_RESP4, PREGUNTA1_RESP5, " +
                "PREGUNTA1_RESP6, PREGUNTA1_RESP7, PREGUNTA1_RESP8, PREGUNTA1_RESP9, PREGUNTA1_RESP10, " +
                "PREGUNTA1_RESP10_2 " +
                "FROM TBL_SIAT_RESPUESTAS WHERE PIDM_ALUMNO = " + pidm;
            List<ReportResult> list = new List<ReportResult>();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand(query, conn)
                {

                })
                {
                    conn.Open();



                    try
                    {

                        command.ExecuteNonQuery();

                        SqlDataReader lector = command.ExecuteReader();

                        while (lector.Read())
                        {
                            list.Add(new ReportResult() { 
                                IdReporte = lector.GetInt32(0),
                                Matricula = lector.GetString(1),
                                NombreAlumno = lector.GetString(2),
                                Carrera = lector.IsDBNull(3) ? string.Empty : lector.GetString(3),
                                Escuela = lector.IsDBNull(4) ? string.Empty : lector.GetString(4),
                                CorreoAlumno = lector.IsDBNull(5) ? string.Empty : lector.GetString(5),
                                Telefono = lector.IsDBNull(6) ? string.Empty : lector.GetString(6),
                                Grado = lector.IsDBNull(7) ? string.Empty : lector.GetString(7),
                                FechaRegistro = lector.IsDBNull(8) ? (DateTime?)null : lector.GetDateTime(8),
                                Crn = lector.IsDBNull(9) ? string.Empty : lector.GetString(9),
                                Materia = lector.IsDBNull(10) ? string.Empty : lector.GetString(10),
                                MatriculaMaestro = lector.GetString(11),
                                NombreMaestro = lector.GetString(12),
                                CorreoMaestro = lector.IsDBNull(13) ? string.Empty : lector.GetString(13),
                                AlumnoEnterado = lector.IsDBNull(14) ? string.Empty : lector.GetString(14),
                                ComentariosMaestro = lector.IsDBNull(15) ? string.Empty : lector.GetString(15),
                                CorreoDPA = lector.IsDBNull(16) ? string.Empty : lector.GetString(16),
                                TipoRegistro = lector.GetString(17),
                                Motivos = Regex.Replace((!lector.IsDBNull(18) && lector.GetString(18).Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                                    ? "Ausencias a clase"
                                    : "") + "," + (!lector.IsDBNull(19) && lector.GetString(19).Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                                    ? "Bajo desempeño académico"
                                    : "") + "," + (!lector.IsDBNull(20) && lector.GetString(20).Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                                    ? "Presenta comportamiento atípico"
                                    : "") + "," + (!lector.IsDBNull(21) && lector.GetString(21).Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                                    ? "Requiere habilidades y métodos de estudio - organización del tiempo"
                                    : "") + "," + (!lector.IsDBNull(22) && lector.GetString(22).Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                                    ? "Dificultad para adaptarse"
                                    : "") + "," + (!lector.IsDBNull(23) && lector.GetString(23).Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                                    ? "Reporta situaciones de salud que dificultan la realización de actividades diarias"
                                    : "") + "," + (!lector.IsDBNull(24) && lector.GetString(24).Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                                    ? "Dificultades económicas"
                                    : "") + "," + (!lector.IsDBNull(25) && lector.GetString(25).Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                                    ? "Dudas de carrera y/o requiere orientación vocacional"
                                    : "") + "," + (!lector.IsDBNull(26) && lector.GetString(26).Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                                    ? "Presión por mantener la beca"
                                    : "") + "," + (!lector.IsDBNull(27) && lector.GetString(27).Equals("Y", StringComparison.InvariantCultureIgnoreCase)
                                    ? "Otra: " + (lector.IsDBNull(28) ? string.Empty : lector.GetString(28))
                                    : ""), ",{2,}", ",")
                                .Trim(',')
                                .Split(',')
                                .ToList(),
                            });
                        }
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();
                    }
                };
            }
            return list;
        }

        public static List<int> ObtenerBecas(int pidm, string term)
        {
            var students = new List<int>();
            try
            {
                using (var connection = new OracleConnection(_conString))
                {
                    OracleCommand command = new OracleCommand("SZ_BGA_SIAT_PROF.F_GET_BECAS", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        BindByName = true
                    };

                    command.Parameters.Add(new OracleParameter("status", OracleDbType.Varchar2)
                    {
                        Direction = ParameterDirection.ReturnValue
                    });
                    command.Parameters.Add(new OracleParameter("P_TERM", OracleDbType.Varchar2)
                    {
                        Value = term,
                        Direction = ParameterDirection.Input
                    });
                    command.Parameters.Add(new OracleParameter("P_PIDMS", OracleDbType.Varchar2)
                    {
                        Value = new List<int>()[pidm],
                        Direction = ParameterDirection.Input
                    });
                    command.Parameters.Add(new OracleParameter("P_BECAS", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    });

                    connection.Open();

                    command.ExecuteNonQuery();

                    try
                    {
                        if(command.Parameters["status"].Value?.ToString() == "OP_EXITOSA")
                        {
                            OracleDataReader lector = ((OracleRefCursor)command.Parameters["P_BECAS"].Value).GetDataReader();
                            while (lector.Read()) students.Add(Convert.ToInt32((decimal)(OracleDecimal)lector["PIDM"]?.ToString()));
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex;
            }

            return students;
        }

        public static List<ReporteArea> ObtenerAsignacion(int identificador)
        {
            string query = "SELECT ID_REP_SIAT, AREA_ASIG, FECHA_ASIG, MOTIVO FROM TBL_SIAT_ASIGNACION " +
                "LEFT JOIN TBL_SIAT_MOTIVOS ON AREA_ASIG = CVE_MOTIVO AND AREA_ASIG IS NOT NULL " +
                "WHERE ID_REP_SIAT = " + identificador;

            List<ReporteArea> list = new List<ReporteArea>();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand(query, conn)
                {

                })
                {
                    conn.Open();



                    try
                    {

                        command.ExecuteNonQuery();

                        SqlDataReader lector = command.ExecuteReader();

                        while (lector.Read())
                        {
                            list.Add(new ReporteArea()
                            {
                                IdRepSiat = Int32.Parse(lector["ID_REP_SIAT"].ToString()),
                                AreaAsig = lector["AREA_ASIG"].ToString(),
                                FechaAsig = DateTime.Parse(lector["FECHA_ASIG"].ToString()),
                                AreaDesc = lector["MOTIVO"].ToString(),
                            });
                        }
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();
                    }
                };
            }
            return list;
        }

        public static TblSiatRegistro ObtenerTipoRegistro(string tipo)
        {
            string query = "SELECT TIPO_REGISTRO, REGISTRO_DESC FROM TBL_SIAT_REGISTROS WHERE TIPO_REGISTRO = " + tipo;

            TblSiatRegistro tipoRegistro = new TblSiatRegistro();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand(query, conn)
                {

                })
                {
                    conn.Open();



                    try
                    {

                        command.ExecuteNonQuery();

                        SqlDataReader lector = command.ExecuteReader();

                        while (lector.Read())
                        {
                            tipoRegistro.TipoRegistro = lector["TIPO_REGISTRO"].ToString();
                            tipoRegistro.RegistroDesc = lector["REGISTRO_DESC"].ToString();
                        }
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();
                    }
                };
            }
            return tipoRegistro;
        }

        public static List<TblSiatComentario> ObtenerComentarios(int identificador)
        {
            string query = "SELECT ID, RESPUESTA_ID, AREA_ID, COMENTARIO, USER_ID, FECHA_COMENTARIO, USER_FULL_NAME, USER_IDBN " +
                "FROM TBL_SIAT_COMENTARIO WHERE RESPUESTA_ID = " + identificador + " ORDER BY FECHA_COMENTARIO DESC";

            List<TblSiatComentario> list = new List<TblSiatComentario>();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand(query, conn)
                {

                })
                {
                    conn.Open();



                    try
                    {

                        command.ExecuteNonQuery();

                        SqlDataReader lector = command.ExecuteReader();

                        while (lector.Read())
                        {
                            list.Add(new TblSiatComentario() { 
                                Id = Int32.Parse(lector["ID"].ToString()),
                                RespuestaId = Int32.Parse(lector["RESPUESTA_ID"].ToString()),
                                AreaId = lector["AREA_ID"].ToString(),
                                Comentario = lector["COMENTARIO"].ToString(),
                                UserId = lector["USER_ID"].ToString(),
                                FechaComentario = DateTime.Parse(lector["FECHA_COMENTARIO"].ToString()),
                                UserFullName = lector["USER_FULL_NAME"].ToString(),
                                UserIDBN = lector["USER_IDBN"].ToString(),
                            });
                        }
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();
                    }
                };
            }
            return list;
        }

        public static List<ReporteEstatus> ObtenerEstatus(int identificador)
        {
            string query = "SELECT RESPUESTA_ID, ESTATUS_ID, ESTATUS, AREA AS AREA_ESTATUS, FECHA_ESTATUS, USER_ID, USER_FULL_NAME, USER_IDBN " +
                "FROM TBL_SIAT_RESPUESTA_ESTATUS LEFT JOIN TBL_SIAT_ESTATUS_CAT c ON ESTATUS_ID = c.ID " +
                "WHERE RESPUESTA_ID = " + identificador + " ORDER BY FECHA_ESTATUS DESC";

            List<ReporteEstatus> list = new List<ReporteEstatus>();

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand command = new SqlCommand(query, conn)
                {

                })
                {
                    conn.Open();



                    try
                    {

                        command.ExecuteNonQuery();

                        SqlDataReader lector = command.ExecuteReader();

                        while (lector.Read())
                        {
                            list.Add(new ReporteEstatus() {
                                RespuestaId = Int32.Parse(lector["RESPUESTA_ID"].ToString()),
                                EstatusId = Int32.Parse(lector["ESTATUS_ID"].ToString()),
                                EstatusDesc = lector["ESTATUS"].ToString(),
                                FechaEstatus = DateTime.Parse(lector["FECHA_ESTATUS"].ToString()),
                                UserId = lector["USER_ID"].ToString(),
                                UserFullName = lector["USER_FULL_NAME"].ToString(),
                                UserIDBN = lector["USER_IDBN"].ToString(),
                                Area = lector["AREA_ESTATUS"].ToString(),
                            });
                        }
                        conn.Close();
                    }
                    finally
                    {
                        conn.Close();
                    }
                };
            }
            return list;
        }

        public static string ObtenerBecasLinde(int pidm, string term)
        {
            string retorno = string.Empty;

            try
            {
                using (var connection = new OracleConnection(_conString))
                {
                    OracleCommand command = new OracleCommand("F_UDEM_STU_BECAS", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        BindByName = true
                    };

                    command.Parameters.Add(new OracleParameter("V_TODAS_BECAS", OracleDbType.Varchar2, 500)
                    {
                        Direction = ParameterDirection.ReturnValue
                    });
                    command.Parameters.Add(new OracleParameter("P_TERM", OracleDbType.Varchar2)
                    {
                        Value = term,
                        Direction = ParameterDirection.Input
                    });
                    command.Parameters.Add(new OracleParameter("P_PIDM", OracleDbType.Int32)
                    {
                        Value = pidm,
                        Direction = ParameterDirection.Input
                    });
                    command.Parameters.Add(new OracleParameter("P_REGRESA", OracleDbType.Int32)
                    {
                        Value = 3,
                        Direction = ParameterDirection.Input
                    });

                    connection.Open();

                    command.ExecuteNonQuery();

                    try
                    {
                        retorno = command.Parameters["V_TODAS_BECAS"].Value.ToString();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex;
            }

            return retorno;
        }
    }
}