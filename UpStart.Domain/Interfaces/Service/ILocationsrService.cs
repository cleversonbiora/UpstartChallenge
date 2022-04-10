using System.Collections.Generic;
using System.Threading.Tasks;
using UpStart.Domain.ViewModels.Location;

namespace UpStart.Domain.Interfaces.Service
{
    public interface ILocationService
    {
        Task<IEnumerable<AddressResultVM>> GetLocation(string address);
    }
}
