import { inject, Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { User } from "../shared/models/user";
import { PageList } from "../shared/models/page-list.model";
import { HttpParams } from "@angular/common/http";

const USER_URL = 'api/users';

@Injectable({
    providedIn: 'root'
})
export class UserApiService {
    private api = inject(ApiService);

    getUsers(params?: {pageIndex: number, pageSize: number}) {
        let httpParams = new HttpParams();
        if (params) {
            httpParams = httpParams
                .set('pageIndex', params.pageIndex)                
                .set('pageSize', params.pageSize)                
        }
        return this.api.get<PageList<User>>(USER_URL, httpParams);
    }
    
    getUserById(id: string) {
        return this.api.get<User>(`${USER_URL}/${id}`);
    }

    createUser(user: Partial<User>) {
        return this.api.post(USER_URL, user);
    }

    editUser(id: string, user: Partial<User>) {
        return this.api.put(`${USER_URL}/${id}`, user);
    }
}