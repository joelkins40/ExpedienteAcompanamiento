﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpedienteAcompanamiento.Models.Entity;
using ExpedienteAcompanamiento.Utils;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ExpedienteAcompanamiento.Models.Services
{

    public class ResidenciasAlertasService
    {

        private static readonly string _conString = ConfigurationManager.ConnectionStrings["AlertaResidencias"].ConnectionString;


        public static ResultObject obtenerInformacionAlertas(string matricula)
        {
            ResultObject result = new ResultObject();
            ResidenciasAlertas datosResidenciasAlertas = new ResidenciasAlertas();
            var residenciasAlertasList = new List<ResidenciasAlertas>();

            try
            {

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
                        result = new ResultObject()
                        {
                            Success = true,
                            Status = 200,
                            Value = residenciasAlertasList
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
    }
}

