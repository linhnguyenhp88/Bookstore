import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { BookListComponent } from './book-list/book-list.component';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { AuthGuard } from './_guards/auth.guard';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { BookListResolver } from './_resolvers/book-list.resolver';
import { BookDetailResolver } from './_resolvers/book-detail.resolver';

export const appRoutes: Routes = [
    {path: 'home', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'books', component: BookListComponent,
                    resolve: {books: BookListResolver}},
            {path: 'books/:id', component: BookDetailComponent,
                    resolve: {book: BookDetailResolver}},
            {path: 'admin', component: AdminPanelComponent},
        ]
    },
    {path: '**', redirectTo: 'home', pathMatch: 'full'},
];
