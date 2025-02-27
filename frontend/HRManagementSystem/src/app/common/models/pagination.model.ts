export class PaginationModel<T>{
    data: T;
    pageNumber: number = 1;
    pageSize: number = 10;
    totalCount: number;
    isFirstPage: boolean;
    isLastPage: boolean;
}