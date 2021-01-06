import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { ProfileComponent } from './profile/profile.component';
import { SignupComponent } from './signup/signup.component';
import { LandingComponent } from './landing/landing.component';
import { LoginComponent } from './login/login.component';
import { PropertiesComponent } from './properties/properties.component';
import { CategoriesComponent } from './categories/categories.component';
import { SuggestionsComponent } from './suggestions/suggestions.component';
import { DocsComponent } from './docs/docs.component';
import { DetailsComponent } from './details/details.component';
import { AutocomplateComponent } from './autocomplate/autocomplate.component';
import { DefaultComponent } from './default/default.component';
import { PlaceCommentsComponent } from './place-comments/place-comments.component';
import { PlaceProductsComponent } from './place-products/place-products.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { PlaceDetailsComponent } from './place-details/place-details.component';
import { TestComponent } from './test/test.component';
import { UserCommentsComponent } from './user-comments/user-comments.component';
import { UserFavoritePlacesComponent } from './user-favorite-places/user-favorite-places.component';
import { UserVisitedComponent } from './user-visited/user-visited.component';
import { HowItWorkComponent } from './how-it-work/how-it-work.component';
import { ContentDetailsComponent } from './content/content-details/content-details.component';
import { ContentListComponent } from './content/content-list/content-list.component';
import { BlogDetailsComponent } from './blog/blog-details/blog-details.component';
import { BlogListComponent } from './blog/blog-list/blog-list.component';
import { CategoriesListComponent } from './category/categories-list/categories-list.component';
import { CategoryDetailsComponent } from './category/category-details/category-details.component';


const routes: Routes =[
    { path: 'profile',     component: UserProfileComponent },
    { path: 'register',           component: SignupComponent },
    { path: 'landing',          component: LandingComponent },
    { path: 'login',          component: LoginComponent },
    { path: 'properties',          component: PropertiesComponent },
    { path: 'categories',          component: CategoriesComponent },
    { path: 'suggestions',          component: SuggestionsComponent },
    { path: 'docs',          component: DocsComponent },
    { path: 'details',          component: PlaceDetailsComponent },
    { path: 'auto',          component: AutocomplateComponent },
    { path: 'default',          component: DefaultComponent },
    { path: 'place-comments',          component: PlaceCommentsComponent },
    { path: 'place-products',          component: PlaceProductsComponent },
    { path: 'user-comments',          component: UserCommentsComponent },
    { path: 'user-favorite-places',          component: UserFavoritePlacesComponent },
    { path: 'user-visited-places',          component: UserVisitedComponent },
    { path: 'how-it-work',          component: HowItWorkComponent },
    { path: 'content-details',          component: ContentDetailsComponent },
    { path: 'content-list',          component: ContentListComponent },
    { path: 'blog-details',          component: BlogDetailsComponent },
    { path: 'blog-list',          component: BlogListComponent },
    { path: 'category-list',          component: CategoriesListComponent },
    { path: 'category-details',          component: CategoryDetailsComponent },
    { path: 'test',          component: TestComponent },
    { path: '', redirectTo: 'default', pathMatch: 'full' }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes,{
      useHash: true
    })
  ],
  exports: [
  ],
})
export class AppRoutingModule { }
