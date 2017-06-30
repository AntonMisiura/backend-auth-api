import { Component, OnInit, AfterViewChecked } from '@angular/core';
import { WindowRef } from '../shared/window-ref';
@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit, AfterViewChecked {

  constructor() { }
  
  ngOnInit() {
     
}
ngAfterViewChecked() {
  }

}
