import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class TodoService {
  
  constructor(private http: HttpClient, @Inject("apiUrl") private apiUrl) {}

  getYourLists(userId) {
    return this.http.get(this.apiUrl + "/home?userId=" + userId);
}
addPendings(obj) {
  return this.http.post(this.apiUrl + "/home", obj);
}
updateLists(listname,comefrom,obj){
  return this.http.put(this.apiUrl+"/home?listname="+listname+"&comefrom="+comefrom,obj);
}

silVeri(id,listname) {
  return this.http.delete(this.apiUrl + "/home?id="+ id+"&listname="+listname);
}
}
