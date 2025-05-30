import { Routes } from '@angular/router';
import { UserListComponent } from './modules/user-list/user-list.component';
import { UserFormComponent } from './modules/user-form/user-form.component';
import { AppComponent } from './app.component';

export const routes: Routes = [
    { path: '', redirectTo: 'users', pathMatch: 'full' },
    {path: 'users', component: UserListComponent},
    {path: 'add-user', component: UserFormComponent},
    {path: 'edit-user/:id', component: UserFormComponent},
];
