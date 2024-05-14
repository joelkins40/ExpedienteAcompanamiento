using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Services
{
    public class AccesoService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

        public static async Task<string> obtenerMatriculaToken()
        {
            string token = HttpContext.Current.Request["token"];
            string tokenAbierto = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            NameValueCollection parsedValues = HttpUtility.ParseQueryString(tokenAbierto);
            string matricula =  parsedValues["matricula"];
            string pin       =  parsedValues["pin"];

            bool valida = await ValidaCredenciales(matricula, pin);

            if (valida)
            {
                return matricula;
            }
            else
            {
                return null;
            }
           
        }
        public async static Task<bool> ValidaCredenciales(string matricula, string pin)
        {
            bool validas = false;
            try
            {


                using (var connection = new OracleConnection(_conString))
                {
                    OracleCommand command = new OracleCommand("SZ_BFQ_KARDEXBA.f_validar_credenciales", connection)
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

                    int ejecucion = await command.ExecuteNonQueryAsync();

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
    }
}