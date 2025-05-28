import { inject, Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { User } from "../shared/models/user";
import { PagedList } from "../shared/models/pagedList";
import { HttpParams } from "@angular/common/http";

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
        return this.api.get<PagedList<User>>('api/users', httpParams);
    }
}