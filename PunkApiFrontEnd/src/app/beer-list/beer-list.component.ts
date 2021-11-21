import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import {MatDialog, MAT_DIALOG_DATA} from '@angular/material/dialog';


import { Beer } from '../Interfaces/Beer';
import { BeerService } from '../Services/beer.service';
import { BeerDetailComponent } from '../beer-detail/beer-detail.component';

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

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private BeerService: BeerService, public dialog: MatDialog ) { }

  ngOnInit(): void {
    console.log("In Init"); 
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
      beerId: selected.id
    }
  })
}

  AddBeerToFavourites(beerId: number)
  {
    const beerToAdd = this.displayBeers.find(x=> x.id == beerId); 
  }
}
