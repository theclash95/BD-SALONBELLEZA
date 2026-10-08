using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SALONBELLEZA.CORE.Interfaces
{
    public interface IMyConexion
    {

        IDbConnection obtenerConexion();
        Task<IDbConnection> obtenerConexionAsync();

    }
}
