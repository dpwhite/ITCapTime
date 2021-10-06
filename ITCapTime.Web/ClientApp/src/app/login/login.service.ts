import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, Subject, Subscriber  } from 'rxjs';
import { AuthUser } from 'src/app/login/auth';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  constructor(private http: HttpClient) { }

  registerUser(creds: string): Observable<AuthUser> {
    let authUser = null;
    return new Observable<AuthUser>((sub: Subscriber<any>) => {
      this.http
        .post<AuthUser>('api/register', creds)
        .subscribe((user: AuthUser) => {
          authUser = user;
          sub.next(user);
          sub.complete();
        },
          (err: any) => {
            authUser = null;
            sub.error(err);
          });
    });
  }

  loginUser(id: string): Observable<any> {
    //return new Observable<string>((sub: Subscriber<string>) => {
    //  this.http.get<string>('api/auth/test');
    //});
    
    return this.http.post(
      `api/auth/login/${id}`, null);
    
  }

  //test(): Observable<string> {
  //  return new Observable<string>((sub: Subscriber<string>) => {
  //    this.http.get<string>('api/test');
  //  });
  //}

  protected getRequestHeaders(): { headers: HttpHeaders | { [header: string]: string | string[]; } } {
    let headers = new HttpHeaders({
      'Authorization': '',
      'Content-Type': 'application/json',
      'Accept': `application/vnd.iman.v$1+json, application/json, text/plain, */*`,
      'App-Version': '1.0'
    });

    return { headers: headers };
  }
  //protected getMultipartRequestHeaders(): { headers: HttpHeaders | { [header: string]: string | string[]; } } {
  //  let headers = new HttpHeaders({
  //    'Authorization': '',

  //    'Content-Disposition': 'multipart/form-data',
  //    'Accept': `application/vnd.iman.v$1+json, application/json, text/plain, */*`,
  //    'App-Version': '1.0'
  //  });

  //  return { headers: headers };
  //}
}
