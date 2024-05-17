import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { TodoItem } from '../../models/todoItem';
import { finalize } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { AddTodoComponent } from '../../components/add-todo/add-todo.component';
import { ConfirmDeleteToDoComponent } from '../../components/confirm-delete-todo/confirm-delete-todo.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class TaskListComponent implements OnInit {
  tasks: TodoItem[] = [];
  loading = false;
  displayedColumns: string[] = [
    'task',
    'deadline',
    'details',
    'isComplete',
    'id'
  ];
  dataSource = [];

  constructor(
    protected tasksService: TasksService,
    private dialog: MatDialog,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.loadTodos();
  }

  public saveTodoItem() {
    const item = {
      id: 12345,
      task: 'Test test test',
      deadline: new Date(),
      details: 'new',
      isComplete: false,
      subItem: null,
    };

    this.tasksService.saveTodoItem(item).subscribe((resp) => {
      const item = resp;
    });
  }

  public addItem(event: MouseEvent) {
    event.stopPropagation();
    const ref = this.dialog.open(AddTodoComponent);

    ref.afterClosed().subscribe((resp) => {
        void this.router.navigate(['/tasks']);
    });
  }

  deleteTask(id: string) {
    const ref = this.dialog.open(ConfirmDeleteToDoComponent, {
      data: id,
    });

    ref.afterClosed().subscribe((resp) => {
      if (resp) {
        void this.router.navigate(['/tasks']);
      }
    });
  }

  private loadTodos() {
    this.loading = true;
    this.tasksService
      .getTodoItems()
      .pipe(finalize(() => (this.loading = false)))
      .subscribe((items) => {
        this.dataSource = this.tasks = items;
      });
  }
}
