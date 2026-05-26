import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Board {
  id: number;
  gameId: number;
  quadrant: number;
  row: number;
  column: number;
  input: number;
  dateEnter: string;
}

export interface Game {
  id: number;
  dateStarted: string;
}

@Injectable({
  providedIn: 'root',
})
export class SudokoolApiService {
  private apiUrl = 'http://localhost:5278/api/sudokool';

  constructor(private http: HttpClient) {}

  startGame(): Observable<Board[]> {
    return this.http.post<Board[]>(`${this.apiUrl}/start-game`, {});
  }

  saveMove(board: Partial<Board>) {
    return this.http.post<Board>(`${this.apiUrl}/move`, board);
  }

  getBoards(gameId: number) {
    return this.http.get<Board[]>(`${this.apiUrl}/games/${gameId}/boards`);
  }

  getGames() {
    return this.http.get<Game[]>(`${this.apiUrl}/games`);
  }
}
