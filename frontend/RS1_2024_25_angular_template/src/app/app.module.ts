import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {MyAuthInterceptor} from './services/auth-services/my-auth-interceptor.service';
import {MyAuthService} from './services/auth-services/my-auth.service';
import {SharedModule} from './modules/shared/shared.module';
import {ReactiveFormsModule} from '@angular/forms';
import { PublicModule } from './modules/public/public.module';
import {LoginComponent} from './login/login.component';
import { TutorRegistrationComponent } from './public/tutor-registration/tutor-registration.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TutorRegistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    PublicModule,
    ReactiveFormsModule// Omogućava korištenje UnauthorizedComponent u AppRoutingModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MyAuthInterceptor,
      multi: true // Ensures multiple interceptors can be used if needed
    },
    MyAuthService // Ensure MyAuthService is available for the interceptor
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
