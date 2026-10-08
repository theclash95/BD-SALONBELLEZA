using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySqlConnector;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.INFRASTRUCTURE.Contexto
{
    public class MyConexion : IMyConexion
    {
        private readonly string _cadenaDeConexion;

        public MyConexion (string _cadena)
        {

            _cadenaDeConexion = _cadena;


        }

        
        public MySqlConnection obtenerConexion()
        {
            MySqlConnection conn = new MySqlConnection(_cadenaDeConexion);
            conn.Open();
            return conn;
            
            }

        

        //COMO IMPLEMENTAR MIS METODOS
        public IDbConnection obtenerConexion()
        {
            throw new NotImplementedException();
        }

        public Task<IDbConnection> obtenerConexionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
