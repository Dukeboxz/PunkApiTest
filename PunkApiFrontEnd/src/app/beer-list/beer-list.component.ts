import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import {MatDialog, MAT_DIALOG_DATA} from '@angular/material/dialog';


import { Beer } from '../Interfaces/Beer';
import { BeerService } from '../Services/beer.service';
import { BeerDetailComponent } from '../beer-detail/beer-detail.component';
import { UserService } from '../Services/user.service';
import { SelectControlValueAccessor } from '@angular/forms';
import { getLocaleNumberSymbol } from '@angular/common';
import { Subscriber } from 'rxjs';

@Component({
  selector: 'app-beer-list',
  templateUrl: './beer-list.component.html',
  styleUrls: ['./beer-list.component.css']
})
export class BeerListComponent implements OnInit {

  displayBeers: Beer[] = []; 
  columnsToDisplay = ['name', 'tagline', 'first_brewed','abv', 'action'];
  searchTerm: string =''; 
  length = 100;
  pageSize = 10;
  currentPage =1; 
  showingFavourites: boolean = false; 
  
  userID: string; 
  UserLoggedIn: boolean = false; 

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private BeerService: BeerService, private userService: UserService,  public dialog: MatDialog ) { }

  ngOnInit(): void {
    this.GetBeers(); 
    
    this.userService.LogggedInUser
    .subscribe({
      next: res=> {
        console.log("log in subsribe", res)

        if(res){
        this.userID = res;
        this.UserLoggedIn = true;
        }
      },
      error: err => {
        alert("User Not Logged In ");
        
      }
    }) 

    this.userService.displayFavs.subscribe(
      {
        next: res=> {
          console.log("Log in subscribe")
          if(res){
            this.ShowUserFavourites();
            this.showingFavourites = true; 
          }else{
            this.GetBeers()
            this.showingFavourites = false; 
          }
        }
      }
    )

  }

  //Gets the start of the beers list
  GetBeers(){

    this.BeerService.GetBeersForList(1, 10)
    .subscribe({
      next: res=>{
         
          this.displayBeers=res
          
      },
      error: error =>{

        //todo make generic error popup
        alert("Could not get beers")
      }

      
    })
  }

  ///Shows user favourites.  If no favourites will just be blank
  ShowUserFavourites()
  {
    this.userService.GetUserFavourites(this.userID)
    .subscribe({
      next: res=> {
        console.log('return faves');
        console.log(res)
      this.displayBeers = res.beers;
      },
      error: err => {
        alert("Could not add Beer to favarites ");
        
      }
  })
  }

  SearchBeersByName(): void {
     
    if(this.searchTerm.length == 0){

      return alert("please enter search term")
    }

    this.BeerService.GetBeersByNameSearch(this.searchTerm)
    .subscribe({
      next: res=>{
        console.log("search reply"); 
        console.log(res);
        this.displayBeers = res
      }, 
      error: error=> {

        alert("could not get searched for beers")
        
      }
    })
  }

  ListPageChange(): void 
  {
    let returnedBeers: Beer[] =[]
    if(this.searchTerm.length > 0){
      this.BeerService.GetBeersByNameSearch(this.searchTerm, this.paginator.pageIndex)
      .subscribe({
        next: res=> {
          returnedBeers = res; 
          if(returnedBeers.length == 0){
            this.paginator.pageIndex -= 1; 
          }else{
            this.displayBeers = returnedBeers; 
          }
        },
        error: err => {
          alert("Could not get beers");
          this.paginator.pageIndex -= 1;
        }
      })
    }else{
      this.BeerService.GetBeersForList(this.paginator.pageIndex)
      .subscribe({
        next: res=> {
          returnedBeers = res; 
          if(returnedBeers.length == 0){
            this.paginator.pageIndex -= 1; 
          }else{
            this.displayBeers = returnedBeers; 
          }
        },
        error: err => {
          alert("Could not get beers");
          this.paginator.pageIndex -= 1;
        }
      })
    }
    
  }

  ShowBeerDetail(selected: Beer): void
{
  this.dialog.open(BeerDetailComponent, {
    data: {
      name: selected.name,
      description: selected.description,
      imageUrl: selected.image_url,
      beerId: selected.id,
      abv: selected.abv
    }
  })
}

  AddBeerToFavourites(beerId: number)
  {
    console.log("Add Beer");
    const beerToAdd = this.displayBeers.find(x=> x.id == beerId); 
    console.log(beerToAdd)
    if(beerToAdd){
      this.userService.LogggedInUser
    .subscribe({
      next: res=> {
        this.userID = res;
      },
      error: err => {
        alert("User Not Logged In ");
        
      }
    }) 

    this.userService.AddUserFavourite(this.userID, beerToAdd)
    .subscribe({
      next: res=> {
        console.log(res)
      },
      error: err => {
        alert("Could not add Beer to favarites ");
        
      }
    })
  }
    }
    
}
