(1) _layouts

1. Multiple Layouts: You can create multiple layout files (e.g., `_Layout.cshtml`, `_PremiumLayout.cshtml`) in `/Views/Shared` and use them selectively in views or via `_ViewStart.cshtml`.

2. Scoped `_ViewStart.cshtml`: Place a `_ViewStart.cshtml` inside folders like `/Views/Premium/` to automatically apply a specific layout (e.g., `_PremiumLayout`) to all views in that folder, overriding the global layout.


(2) Sections

Purpose: Sections allow views to inject content (like scripts or styles) into specific placeholders in a shared layout, keeping layout structure clean and reusable.

Scope: Sections are scoped to the view where they’re defined and only rendered when the layout includes a matching @RenderSection() — they are not shared across views.  
  
