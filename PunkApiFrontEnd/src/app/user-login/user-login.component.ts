import { Component, OnInit } from '@angular/core';
import { User } from '../Interfaces/User';
import { FormBuilder, FormGroup, Validators, ValidationErrors, ReactiveFormsModule } from '@angular/forms';
import { UserService
 } from '../Services/user.service';
@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  LoginForm: FormGroup
  UserId: string


  constructor(private fb : FormBuilder, private userService: UserService) { }

  ngOnInit(): void {
    this.LoginForm = this.fb.group({
      UserId: ['', [Validators.required, Validators.email]]
    })

    
  }

  Login()
  {
    if(this.LoginForm.valid){

      console.log('value is ' + this.LoginForm.controls['UserId'].value);
        this.userService.GetOrCreateUser(this.LoginForm.controls['UserId'].value)
        .subscribe({
          next: res=>{
            console.log(res)
            this.userService.UpdateCurrentUserId(res.UserId)
            this.UserId = res.UserId
            this.userService.UpdateUserLogInStatus(true); 
            
        },
        error: error =>{
          console.log(error);
          //todo make generic error popup
          alert(error)
        }
        })

    }


  }

  
  ShowFavourites(show: boolean){
      
      this.userService.UpdateShowFavourites(show); 
  }

  

  

}
