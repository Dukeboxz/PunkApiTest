import { Beer } from "./Beer";

export interface ReturnView{

    message: string,
    userId: string, 
    beers: Beer[]
}