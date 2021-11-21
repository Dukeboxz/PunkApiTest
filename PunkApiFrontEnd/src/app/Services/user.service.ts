import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { User } from '../Interfaces/User';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  LoggedInUser: User; 
  baseUrl = 'https://localhost:7102/api/UserFavourites'; 

  constructor(private http: HttpClient) { }

GetUser(userID: string): Observable<User>{

 const headers = new HttpHeaders()
  .append('Content-Type', 'application/json');

  let bodyData = {
      Userid: userID
  }
  const body = JSON.stringify(bodyData)

  return this.http.post<User>(this.baseUrl, body, {headers});
}

}
