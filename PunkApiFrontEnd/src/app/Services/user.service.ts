import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { User } from '../Interfaces/User';
import { BehaviorSubject, Observable } from 'rxjs';
import { Beer } from '../Interfaces/Beer';
import { ReturnView } from '../Interfaces/ReturnView';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  
  private LoggedInUserIdSource = new BehaviorSubject<string>(''); 
  LogggedInUser = this.LoggedInUserIdSource.asObservable(); 

  private UserLoggedInSource = new BehaviorSubject<boolean>(false); 
  UserLoggedIn = this.UserLoggedInSource.asObservable(); 

  private showUsersFavourites = new BehaviorSubject<boolean>(false); 
  displayFavs = this.showUsersFavourites.asObservable(); 
  
  baseUrl = 'https://localhost:7102/api/UserFavourites'; 

  constructor(private http: HttpClient) { }

  //Once user is logged in they are confirmed as existing or if not a new db record is created.
GetOrCreateUser(userID: string): Observable<User>{

 const headers = new HttpHeaders()
  .append('Content-Type', 'application/json');

  const params = new HttpParams()
    .set('userId', userID)

  const bodyData = {
    'userId': userID
  }

  let body = JSON.stringify(bodyData);

  return this.http.post<User>(this.baseUrl + "/User", body, { params, headers});
}

  //Updates the observable if the user is changed
  UpdateCurrentUserId(userId: string){

    this.LoggedInUserIdSource.next(userId); 
  }

  //update User logged in status
  UpdateUserLogInStatus(loggedIn: boolean){
    this.UserLoggedInSource.next(loggedIn); 
  }

  //Updates if user wants to see favs or not
  UpdateShowFavourites(show: boolean){
    this.showUsersFavourites.next(show); 
  }


  ///Does the http call to add a single beer to user favs on db
  AddUserFavourite(userID: string, newBeer: Beer):Observable<string>
  {
    console.log("in Add User Favourites")
    const headers = new HttpHeaders()
    .append('Content-Type', 'application/json');

    const params = new HttpParams()
    .set('userId', userID)

    let body = JSON.stringify(newBeer); 

    return this.http.post<string>(this.baseUrl, body, {params, headers})
    

  }

  GetUserFavourites(userid: string):Observable<ReturnView>
  {
    const headers = new HttpHeaders()
  .append('Content-Type', 'application/json');

  const params = new HttpParams()
    .set('userId', userid); 

    return this.http.get<ReturnView>(this.baseUrl, {params, headers})

  }


}
