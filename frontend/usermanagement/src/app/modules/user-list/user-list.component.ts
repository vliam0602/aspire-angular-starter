import { Component, inject, ViewChild } from '@angular/core';
import { User } from '../../shared/models/user';
import { UserApiService } from '../../services/user-api.service';
import { PageList } from '../../shared/models/page-list.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { SplitButtonModule } from 'primeng/splitbutton';
import { MenuItem } from 'primeng/api';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [
    CommonModule,
    TableModule,
    SplitButtonModule,
    ButtonModule
  ],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent {
  pagedUsers: PageList<User> =
    {
      items: [],
      totalCount: 0,
      pageIndex: 1,
      pageSize: 5
    };

  private userApi = inject(UserApiService);
  private router = inject(Router);

  ngOnInit() {
    this.loadUser(this.pagedUsers.pageIndex, this.pagedUsers.pageSize);
  }

  loadUser(pageIndex: number, pageSize: number) {  
    this.userApi.getUsers({ pageIndex, pageSize }).subscribe(data => {
      this.pagedUsers = data
    });
  }

  onPage(event: any) {
    const pageIndex = Math.floor(event.first / event.rows) + 1;
    this.loadUser(pageIndex, event.rows);
  }

  onAddUser() {
    this.router.navigate(['/add-user']);
  }
  onEditUser(user: User) {
    this.router.navigate(['/edit-user', user.id]);
  }
  onDeleteUser(user: User) {
    alert('Delete user: ' + user.username);
  }

  getActionMenu(user: User): MenuItem[] {
    return [
      {
        label: 'Edit',
        icon: 'pi pi-pencil',
        command: () => this.onEditUser(user)
      },
      {
        label: 'Remove',
        icon: 'pi pi-trash',
        command: () => this.onDeleteUser(user)
      }
    ];
  }
}