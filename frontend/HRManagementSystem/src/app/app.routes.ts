import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path:"login",
        loadComponent: ()=> import("./components/auth/components/login/login.component").then(c=> c.LoginComponent)
    },
    {
        path: "",
        loadComponent: () => import("./components/layouts/layout/layout.component").then(c => c.LayoutComponent),
        children: []
    }
];
