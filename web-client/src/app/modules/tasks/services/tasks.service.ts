import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, finalize } from 'rxjs';
import { TodoItem } from '../models/todoItem';

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  public isLoadingTaskItems$ = new BehaviorSubject(false);
  public isSavingTaskItems$ = new BehaviorSubject(false);
  public isDeletingTaskItems$ = new BehaviorSubject(false);

  constructor(protected http: HttpClient) {}

  public getTodoItems(): Observable<TodoItem[]> {
    this.isLoadingTaskItems$.next(true);

    return this.http
      .get<TodoItem[]>(`http://localhost:5220/api/TodoItems`)
      .pipe(finalize(() => this.isLoadingTaskItems$.next(false)));
  }

  public saveTodoItem(item: TodoItem): Observable<TodoItem> {
    this.isSavingTaskItems$.next(true);

    return this.http
      .post<TodoItem>(`http://localhost:5220/api/TodoItems`, item)
      .pipe(finalize(() => this.isSavingTaskItems$.next(false)));
  }

  public deleteToDo(guid: string): Observable<void> {
    this.isDeletingTaskItems$.next(true);

    return this.http
      .delete<void>(`http://localhost:5220/api/TodoItems/${guid}`, {})
      .pipe(finalize(() => this.isDeletingTaskItems$.next(false)));
  }
}
