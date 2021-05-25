using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.ViewModels.Slot;

namespace ApiEndpointsProject.Interfaces
{
    public interface ISlotViewModelService
    {
        Task<IEnumerable<SlotViewModel>> GetUserSlots(string userId);
    }
}