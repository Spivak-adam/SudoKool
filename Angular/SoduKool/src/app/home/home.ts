import { Component } from '@angular/core';
import { Board } from '../board/board';

@Component({
  selector: 'app-home',
  imports: [Board],
  standalone: true,
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
}
