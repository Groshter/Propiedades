using Propiedades.Models;
using static Propiedades.Models.DetallesPropiedad;

namespace Propiedades.Interfaces
{
    public interface IApiService
    {
        Task<vmPropiedadResponse> ObtenerTodasLasPropiedades(int? pageNumber, string orderBy, string searchTerm,
                                                                          int? pageSize, string orderDirection, int? tipo, int? Sector);
        Task<PropertyDetalle> ObtenerDetalles(int? PropiedadId);
        Task<List<TipoPropiedades>> GetPropertyTypes();
        Task<List<Regiones>> GetRegiones();
        Task<List<Boroughs>> GetBoroughs(int RegionId);
    }
}
