import { Component, OnInit } from "@angular/core";
import {CdkDragDrop,moveItemInArray,transferArrayItem} from "@angular/cdk/drag-drop";
import { TodoService } from "src/app/services/todo.service";
import { MatSnackBar } from "@angular/material";
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"]
})
export class HomeComponent implements OnInit {
  data = {};

  taskid: any;
  comefrom:any;
  arrive:any;

  constructor(
    private todoServices: TodoService,
    public _snackBar: MatSnackBar,
    private authService:AuthService
  ) {}

  ngOnInit() {
    this.getYourLists();
  }

  get userId(){
    return localStorage.getItem("userId");
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
    this.updateLists(event);

  }

  getYourLists(){
    var userid =+this.userId;
    this.todoServices.getYourLists(userid)
    .subscribe((res) => {
      console.log(res);
      Object.keys(res).forEach((key) => {
        this.data[key] = res[key];
        
      })
    }, (err) => { console.log(err); });
  }

  addPendings(pending){
    var userid=+ this.userId;
    const obj = { todo: pending.value, UserId: userid }
    this.todoServices.addPendings(obj)
    .subscribe((res:any) => {
      this.getYourLists();
      this.openSnackBar('Task başarıyla eklendi.');
      pending.value = '';
    }, (err) => { console.log(err); });
  }

  updateLists(event:CdkDragDrop<string[]>){
      const obj=event.container.data[event.currentIndex]
      var listname=event.container.id
      var comefrom=event.previousContainer.id
      this.todoServices.updateLists(listname,comefrom,obj).subscribe((res)=>{
        if(event.previousIndex>event.currentIndex && listname===comefrom)
        {
          this.openSnackBar("Yukarı taşıdınız")
          console.log(obj)
        
        }
        this.getYourLists()
      },
      (err)=>{console.log(err)})
  }


  silVeri(id,listname) {
    if(confirm('Bu maddeyi silmek istediğinize emin misiniz ?'))
    { 
      console.log(id)
      console.log(listname)
      this.todoServices.silVeri(id,listname)
      .subscribe((res) => {
        this.getYourLists();
      }, (err) => {
        console.log(err);
      });
    }
  }
  
  openSnackBar(message: string) {
    this._snackBar.open(message, "Tamam", {
      duration: 2500
    });
  }

  logOut(){
      this.authService.logOut();
      localStorage.clear();
      this.openSnackBar("Çıkış başarılı !")
  }
  
  get isAuthenticated(){
    return this.authService.loggedIn();
  }

}
