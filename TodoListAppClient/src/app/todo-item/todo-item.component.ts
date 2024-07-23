import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TodoItem } from '../todo-item';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-todo-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.css'
})
export class TodoItemComponent {
  @Input() item!: TodoItem;
  @Output() remove = new EventEmitter<TodoItem>();
}
