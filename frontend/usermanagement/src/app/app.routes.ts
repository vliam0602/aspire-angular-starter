import { Routes } from '@angular/router';
import { UserListComponent } from './modules/user-list/user-list.component';
import { UserFormComponent } from './modules/user-form/user-form.component';
import { LayoutComponent } from './shared/layout/layout.component';

export const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            { path: '', redirectTo: 'users', pathMatch: 'full' },
            { path: 'users', component: UserListComponent },
            { path: 'add-user', component: UserFormComponent },
            { path: 'edit-user/:id', component: UserFormComponent },
        ]
    }
];
