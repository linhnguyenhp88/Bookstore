import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Book } from '../_models/book';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
};

@Injectable({
  providedIn: 'root'
})
export class BookService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getBooks(page?, itemsPerPage?, command?): Observable<PaginatedResult<Book[]>> {
    const paginatedResult: PaginatedResult<Book[]> = new PaginatedResult<Book[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (command != null) {
       params = params.append('queryParams', command.Query);
    }

    return this.http.get<Book[]>(this.baseUrl + 'books/get-all-books', { observe: 'response', params})
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getBook(id): Observable<Book> {
    return this.http.get<Book>(this.baseUrl + 'books/get-book/' + id, httpOptions);
  }

}
