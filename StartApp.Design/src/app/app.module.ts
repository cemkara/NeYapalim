import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';

import { AppComponent } from './app.component';
import { SignupComponent } from './signup/signup.component';
import { LandingComponent } from './landing/landing.component';
import { ProfileComponent } from './profile/profile.component';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { FooterComponent } from './shared/footer/footer.component';

import { HomeModule } from './home/home.module';
import { LoginComponent } from './login/login.component';
import { PropertiesComponent } from './properties/properties.component';
import { CategoriesComponent } from './categories/categories.component';
import { SuggestionsComponent } from './suggestions/suggestions.component';
import { DocsComponent } from './docs/docs.component';
import { DetailsComponent } from './details/details.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {AutocompleteLibModule} from 'angular-ng-autocomplete';
import { AutocomplateComponent } from './autocomplate/autocomplate.component';
import { DefaultComponent } from './default/default.component';
import { PlaceCommentsComponent } from './place-comments/place-comments.component';
import { PlaceProductsComponent } from './place-products/place-products.component';
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import {
  GoogleLoginProvider,
  FacebookLoginProvider
} from 'angularx-social-login';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { PlaceDetailsComponent } from './place-details/place-details.component';
import { CommentListComponent } from './comment-list/comment-list.component';
import { TestComponent } from './test/test.component';
import { UserCommentsComponent } from './user-comments/user-comments.component';
import { UserFavoritePlacesComponent } from './user-favorite-places/user-favorite-places.component';


@NgModule({
  declarations: [
    AppComponent,
    SignupComponent,
    LandingComponent,
    ProfileComponent,
    NavbarComponent,
    FooterComponent,
    LoginComponent,
    PropertiesComponent,
    CategoriesComponent,
    SuggestionsComponent,
    DocsComponent,
    DetailsComponent,
    AutocomplateComponent,
    DefaultComponent,
    PlaceCommentsComponent,
    PlaceProductsComponent,
    UserProfileComponent,
    PlaceDetailsComponent,
    CommentListComponent,
    TestComponent,
    UserCommentsComponent,
    UserFavoritePlacesComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
    HomeModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AutocompleteLibModule, 
    FormsModule, 
    ReactiveFormsModule,
    SocialLoginModule,
  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '904555948584-72k2lvra77j062et61bfu9dogeaefrlh.apps.googleusercontent.com'
            )
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('2815150752088966')
          }
        ]
      } as SocialAuthServiceConfig,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
