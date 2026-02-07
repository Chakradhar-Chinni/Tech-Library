Angular Rest Api Integration guide

-step 1: Enable HttpclientModule in app.module.ts 

		import { HttpClientModule } from '@angular/common/http';

		@NgModule({
		  imports: [
			BrowserModule,
			HttpClientModule
		  ]
		})
		export class AppModule { }

-step 2: create services module for API calls
	ng generate service services/task
	
	import { HttpClient } from '@angular/common/http';
	import { Observable } from 'rxjs'; //?
		@Injectable({
	  providedIn: 'root'
	})
	export class TaskService {

	  constructor(private http: HttpClient) { }

	   private apiUrl = 'https://jsonplaceholder.typicode.com/todos'; 

	  getTasks(): Observable<ITask[]> 
	  {
		return this.http.get<ITask[]>(this.apiUrl);
	  }
	}
