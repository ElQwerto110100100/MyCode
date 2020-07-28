import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title: string;
  yeetWord: string;


  constructor() {
    this.title = "YEET"
    this.yeetWord = "memememememem"
  }
}
