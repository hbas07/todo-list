import { Injectable } from "@angular/core";
import { LoginUser } from "../models/loginUser";
import {
  HttpHeaders,
  HttpClient,
  HttpErrorResponse
} from "@angular/common/http";
import { JwtHelper, tokenNotExpired } from "angular2-jwt";
import { Router } from "@angular/router";
import { AlertifyService } from "./alertify.service";
import { RegisterUser } from "../models/registerUser";
import { MatSnackBar } from "@angular/material";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private alertifyService: AlertifyService,
    public _snackBar: MatSnackBar,
  ) {}
  path = "https://localhost:44333/api/auth/";
  userToken: any;
  decodedToken: any;
  jwtHelper: JwtHelper = new JwtHelper();
  TOKEN_KEY = "token";

  login(loginUser: LoginUser) {
    //Backenddeki login aksiyonuna veri göndermek için(headerda, application json formatında)
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");
    //Login olan kullanıcı bilgisini alır.
    this.httpClient
      .post(this.path + "login", loginUser, { headers: headers })
      .subscribe(data => {
        if (data.toString() != null) {
          this.userToken = data;
          this.decodedToken = this.jwtHelper.decodeToken(data.toString());
          console.log(this.decodedToken.nameid);
          this.save(data, this.decodedToken.nameid);
          this.router.navigateByUrl("/home");
          this.openSnackBar("Giriş işlemi başarılı !");
        }
      }, (err) => { this.openSnackBar("Kullanıcı adı ya da şifre hatalı !") })
  }

  register(registerUser: RegisterUser) {
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");
    this.httpClient
      .post(this.path + "register", registerUser, { headers: headers })
      .subscribe((res: any) => {
        this.openSnackBar("Başarılı !");
      });
  }

  save(token, userId) {
    localStorage.setItem(this.TOKEN_KEY, token);
    localStorage.setItem("userId", userId);
  }

  openSnackBar(message: string) {
    this._snackBar.open(message, "Tamam.", {
      duration: 3000
    });
  }

  logOut() {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  loggedIn() {
    return tokenNotExpired(this.TOKEN_KEY);
  }

  get token() {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  getCurrentUserId() {
    return this.jwtHelper.decodeToken(this.token).nameid;
  }
}
