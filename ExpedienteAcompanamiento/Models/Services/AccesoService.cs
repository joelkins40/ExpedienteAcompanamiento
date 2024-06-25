using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Services
{
    public class AccesoService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

        public static async Task<string> obtenerMatriculaToken(string token)
        {
    
            string tokenAbierto = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            NameValueCollection parsedValues = HttpUtility.ParseQueryString(tokenAbierto);

            string matricula = parsedValues["matricula"];
            string pin = parsedValues["pin"];

          
              if(ValidaCredenciales(matricula, pin))
            {

                return ObtenerPIDM(parsedValues["matricula"]);
            }
            else
            {
                return null;
            }

          

           
           
        }
        public static bool ValidaCredenciales(string matricula, string pin)
        {
            bool validas = false;
            try
            {


                using (var connection = new OracleConnection(_conString))
                {
                    OracleCommand command = new OracleCommand("SZ_BFQ_REGISTRATION.f_validar_credenciales", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        BindByName = true
                    };

                    command.Parameters.Add(new OracleParameter("salida", OracleDbType.Varchar2, 1)
                    {
                        Direction = System.Data.ParameterDirection.ReturnValue
                    });
                    command.Parameters.Add(new OracleParameter("p_matricula", OracleDbType.Varchar2, 200)
                    {
                        Value = matricula,
                        Direction = System.Data.ParameterDirection.Input
                    });

                    command.Parameters.Add(new OracleParameter("p_pin", OracleDbType.Varchar2, 200)
                    {
                        Value = pin,
                        Direction = System.Data.ParameterDirection.Input
                    });

                    connection.Open();

                    int ejecucion = command.ExecuteNonQuery();

                    try
                    {
                        if (Convert.ToString(command.Parameters["salida"]?.Value) == "Y")
                        {
                            validas = true;
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
            return validas;
        }
        public static string ObtenerPIDM(string matricula)
        {
            string pidm = "";
            try
            {
                using (var connection = new OracleConnection(_conString))
                {
                    OracleCommand command = new OracleCommand("F_UDEM_STU_PIDM", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        BindByName = true
                    };

                    command.Parameters.Add(new OracleParameter("salida", OracleDbType.Int16)
                    {
                        Direction = ParameterDirection.ReturnValue
                    });
                    command.Parameters.Add(new OracleParameter("cMatricula", OracleDbType.Varchar2)
                    {
                        Value = matricula,
                        Direction = ParameterDirection.Input
                    });

                    connection.Open();

                    int ejecucion = command.ExecuteNonQuery();

                    try
                    {
                        pidm = Convert.ToString(command.Parameters["salida"]?.Value);
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

            return pidm;
        }


    }
}