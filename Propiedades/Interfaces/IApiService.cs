using Propiedades.Models;
using static Propiedades.Models.DetallesPropiedad;

namespace Propiedades.Interfaces
{
    public interface IApiService
    {
        // metodo para obtener todas las propiedades que nos permita la api
        Task<vmPropiedadResponse> ObtenerTodasLasPropiedades(int? pageNumber, string orderBy, string searchTerm,
                                                                          int? pageSize, string orderDirection, int? tipo, int? Sector);
        // metodo para obtener detalle de la propiedad segun el Id Propiedad
        Task<PropertyDetalle> ObtenerDetalles(int? PropiedadId);

        // metodo para obtener los tipos de Propiedad
        Task<List<TipoPropiedades>> GetTipoPropiedad();

        // metodo para obtener regiones
        Task<List<Regiones>> GetRegiones();

        // metodo para obtener los sectores segun la region, lo dejo asi para no confundir el concepto Boroughs
        Task<List<Boroughs>> GetBoroughs(int RegionId);
    }
}
