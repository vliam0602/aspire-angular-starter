export interface PageList<T> {
    items: T[];
    totalCount: number;
    pageIndex: number;
    pageSize: number;
}
