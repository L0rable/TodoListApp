import { Component } from '@angular/core';
import { TodoItem } from '../todo-item';
import { ApiService } from '../api.service';
import { TodoItemComponent } from '../todo-item/todo-item.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-todo-list',
  standalone: true,
  imports: [CommonModule, TodoItemComponent],
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.css'
})
export class TodoListComponent {
  todoItems: TodoItem[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.getTodoItems();
  }

  getTodoItems(): void {
    this.apiService.getTodoItem().subscribe(data => {
      this.todoItems = data;
    },
    error => error.log(error));
  }
  
  addTodoItem(todoItemName: string) {
    if (!todoItemName) return;

    this.apiService.addTodoItem(todoItemName).subscribe({
      next: (todoItem) => this.todoItems.push({
        "id": todoItem.id,
        "name": todoItem.name
      }),
      error: (e) => console.error(e)
    });
  }

  deleteTodoItem(todoItem: TodoItem) {
    this.apiService.deleteTodoItem(todoItem).subscribe({
      next: () => this.todoItems.splice(this.todoItems.indexOf(todoItem), 1),
      error: (e) => console.error(e)  
    });
  }
}
