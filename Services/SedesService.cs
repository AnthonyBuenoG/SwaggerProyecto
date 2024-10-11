using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using reportesApi.Models.Compras;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
namespace reportesApi.Services
{
    public class SedesService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public SedesService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetSedesModel> GetSedes()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            GetSedesModel sedes = new GetSedesModel();

            List<GetSedesModel> lista = new List<GetSedesModel>();
            try
            {
                parametros = new ArrayList();
                DataSet ds = dac.Fill("sp_get_sedes", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetSedesModel {
                        Id = int.Parse(dataRow["ID"].ToString()),
                        Nombre = dataRow["NOMBRE"].ToString(),
                        Abreviatura = dataRow["ABREVIATURA"].ToString(),
                        Direccion = dataRow["DIRECCION"].ToString(),
                        FechaHora = dataRow["FECHAHORA"].ToString(),
                        Activo = dataRow["ACTIVO"].ToString(),
                        Usuario = dataRow["USUARIO"].ToString(),


                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public string InsertSedes(InsertSedesModel sedes)
        {

            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.VarChar, Value = sedes.Nombre});
            parametros.Add(new SqlParameter { ParameterName = "@Abreviatura", SqlDbType = System.Data.SqlDbType.VarChar, Value = sedes.Abreviatura});
            parametros.Add(new SqlParameter { ParameterName = "@Direccion", SqlDbType = System.Data.SqlDbType.VarChar, Value = sedes.Direccion });
            parametros.Add(new SqlParameter { ParameterName = "@Usuario", SqlDbType = System.Data.SqlDbType.Int, Value = sedes.Usuario });


            try
            {
                DataSet ds = dac.Fill("sp_insert_sedes", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

        public string UpdateSedes(UpdateSedesModel sedes)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;


            parametros.Add(new SqlParameter { ParameterName = "@ID", SqlDbType = System.Data.SqlDbType.Int, Value = sedes.Id });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.VarChar, Value = sedes.Nombre});
            parametros.Add(new SqlParameter { ParameterName = "@Abreviatura", SqlDbType = System.Data.SqlDbType.VarChar, Value = sedes.Abreviatura});
            parametros.Add(new SqlParameter { ParameterName = "@Direccion", SqlDbType = System.Data.SqlDbType.VarChar, Value = sedes.Direccion });
            parametros.Add(new SqlParameter { ParameterName = "@Activo", SqlDbType = System.Data.SqlDbType.Int, Value = sedes.Activo});
            parametros.Add(new SqlParameter { ParameterName = "@Usuario", SqlDbType = System.Data.SqlDbType.Int, Value = sedes.Usuario });
            try
            {
                DataSet ds = dac.Fill("sp_update_sedes", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

      public void DeleteSedes(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("sp_delete_sedes", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}