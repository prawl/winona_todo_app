import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TodoItem } from '../models/todoItem';


@Injectable({
  providedIn: 'root',
})
export class TasksService {

  constructor(protected http: HttpClient) {}

  public getTodoItems(): Observable<TodoItem[]> {
    return this.http.get<TodoItem[]>(`http://localhost:5220/api/TodoItems`);
  }

  public saveTodoItem(item: TodoItem): Observable<TodoItem> {
    return this.http.post<TodoItem>(`http://localhost:5220/api/TodoItems`, item);
  }

  public deleteToDo(guid: string): Observable<void> {
    return this.http.delete<void>(
      `http://localhost:5220/api/TodoItems/${guid}`,
      {}
    );
  }
}
