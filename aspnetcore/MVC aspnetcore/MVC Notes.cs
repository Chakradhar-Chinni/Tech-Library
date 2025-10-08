(1) _layouts

1. Multiple Layouts: You can create multiple layout files (e.g., `_Layout.cshtml`, `_PremiumLayout.cshtml`) in `/Views/Shared` and use them selectively in views or via `_ViewStart.cshtml`.

2. Scoped `_ViewStart.cshtml`: Place a `_ViewStart.cshtml` inside folders like `/Views/Premium/` to automatically apply a specific layout (e.g., `_PremiumLayout`) to all views in that folder, overriding the global layout.




  
(2) Sections

Purpose: Sections allow views to inject content (like scripts or styles) into specific placeholders in a shared layout, keeping layout structure clean and reusable.

Scope: Sections are scoped to the view where they’re defined and only rendered when the layout includes a matching @RenderSection() — they are not shared across views.  




  
(3) ModelState validation

- When using the [ApiController] attribute on a controller, automatic model validation is enabled.
- If the model state is invalid, ASP.NET Core automatically returns a 400 Bad Request response with a ProblemDetails object in the response body.
- This behavior is ideal for APIs but not suitable for form-based web apps, because:
   - The browser will show a serialized JSON error page instead of rendering validation messages in the view.
   - You lose the ability to display field-level validation errors in Razor views.

- Use Manual Model validation to Show Validation Messages in Razor Views
[HttpPost]
public IActionResult Create(UserViewModel viewModel)
{
    if (!ModelState.IsValid)
    {
        return View(viewModel); // Re-render the form with validation messages
    }
    // Proceed with valid data
}




(4) Learning during Update in CRUD
Issue 1: Readonly ID fields became blank after validation failure
Root Cause: Inputs used asp-for plus manual value="@TempData[...]". TempData is read-once; after first GET/POST cycle values vanished. Explicit value= suppressed Tag Helper fallback to ModelState values.
Fix: Populate model in GET, remove manual value=, keep readonly (or hidden) so IDs post back.

  
Issue 2: InvalidOperationException creating EditSalesOrderDetailViewModel on POST
Root Cause: View model lacked a public parameterless constructor; model binder could not instantiate the type.
Fix: Add a public parameterless ctor alongside the convenience ctor taking IDs.

  
Issue 3: Using TempData for form field persistence
Root Cause: TempData clears after it is read, unsuitable for retaining form field values across a failed POST where you return the same view.
Fix: Rely on ModelState (return View(model)) and initialize IDs in the GET action; reserve TempData for PRG success messages.

  
Issue 4: Potential loss of key values if disabled were used instead of readonly (general pitfall)
Root Cause: Disabled inputs are not posted, leading to missing IDs if changed later to disabled.
Fix: Use readonly (submits value) or pair a hidden field with a plaintext display.

  
Issue 5: Missing anti-forgery protection on POST actions
Root Cause: [ValidateAntiForgeryToken] attribute commented out and no @Html.AntiForgeryToken() in form, exposing CSRF risk.
Fix: Add attribute to POST action and token helper in the form.

  
Issue 6: Duplicate property declarations (e.g., Product.Name, ProductNumber, SalesOrderDetail.CarrierTrackingNumber) in shown signatures
Root Cause: Accidental duplicate lines (possibly merge/edit artifact) will cause compile errors or confusion.
Fix: Remove duplicates; keep a single properly initialized definition.


Issue 7: Nullable / uninitialized reference properties (risk of null)
Root Cause: Properties like CarrierTrackingNumber not initialized; could cause null reference usage or validation confusion.
Fix: Initialize to string.Empty or apply [Required] (already present) and ensure the binder sets a value; safer to initialize.

  
Issue 8: IDs initially nullable (earlier version) permitting invalid state
Root Cause: Allowing nullable IDs when editing existing entities can produce unnecessary validation complexity.
Fix: Make IDs non-nullable int when they must always exist on edit forms.
