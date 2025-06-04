using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductAdminPanel.DAL.Models;
using ProductAdminPanel.Services.Interfaces;

namespace ProductAdminPanel.Components
{
    public partial class CategoryForm : ComponentBase
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;

        [Parameter] public Category? ExistingCategory { get; set; }

        private MudForm? _formRef;
        private Category _category = new();
        private List<Category> _availableParents = new();
        private bool _isEdit => ExistingCategory != null;

        protected override async Task OnInitializedAsync()
        {
            _availableParents = await CategoryService.GetAllAsync();

            if (_isEdit && ExistingCategory is not null)
            {
                _category = new Category
                {
                    Id = ExistingCategory.Id,
                    Name = ExistingCategory.Name,
                    Description = ExistingCategory.Description,
                    ParentCategoryId = ExistingCategory.ParentCategoryId
                };
            }

            // Prevent category from assigning itself as parent
            if (_isEdit)
                _availableParents = _availableParents.Where(c => c.Id != _category.Id).ToList();
        }

        private async Task Save()
        {
            if (_formRef is null)
                return;

            await _formRef.Validate();

            if (!_formRef.IsValid)
                return;

            if (_isEdit)
                await CategoryService.UpdateAsync(_category);
            else
                await CategoryService.AddAsync(_category);

            MudDialog.Close(DialogResult.Ok(true));
        }

        private void Cancel() => MudDialog.Cancel();

        // Event Callbacks
        private void OnNameChanged(string value) => _category.Name = value;
        private void OnDescriptionChanged(string? value) => _category.Description = value;
        private void OnParentChanged(Guid? value) => _category.ParentCategoryId = value;
    }
}
