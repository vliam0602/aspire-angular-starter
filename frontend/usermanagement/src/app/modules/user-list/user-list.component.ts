import { Component, inject, ViewChild } from '@angular/core';
import { User } from '../../shared/models/user';
import { MatTableModule } from '@angular/material/table'
import { UserApiService } from '../../services/user-api.service';
import { PagedList } from '../../shared/models/pagedList';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [
    MatTableModule, 
    MatPaginatorModule, 
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule
  ],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent {
  private userApi = inject(UserApiService);

  pagedUsers: PagedList<User> =
    {
      items: [],
      totalCount: 0,
      pageIndex: 1,
      pageSize: 2
    };

  displayedColumns: string[] = ['no', 'username', 'email', 'status', 'joinDate', 'actions'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit() {
    this.loadUser(this.pagedUsers.pageIndex, this.pagedUsers.pageSize);
  }

  ngAfterViewInit() {
    this.paginator.page.subscribe((event: PageEvent) => {
      this.loadUser(event.pageIndex + 1, event.pageSize);
    });
  }

  loadUser(pageIndex: number, pageSize: number) {
    this.userApi.getUsers({ pageIndex, pageSize }).subscribe(data =>
      this.pagedUsers = data
    );
  }

  onAddUser() {
    alert('Add new user');
  }
  onEditUser(user: User) {
    alert('Edit user: '+ user.username);
  }
  onDeleteUser(user: User) {
    alert('Delete user: '+ user.username);
  }
}