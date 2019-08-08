import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from './components/about/about.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';


const routes: Routes = [
  {
    path :"", component:LoginComponent,
  },
  {
    path:"login",component:LoginComponent,
  },
  {
    path:"register",component:RegisterComponent,
  },
  {
    path:"home",component:HomeComponent,
  },
  {
    path :"about", component:AboutComponent,
  },
  {
    path:"**", component:NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
