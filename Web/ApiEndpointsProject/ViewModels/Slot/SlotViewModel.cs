namespace WebApi.ViewModels.Slot
{
    public class SlotViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal StartPrice { get; set; }
        public bool ItemChosen { get; set; }
    }
}