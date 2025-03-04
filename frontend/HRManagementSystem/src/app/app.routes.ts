import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';


export const routes: Routes = [
    {
        path:"login",
        loadComponent: ()=> import("./components/auth/components/login/login.component").then(c=> c.LoginComponent)
    },
    {
        path:"change-password",
        loadComponent: ()=> import("./components/auth/components/change-password/change-password.component").then(c=> c.ChangePasswordComponent)
    },
    {
        path: "",
        canActivate: [AuthGuard],
        loadComponent: () => import("./components/layouts/layout/layout.component").then(c => c.LayoutComponent),
        children: [
            {
                path:"",
                loadComponent: ()=> import("./components/employee/employee.component").then(c=> c.EmployeeComponent)    
            },
            {
                path:"employee/detail/:id",
                loadComponent: ()=> import("./components//employee/employee-detail/employee-detail.component").then(c=> c.EmployeeDetailComponent)    
            },
            {
                path:"payroll",
                loadComponent: ()=> import("./components/payroll/payroll.component").then(c=> c.PayrollComponent)    
            },
            {
                path:"leaveform",
                loadComponent: ()=> import("./components/leaveform/components/leaveform/leaveform.component").then(c=> c.LeaveformComponent)    
            },
            {
                path:"performance",
                loadComponent: ()=> import("./components/performance/components/performance/performance.component").then(c=> c.PerformanceComponent)    
            }
        ]
    }
];
