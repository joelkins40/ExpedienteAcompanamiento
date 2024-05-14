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
            return await ObtenerPIDM(parsedValues["matricula"]);

           
           
        }
        public static async Task<string> ObtenerPIDM(string pidm)
        {
            string matricula = "";
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
                        Value = pidm,
                        Direction = ParameterDirection.Input
                    });

                    connection.Open();

                    int ejecucion = await command.ExecuteNonQueryAsync();

                    try
                    {
                        matricula = Convert.ToString(command.Parameters["salida"]?.Value);
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

            return matricula;
        }

    }
}