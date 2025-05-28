export interface PagedList<T> {
    items: T[];
    totalCount: number;
    pageIndex: number;
    pageSize: number;
}
