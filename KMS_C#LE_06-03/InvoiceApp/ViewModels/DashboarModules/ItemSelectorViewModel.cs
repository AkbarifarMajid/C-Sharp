
//Select product from list and interact(Relation) with ComboBox

namespace InvoiceApp.ViewModels.DashboardModules
{

    // This ViewModel manages the selected item
    public class ItemSelectorViewModel
    {
        public ItemViewModel SelectedItem { get; set; }
        public int Quantity { get; set; }
    }
}
