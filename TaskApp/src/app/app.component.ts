import { Component, OnInit } from '@angular/core';

import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { Task } from './Interfaces/task';
import { TaskService } from './Services/task.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  taskList:Task[]= [];
  taskForm:FormGroup;

  constructor(
    private _taskService:TaskService,
    private fb:FormBuilder
  ){
    this.taskForm = this.fb.group({
      taskName:['',Validators.required]
    });
  }

  getTasks(){
    this._taskService.getList().subscribe({
      next:(data) => {
        this.taskList = data;
      },error:(e)=>{console.log('Something is wrong...')}
    });
  }

  ngOnInit(): void {
    this.getTasks();
  }

  addTask(){
    const request:Task = {
      taskID:0,
      taskName: this.taskForm.value.taskName
    }

    this._taskService.add(request).subscribe({
      next:(data) => {
        this.taskList.push(data);
        this.taskForm.patchValue({
          taskName:""
        });
      },error:(e)=>{console.log('Something is wrong...')}
    });
  }

  deleteTask(task:Task){
    this._taskService.delete(task.taskID).subscribe({
      next:(data) => {
        const newList = this.taskList.filter(item => item.taskID != task.taskID);
        this.taskList = newList;
      },error:(e)=>{console.log('Something is wrong...')}
    });
  }
}
