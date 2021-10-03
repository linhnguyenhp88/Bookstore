import { Component, OnInit, ViewChild } from '@angular/core';
import { Book } from '../_models/book';
import { BookService } from '../_services/book.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { TabsetComponent } from 'ngx-bootstrap';
import { DecimalPipe } from '@angular/common';
import * as moment from 'moment';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.scss']
})
export class BookDetailComponent implements OnInit {
  @ViewChild('bookTabs') bookTabs: TabsetComponent;
  book: Book;
  public shopName = 'Amazon';
  public motorbikeCost: number = 5;
  public trainCost: number = 10;
  public airCraftCost: number = 20;

  constructor(private bookService: BookService, private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.book = data['book'];
    });
    this.CalculateDeliveryCost();
  }

  private CalculateDeliveryCost() {
    const currentTime = new Date();
    const currentMonth = currentTime.getMonth();
    if (currentMonth >= 6 && currentMonth <= 8) {
       this.motorbikeCost = this.motorbikeCost * 0.5;
       this.trainCost = this.trainCost * 0.8;
       this.airCraftCost = this.airCraftCost * 0.8;
    } else {
      if (currentMonth === 9) {
        this.motorbikeCost = this.motorbikeCost * 1.5;
        this.trainCost = this.trainCost * 1.8;
        this.airCraftCost = this.airCraftCost * 2;
      } else {
        this.motorbikeCost = this.motorbikeCost * 1;
        this.trainCost = this.trainCost * 1;
        this.airCraftCost = this.airCraftCost * 1;
      }
    }
  }

}
