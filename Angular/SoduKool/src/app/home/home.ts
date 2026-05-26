import { Component, ViewChild } from '@angular/core';
import { BoardComponent } from '../board/board';

@Component({
  selector: 'app-home',
  imports: [BoardComponent],
  standalone: true,
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  @ViewChild(BoardComponent) boardComponent!: BoardComponent;

  startGame() {
    this.boardComponent.startGame();
  }

  loadGame() {
    this.boardComponent.loadGame();
  }
}