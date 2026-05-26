import { Component, ViewChild } from '@angular/core';
import { BoardComponent } from '../board/board';
import { SudokoolApiService } from '../services/sudokool-api';

interface Game {
  id: number;
  dateStarted: string;
}

@Component({
  selector: 'app-home',
  imports: [BoardComponent],
  standalone: true,
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  @ViewChild(BoardComponent) boardComponent!: BoardComponent;

  showLoadPopup = false;
  games: Game[] = [];

  constructor(private api: SudokoolApiService) {}

  startGame() {
    this.boardComponent.startGame();
  }

  openLoadPopup() {
    this.api.getGames().subscribe({
      next: (games) => {
        this.games = games;
        this.showLoadPopup = true;
      },
      error: (err) => {
        console.error('Failed to load games:', err);
      },
    });
  }

  closeLoadPopup() {
    this.showLoadPopup = false;
  }

  loadGame(gameId: number) {
    this.boardComponent.loadGameById(gameId);
    this.showLoadPopup = false;
  }
}
