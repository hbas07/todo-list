import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authService:AuthService,
    public _snackBar: MatSnackBar) { }

  loginUser:any={}

  ngOnInit() {
  }

  
  login(){
    this.authService.login(this.loginUser);
  }

  logOut(){
    this.authService.logOut();
  }

  get isAuthenticated(){
    return this.authService.loggedIn();
  }

  openSnackBar(message: string) {
    this._snackBar.open(message, "Tamam", {
      duration: 2500
    });
  }
}
