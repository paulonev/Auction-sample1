using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEndpointsProject.Interfaces;
using ApiEndpointsProject.Specifications;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using WebApi.ViewModels.Slot;

namespace ApiEndpointsProject.Services
{
    public class SlotViewModelService : ISlotViewModelService
    {
        private IAsyncRepository<Slot> _slotRepository;

        public SlotViewModelService(IAsyncRepository<Slot> slotRepository)
        {
            _slotRepository = slotRepository;
        }
        
        public async Task<IEnumerable<SlotViewModel>> GetUserSlots(string userId)
        {
            var userGuid = Guid.Parse(userId);
            var userSlotsSpecification = new UserSlotsSpecification(userGuid);
            var userSlots = await _slotRepository.ListAsync(userSlotsSpecification);

            var vm = userSlots.Select(s => new SlotViewModel()
            {
                Id = s.Id.ToString(),
                Title = s.Title,
                Description = s.Description,
                StartPrice = s.StartPrice,
                ItemChosen = false
            });

            return vm;
        }
    }
}