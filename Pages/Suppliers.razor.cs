using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductAdminPanel.Components;
using ProductAdminPanel.DAL.Models;
using ProductAdminPanel.Services.Interfaces;

namespace ProductAdminPanel.Pages
{
    public partial class Suppliers
    {
        [Inject] private ISupplierService SupplierService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!;

        private List<Supplier> _suppliers = new();
        private string _searchTerm = string.Empty;
        private MudForm? _form;
        private bool _isLoading = true;

        private List<BreadcrumbItem> _breadcrumbs = new()
        {
            new BreadcrumbItem("Home", href: "/"),
            new BreadcrumbItem("Suppliers", href: "/suppliers", disabled: true)
        };

        private IEnumerable<Supplier> _filteredSuppliers =>
            string.IsNullOrWhiteSpace(_searchTerm)
                ? _suppliers
                : _suppliers.Where(s =>
                    (s.Name ?? "").Contains(_searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (s.ContactEmail ?? "").Contains(_searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (s.ContactNumber ?? "").Contains(_searchTerm, StringComparison.OrdinalIgnoreCase));

        protected override async Task OnInitializedAsync()
        {
            await LoadSuppliers();
            _isLoading = false;
        }

        private async Task LoadSuppliers()
        {
            try
            {
                _suppliers = await SupplierService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Failed to load suppliers: {ex.Message}", Severity.Error);
            }
        }

        private void OnSearchTermChanged(string value)
        {
            _searchTerm = value;
            StateHasChanged();
        }

        private async Task OpenAddDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };
            var dialog = DialogService.Show<SupplierForm>("Add Supplier", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                Snackbar.Add("Supplier added successfully", Severity.Success);
                await LoadSuppliers();
            }
        }

        private async Task OpenEditDialog(Supplier supplier)
        {
            var parameters = new DialogParameters { ["ExistingSupplier"] = supplier };
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };
            var dialog = DialogService.Show<SupplierForm>("Edit Supplier", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                Snackbar.Add("Supplier updated successfully", Severity.Info);
                await LoadSuppliers();
            }
        }

        private async Task DeleteSupplier(Guid id)
        {
            bool? confirm = await DialogService.ShowMessageBox(
                "Confirm Delete", "Are you sure you want to delete this supplier?",
                yesText: "Delete", cancelText: "Cancel");

            if (confirm == true)
            {
                try
                {
                    await SupplierService.DeleteAsync(id);
                    Snackbar.Add("Supplier deleted successfully", Severity.Success);
                    await LoadSuppliers();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Failed to delete supplier: {ex.Message}", Severity.Error);
                }
            }
        }
    }
}
