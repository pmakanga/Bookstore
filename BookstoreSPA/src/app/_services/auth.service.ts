import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

constructor(private http: HttpClient) { }

apiUrl = 'http://localhost:5000/api/auth/';

login(data: any): Observable<any> {
  return this.http.post<any>(this.apiUrl + 'login', data)
    .pipe(
      tap(_ => this.log('login')),
      catchError(this.handleError('login', []))
    );
}

register(data: any): Observable<any> {
  return this.http.post<any>(this.apiUrl + 'register', data)
    .pipe(
      tap(_ => this.log('login')),
      catchError(this.handleError('login', []))
    );
}

private handleError<T>(operation = 'operation', result?: T) {
  return (error: any): Observable<T> => {

    // TODO: send the error to remote logging infrastructure
    console.error(error); // log to console instead

    // TODO: better job of transforming error for user consumption
    this.log(`${operation} failed: ${error.message}`);

    // Let the app keep running by returning an empty result.
    return of(result as T);
  };
}
/** Log a AuthService  */
private log(message: string) {
  console.log(message);
}

}
