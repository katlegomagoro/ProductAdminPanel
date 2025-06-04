using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductAdminPanel.DAL.Models;
using ProductAdminPanel.Services.Interfaces;

namespace ProductAdminPanel.Components
{
    public partial class CategoryList
    {
        [Inject] private ICategoryService CategoryService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!;

        private List<Category> _categories = new();
        private string _searchTerm = string.Empty;
        private MudForm? _form;
        private bool _isLoading = true;

        private List<BreadcrumbItem> _breadcrumbs = new()
        {
            new BreadcrumbItem("Home", href: "/"),
            new BreadcrumbItem("Categories", href: "/categories", disabled: true)
        };

        private IEnumerable<Category> _filteredCategories =>
            string.IsNullOrWhiteSpace(_searchTerm)
                ? _categories
                : _categories.Where(c =>
                    (c.Name ?? "").Contains(_searchTerm, StringComparison.OrdinalIgnoreCase));

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
            _isLoading = false;
        }

        private async Task LoadCategories()
        {
            try
            {
                _categories = await CategoryService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Failed to load categories: {ex.Message}", Severity.Error);
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
            var dialog = DialogService.Show<CategoryForm>("Add Category", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                Snackbar.Add("Category added successfully", Severity.Success);
                await LoadCategories();
            }
        }

        private async Task OpenEditDialog(Category category)
        {
            var parameters = new DialogParameters { ["ExistingCategory"] = category };
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };
            var dialog = DialogService.Show<CategoryForm>("Edit Category", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                Snackbar.Add("Category updated successfully", Severity.Info);
                await LoadCategories();
            }
        }

        private async Task DeleteCategory(Guid id)
        {
            bool? confirm = await DialogService.ShowMessageBox(
                "Confirm Delete",
                "Are you sure you want to delete this category?",
                yesText: "Delete", cancelText: "Cancel");

            if (confirm == true)
            {
                try
                {
                    await CategoryService.DeleteAsync(id);
                    Snackbar.Add("Category deleted successfully", Severity.Success);
                    await LoadCategories();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Failed to delete category: {ex.Message}", Severity.Error);
                }
            }
        }
    }
}
