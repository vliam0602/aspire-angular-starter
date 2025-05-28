import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../shared/constants/environment";

@Injectable({
    providedIn: 'root'
})
export class ApiService {
    private http = inject(HttpClient);
    private baseUrl = environment.apiBaseUrl;

    get<T>(endpoint: string, params?: HttpParams) {
        return this.http.get<T>(`${this.baseUrl}/${endpoint}`, {params});
    }

    post<T>(endpoint: string, body: any) {
        return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body);
    }

    put<T>(endpoint: string, body: any) {
        return this.http.put<T>(`${this.baseUrl}/${endpoint}`, body);
    }

    delete<T>(endpoint: string) {
        return this.http.delete<T>(`${this.baseUrl}/${endpoint}`);
    }
}