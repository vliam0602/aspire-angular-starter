import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserApiService } from '../../services/user-api.service';
import { User } from '../../shared/models/user';
// PrimeNG modules
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DropdownModule,
    CalendarModule,
    ButtonModule,
    InputTextModule
  ],
  templateUrl: './user-form.component.html',
  styleUrl: './user-form.component.scss'
})
export class UserFormComponent {
  mode: 'add' | 'edit' = 'add';
  userId?: string;
  form: FormGroup;
  loading = false;

  statusOptions = [
    { label: 'Inactive', value: 0 },
    { label: 'Active', value: 1 }
  ];

  private route = inject(ActivatedRoute);
  private formBuilder = inject(FormBuilder);
  private userApi = inject(UserApiService);
  private router = inject(Router);

  constructor() {
    this.form = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      status: [0, Validators.required],
      joinDate: [new Date(), Validators.required],
    });

    this.route.paramMap.subscribe(params => {
      if (params.has('id')) {
        this.mode = 'edit';
        this.userId = params.get('id') ?? undefined;
        this.loadUser();
      }
    });
  }

  loadUser() {
    if (!this.userId) return;
    this.loading = true;
    this.userApi.getUserById(this.userId).subscribe({
      next: (user: User) => {
        this.form.patchValue({
          username: user.username,
          email: user.email,
          status: user.status,
          joinDate: new Date(user.joinDate)
        });
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  onSubmit() {
    if (this.form.invalid) return;
    const value = this.form.value;
    if (this.mode === 'add') {
      this.userApi.createUser(value).subscribe(() => {
        this.router.navigate(['users']);
      });
    } else if (this.mode === 'edit' && this.userId) {
      this.userApi.editUser(this.userId, value).subscribe(() => {
        this.router.navigate(['users']);
      });
    }
  }
}
