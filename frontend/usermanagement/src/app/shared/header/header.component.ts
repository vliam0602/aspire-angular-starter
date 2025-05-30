import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-header',
  imports: [
    MenubarModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  menuItems = [
    { label: 'Home', icon: 'pi pi-home', routerLink: '/' },
    { label: 'User List', icon: 'pi pi-user', routerLink: '/users' },
    { label: 'Add User', icon: 'pi pi-plus', routerLink: '/add-user' },
  ];
}
