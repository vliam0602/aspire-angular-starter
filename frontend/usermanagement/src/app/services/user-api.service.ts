import { inject, Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { User } from "../shared/models/user";
import { PagedList } from "../shared/models/pagedList";

@Injectable({
    providedIn: 'root'
})
export class UserApiService {
    private api = inject(ApiService);

    getUsers() {
        return this.api.get<PagedList<User>>('api/users');
    }
}