using System;
namespace reportesApi.Models
{
    public class GetSedesModel
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public string Abreviatura {get; set;}
        public string Direccion { get; set; }
        public string FechaHora { get; set; }
        public string Activo { get; set; }
        public string Usuario { get; set; }


    }

    public class InsertSedesModel 
    {
        public string Nombre{ get; set; }
        public string Abreviatura {get; set;}
        public string Direccion {get; set;}
        public int Usuario {get; set;}
       
    }

    public class UpdateSedesModel
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public string Abreviatura {get; set;}
        public string Direccion {get; set;}
        public int Activo { get; set; }

        public int Usuario { get; set; }


       
    }

}