import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { TodoItem } from '../../models/todoItem';
import { finalize } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { AddTodoComponent } from '../../components/add-todo/add-todo.component';
import { ConfirmDeleteToDoComponent } from '../../components/confirm-delete-todo/confirm-delete-todo.component';
import { Router } from '@angular/router';
import { TaskDataSource } from '../../models/taskDataSource';
import { HttpErrorResponse } from '@angular/common/http';
import { SnackBarService } from '../../../../shared/services/snack-bar.service';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed,void', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
  encapsulation: ViewEncapsulation.None,
})
export class TaskListComponent implements OnInit {
  dataSource: TaskDataSource;
  displayedColumns: string[] = [
    'task',
    'deadline',
    'details',
    'isComplete',
    'id',
  ];
  columnsToDisplayWithExpand = [...this.displayedColumns, 'expand'];
  expandedElement: TodoItem | null;

  constructor(
    protected tasksService: TasksService,
    private dialog: MatDialog,
    protected snackBarService: SnackBarService
  ) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  public addItem(event: MouseEvent) {
    event.stopPropagation();
    const ref = this.dialog.open(AddTodoComponent);

    ref.afterClosed().subscribe((item: TodoItem) => {
      item.subTasks = [];
      this.tasksService.saveTodoItem(item).subscribe({
        next: (resp) => {
          const items = this.dataSource.getData();
          items.push(resp);
          this.dataSource.setData(items);
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.error('Error deleting to do.');
        },
      });
    });
  }

  addSubTask(task: TodoItem) {
    const ref = this.dialog.open(AddTodoComponent);

    ref.afterClosed().subscribe((item: TodoItem) => {
      if (!task.subTasks) {
        task.subTasks = [];
      }
      task.subTasks.push(item)
      this.tasksService.updateTodoItem(task).subscribe({
        next: (resp) => {
          const items = this.dataSource.getData();
          const filteredTasks = items.filter(x => x.id !== task.id);
          filteredTasks.push(resp);
          this.dataSource.setData(filteredTasks);
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.error('Error deleting to do.');
        },
      });
    });
  }

  deleteTask(id: string) {
    const ref = this.dialog.open(ConfirmDeleteToDoComponent, {data: id});

    ref.afterClosed().subscribe((resp) => {
      if (resp) {
        const items = this.dataSource.getData();
        this.dataSource.setData(items.filter(x => x.id !== id));
      }
    });
  }

  private loadTasks() {
    this.tasksService
      .getTodoItems()
      .subscribe({
        next: (items) => {
          this.dataSource = new TaskDataSource(items);
        },
        error: (err: HttpErrorResponse) => {
          this.snackBarService.error('Error loading Todo Tasks');
        }
      });
  }
}
