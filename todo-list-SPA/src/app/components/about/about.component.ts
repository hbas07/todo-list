import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {

  constructor(private authService:AuthService) { }

  ngOnInit() {
  }

  get isAuthenticated(){
    return this.authService.loggedIn();
  }
}
