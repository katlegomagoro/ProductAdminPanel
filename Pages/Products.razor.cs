using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductAdminPanel.Components;
using ProductAdminPanel.DAL.Models;
using ProductAdminPanel.Services.Interfaces;

namespace ProductAdminPanel.Pages
{
    public partial class Products
    {
        [Inject] private IProductService ProductService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!;

        private List<Product> _products = new();
        private string _searchTerm = string.Empty;
        private MudForm? _form;
        private bool _isLoading = true;

        private List<BreadcrumbItem> _breadcrumbs = new()
        {
            new BreadcrumbItem("Home", href: "/"),
            new BreadcrumbItem("Products", href: "/products", disabled: true)
        };

        private IEnumerable<Product> _filteredProducts =>
            string.IsNullOrWhiteSpace(_searchTerm)
                ? _products
                : _products.Where(p => (p.Name ?? "").Contains(_searchTerm, StringComparison.OrdinalIgnoreCase));

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
            _isLoading = false;
        }

        private async Task LoadProducts()
        {
            try
            {
                _products = await ProductService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Failed to load products: {ex.Message}", Severity.Error);
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
            var dialog = DialogService.Show<ProductForm>("Add Product", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                Snackbar.Add("Product added successfully", Severity.Success);
                await LoadProducts();
            }
        }

        private async Task OpenEditDialog(Product product)
        {
            var parameters = new DialogParameters { ["ExistingProduct"] = product };
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };
            var dialog = DialogService.Show<ProductForm>("Edit Product", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                Snackbar.Add("Product updated successfully", Severity.Info);
                await LoadProducts();
            }
        }

        private async Task DeleteProduct(Guid id)
        {
            bool? confirm = await DialogService.ShowMessageBox(
                "Confirm Delete",
                "Are you sure you want to delete this product?",
                yesText: "Delete", cancelText: "Cancel");

            if (confirm == true)
            {
                try
                {
                    await ProductService.DeleteAsync(id);
                    Snackbar.Add("Product deleted successfully", Severity.Success);
                    await LoadProducts();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Failed to delete product: {ex.Message}", Severity.Error);
                }
            }
        }
    }
}
