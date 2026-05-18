# DataEntry Reference

This reference standardizes how to build `DataEntry*.razor` screens in MedicalCareWeb.

## Baseline skeleton

```razor
<MudForm @ref="_form" Model="@modelo">
    <div class="dea-shell">
        <div class="dea-section-label px-4">
            <MudIcon Icon="@Icons.Material.Outlined.Business" />
            Informacion General
        </div>

        <MudGrid Spacing="2" Class="px-4">
            <MudItem xs="12" sm="6">
                <MudTextField T="string"
                              Label="Nombre"
                              @bind-Value="modelo.Name"
                              Required="true"
                              RequiredError="El nombre es obligatorio"
                              Variant="Variant.Outlined"
                              Margin="Margin.Dense"
                              FullWidth="true" />
            </MudItem>
        </MudGrid>

        <div class="dea-actions pa-4">
            <MudButton Variant="Variant.Text" OnClick="Cancel">Cancelar</MudButton>
            <MudButton Variant="Variant.Filled" Color="Color.Primary" OnClick="Submit">Guardar</MudButton>
        </div>
    </div>
</MudForm>
```

## Recommended CSS tokens/classes

- `dea-shell`: vertical flow container for the full form body.
- `dea-section-label`: small uppercase section marker with icon.
- `dea-status-wrap` + `dea-status-pill`: active/inactive state container.
- `dea-actions`: final action row aligned to the end.

## Submit flow

```csharp
private async Task Submit()
{
    await _form!.ValidateAsync();
    if (!_form.IsValid)
    {
        Snackbar.Add("Por favor completa los campos obligatorios.", Severity.Warning);
        return;
    }

    if (EsEdicion)
        await EditSubmit();
    else
        await NewSubmit();
}
```

## Do and do not

Do:
- Keep `Variant.Outlined` + `Margin.Dense` for consistency.
- Keep labels/messages in Spanish.
- Keep submit/cancel locations stable.

Do not:
- Mix a new style language when `dea-*` already fits.
- Skip required validation or snackbar feedback.
- Change status representation pattern without UX reason.
