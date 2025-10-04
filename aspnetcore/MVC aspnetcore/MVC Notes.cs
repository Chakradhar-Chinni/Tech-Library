(1)

1. Multiple Layouts: You can create multiple layout files (e.g., `_Layout.cshtml`, `_PremiumLayout.cshtml`) in `/Views/Shared` and use them selectively in views or via `_ViewStart.cshtml`.

2. Scoped `_ViewStart.cshtml`: Place a `_ViewStart.cshtml` inside folders like `/Views/Premium/` to automatically apply a specific layout (e.g., `_PremiumLayout`) to all views in that folder, overriding the global layout.
