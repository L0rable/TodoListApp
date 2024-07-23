import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TodoItem } from './todo-item';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5206';

  constructor(private http: HttpClient) { }

  getTodoItem(): Observable<any> {
    return this.http.get(this.apiUrl + '/todoItems');
  }

  addTodoItem(todoItemName: string): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.apiUrl + '/todoItems', {"name": todoItemName}, { headers });
  }

  deleteTodoItem(todoItem: TodoItem): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.delete(this.apiUrl + '/todoItems/' + todoItem.id, { headers });
  }
}
