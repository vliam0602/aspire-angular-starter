import { Component, inject } from '@angular/core';
import { User } from '../../shared/models/user';
import { MatTableModule } from '@angular/material/table'
import { UserApiService } from '../../services/user-api.service';
import { PagedList } from '../../shared/models/pagedList';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [MatTableModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent {
  private userApi = inject(UserApiService);

  users: PagedList<User> =
  {
    items:[],
    totalCount: 0,
    pageIndex: 1,
    pageSize: 5
  };
  
  displayedColumns: string[] = ['no', 'username', 'email', 'status', 'joinDate'];

  ngOnInit() {
    this.userApi.getUsers().subscribe(data => {
      this.users = data;
    });
  }
}