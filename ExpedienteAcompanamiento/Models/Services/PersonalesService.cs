using ExpedienteAcompanamiento.Models.Entity;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ExpedienteAcompanamiento.Models.Services
{
    public class PersonalesService
    {
        private static readonly string _conString = ConfigurationManager.ConnectionStrings["BANNER"].ConnectionString;

        public static Personales ObtenerPersonales(int pidm)
        {
            Personales personales = null;

            using (OracleConnection cnx = new OracleConnection(_conString))
            {
                cnx.Open();

                // Preparamos la consulta de los datos personales
                OracleCommand comando = new OracleCommand();
                comando.Connection = cnx;
                comando.CommandText = @"GZ_BGSEXPE.F_GET_DATOS_PERSONALES_AUX";
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.BindByName = true;

                comando.Parameters.Add(new OracleParameter("salida", OracleDbType.Varchar2)
                {
                    Size = 200,
                    Direction = System.Data.ParameterDirection.ReturnValue
                });

                comando.Parameters.Add(new OracleParameter("PIDM", OracleDbType.Int16)
                {
                    Value = pidm,
                    Size = 9
                });

                comando.Parameters.Add(new OracleParameter("P_DATOS_PERSONALES", OracleDbType.RefCursor)
                {
                    Direction = System.Data.ParameterDirection.Output
                });

                comando.ExecuteNonQuery();

                // Revisamos si se pudo ejecutar la consulta
                if (comando.Parameters["salida"].Value?.ToString() == "OP_EXITOSA")
                {
                    // Inicializamos la variable de salida
                    personales = new Personales();

                    OracleDataReader lector = ((OracleRefCursor)comando.Parameters["P_DATOS_PERSONALES"].Value).GetDataReader();

                    while (lector.Read())
                    {
                        personales = new Personales()
                        {
                            matricula = lector["Matricula"]?.ToString(),
                            nombreCompleto = lector["Nombre_Completo"]?.ToString(),
                            fechaNacimiento = lector["Fecha_Nacimiento"]?.ToString(),
                            ciudadOrigen = lector["Ciudad_Origen"]?.ToString(),
                            estadoOrigen = lector["Estado_Origen"]?.ToString(),
                            domicilioPermanente = lector["Direccion"]?.ToString(),
                            telefono = lector["Telefono"]?.ToString(),
                            domicilioZona = lector["Direccion_Metropolitana"]?.ToString(),
                            nivel = lector["nivel"]?.ToString(),
                            carreraEstudia = lector["carrera"]?.ToString(),
                            carrerasInscrito = lector["Carrera_Anterior"]?.ToString(),
                            dobleTitulacion = lector["Doble_Titulacion"]?.ToString(),
                            //dobleGrado = lector["Correo_Preferencia"]?.ToString(),
                            correoPreferente = lector["Correo_Preferencia"]?.ToString(),
                            correoPersonal = lector["Correo_Personal"]?.ToString(),
                            correoUDEM = lector["Correo_UDEM"]?.ToString(),
                            seguroGMMUDEM = lector["SGMM_UDEM"]?.ToString(),
                            seguroGMMParticular = lector["SGMM_Particular"]?.ToString(),
                            localForaneo = lector["Etnia"]?.ToString(),
                            genero = lector["Genero"]?.ToString(),
                            periodoActual = lector["Periodo_Actual"]?.ToString(),
                            semestre = lector["Semestre"]?.ToString(),
                            prepaProcedencia = lector["Preparatoria_Procedencia"]?.ToString(),
                        };

                    }
                }

                cnx.Close();


            }

            return personales;
        }
    }
}