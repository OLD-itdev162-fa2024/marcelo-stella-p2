import { Injectable } from '@angular/core';

import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {Observable} from 'rxjs';
import { Task } from '../Interfaces/task';


@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private endpoint:string = environment.endPoint;
  private apiURL:string = this.endpoint + "Task/";

  constructor(private http:HttpClient) { }

  getList():Observable<Task[]>{
    return this.http.get<Task[]>(`${this.apiURL}List`);
  }

  add(request:Task):Observable<Task>{
    return this.http.post<Task>(`${this.apiURL}Add`, request);
  }

  delete(id:number):Observable<void>{
    return this.http.delete<void>(`${this.apiURL}Delete/${id}`);
  }
}
