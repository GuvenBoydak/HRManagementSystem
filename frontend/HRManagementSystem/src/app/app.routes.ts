import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: "",
        loadComponent: () => import("./components/layouts/layout/layout.component").then(c => c.LayoutComponent),
        children: []
    }
];
