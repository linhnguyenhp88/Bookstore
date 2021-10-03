import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import {TimeAgoPipe} from 'time-ago-pipe'
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { BookListComponent } from './book-list/book-list.component';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import {AdminPanelComponent} from './admin/admin-panel/admin-panel.component';
import { RegisterComponent } from './register/register.component';
import { JwtModule } from '@auth0/angular-jwt';
import { BsDropdownModule, TabsModule, BsDatepickerModule,
    PaginationModule, ButtonsModule, ModalModule, BsModalRef, BsModalService } from 'ngx-bootstrap';
import {AlertifyService} from './_services/alertify.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { BookService } from './_services/book.service';
import { BookListResolver } from './_resolvers/book-list.resolver';
import { BookCardComponent } from './book-card/book-card.component';
import { AppRoutingModule } from './app-routing.module';
import { BookDetailResolver } from './_resolvers/book-detail.resolver';

export function tokenGetter() {
   return localStorage.getItem('token');
 }


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      BookListComponent,
      BookDetailComponent,
      AdminPanelComponent,
      RegisterComponent,
      BookCardComponent,
      TimeAgoPipe
   ],
   imports: [
      HttpClientModule ,
      AppRoutingModule,
      BrowserModule,
      FormsModule,
      ReactiveFormsModule,
      TabsModule.forRoot(),
      BsDropdownModule.forRoot(),
      PaginationModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: {
           tokenGetter: tokenGetter,
           whitelistedDomains: ['localhost:30921'],
           blacklistedRoutes: ['localhost:30921/api/auth']
         }
       })
   ],
   providers: [
      AuthService,
      AlertifyService,
      ErrorInterceptorProvider,
      BookService,
      BookListResolver,
      BookDetailResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
