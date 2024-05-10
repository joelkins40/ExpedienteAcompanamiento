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

        public static Personales getPersonales(int pidm)
        {
            Personales personales = new Personales();
            try
            {
                using (OracleConnection cnx = new OracleConnection(_conString))
                {

                    using (OracleCommand comando = new OracleCommand())
                    {
                        comando.Connection = cnx;
                        comando.CommandText = "GZ_BGSEXPE.F_GET_DATOS_PERSONALES_AUX";
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.BindByName = true;
                        comando.Parameters.Add(new OracleParameter("salida", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.ReturnValue
                        });

                        comando.Parameters.Add(new OracleParameter("PIDM", OracleDbType.Int16)
                        {
                            Value = pidm,
                            Direction = System.Data.ParameterDirection.Input
                        });


                        // Revisamos si se pudo ejecutar la consulta
                        cnx.Open();

                        try
                        {

                            OracleDataReader lector = comando.ExecuteReader();

                            // Revisamos cada contacto

                            while (lector.Read())
                            {



                                personales = new Personales()
                                {
                                    matricula = lector["matricula"]?.ToString(),
                                    nombreCompleto = lector["nombreCompleto"]?.ToString(),
                                    fechaNacimiento = lector["fechaNac"]?.ToString(),
                                    ciudadOrigen = lector["ciudadOrigen"]?.ToString(),
                                    estadoOrigen = lector["direccionMetropolitana"]?.ToString(),
                                    domicilioPermanente = lector["direccion"]?.ToString(),
                                    telefono = lector["telefono"]?.ToString(),
                                    domicilioZona = lector["nivel"]?.ToString(),
                                    carreraEstudia = lector["carrera"]?.ToString(),
                                    carrerasInscrito = lector["carrerasAnt"]?.ToString(),
                                    dobleTitulacion = lector["dobleTit"]?.ToString(),
                                    dobleGrado = lector["correoPref"]?.ToString(),
                                    correoPreferente = lector["correoPref"]?.ToString(),
                                    correoPersonal = lector["correoPers"]?.ToString(),
                                    correoUDEM = lector["correoUDEM"]?.ToString(),
                                    seguroGMMUDEM = lector["sgmmUDEM"]?.ToString(),
                                    seguroGMMParticular = lector["sgmmParti"]?.ToString(),
                                    localForaneo = lector["localForaneo"]?.ToString(),
                                    genero = lector["genero"]?.ToString(),
                                    periodoActual = lector["periodoActual"]?.ToString(),
                                    semestre = lector["semestre"]?.ToString(),
                                };




                            }


                        }
                        finally
                        {

                            cnx.Close();
                        }

                    }

                    //cnx.Close();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
            return personales;

        }

    
    }
}