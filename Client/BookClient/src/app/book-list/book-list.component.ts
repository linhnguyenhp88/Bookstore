import { Component, OnInit } from '@angular/core';
import { Book } from '../_models/book';
import { BookService } from '../_services/book.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from '../_models/pagination';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {
  books: Book[];
  public pagination: Pagination;
  public command: any = {};


  constructor(private bookService: BookService,
    private alertify: AlertifyService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.books = data['books'].result;
      this.pagination = data['books'].pagination;
    });

    this.command.Query = '';
  }

  pageChanged (event: any): void {
    this.pagination.currentPage = event.page;
    this.loadBooks();
  }

  loadBooks() {
    this.bookService.getBooks(this.pagination.currentPage, this.pagination.itemsPerPage, this.command)
      .subscribe((res: PaginatedResult<Book[]>) => {
        this.books = res.result;
        this.pagination = res.pagination;
        this.command.Query = '';
    }, error => {
      this.alertify.error(error);
    });
  }
}
